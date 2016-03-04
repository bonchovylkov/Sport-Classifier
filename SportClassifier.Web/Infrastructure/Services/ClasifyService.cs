using SportClassifier.Data;
using SportClassifier.Models;
using SportClassifier.Web.Infrastructure.Classifier;
using SportClassifier.Web.Infrastructure.Classifier.Bayesian;
using SportClassifier.Web.Infrastructure.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MoreLinq;
using SportClassifier.Web.Infrastructure.Helpers;
using Kendo.Mvc.UI;
using SportClassifier.Web.Models;

namespace SportClassifier.Web.Infrastructure.Services
{
    public class ClasifyService : BaseService, IClasifyService
    {

        public ClasifyService(IUowData data)
            : base(data)
        {

        }

        public WordProbability GetWordProbability(string category, string word, int? catId = null)
        {
            try
            {
                WordProbability wp = null;
                long matchingCount = 0;
                long nonMatchingCount = 0;

                WordSource source = null;
                if (catId != null)
                {
                    source = this.Data.WordSources.All(new string[] { "Category" }).FirstOrDefault(w => w.Word == word && w.CategoryId == catId.Value);
                }
                else
                {
                    source = this.Data.WordSources.All(new string[] { "Category" }).FirstOrDefault(w => w.Word == word && w.Category.Name == category);
                }

                if (source != null)
                {
                    matchingCount = source.Matches;
                    nonMatchingCount = source.NonMatches;
                }

                wp = new WordProbability(word, matchingCount, nonMatchingCount);

                return wp;
            }
            catch (Exception ex)
            {
                throw new WordsDataSourceException("Problem updating WordProbability.", ex);
            }
        }

        public WordProbability GetWordProbability(string word)
        {
            return GetWordProbability(ICategorizedClassifierConstants.NEUTRAL_CATEGORY, word);
        }

        public void AddMatch(string category, string word, int? catId = null)
        {
            if (category == null)
                throw new ArgumentNullException("Category cannot be null.");
            UpdateWordProbability(category, word, true, catId);
        }

        //public ActionResult TrainModel()
        //{
        //    Trainer.TrainModel();
        //    int traindedWords = this.Data.WordsSource.All().Count();
        //    return View(traindedWords);

        //}

        public void AddNonMatch(string category, string word, int? catId = null)
        {
            if (category == null)
                throw new ArgumentNullException("Category cannot be null.");
            UpdateWordProbability(category, word, false, catId);
        }



        private void UpdateWordProbability(string category, string word, bool isMatch, int? catId = null)
        {

            try
            {
                // truncate word at 128 characters
                if (word.Length > 128)
                    word = word.Substring(0, 128);

                WordSource source = null;
                Category cat = null;
                if (catId != null)
                {
                    source = this.Data.WordSources.All(new string[] { "Category" }).FirstOrDefault(w => w.Word == word && w.CategoryId == catId.Value);
                    cat = cat = this.Data.Categories.All().FirstOrDefault(s => s.Id == catId.Value);
                }
                else
                {
                    source = this.Data.WordSources.All(new string[] { "Category" }).FirstOrDefault(w => w.Word == word && w.Category.Name == category);
                    cat = this.Data.Categories.All().FirstOrDefault(s => s.Name.Trim().ToLower() == category.Trim().ToLower());
                }


                if (source == null)//if don't exist i add it
                {
                    WordSource newSource = new WordSource()
                    {
                        Word = word,
                        CategoryId = cat.Id,

                    };
                    if (isMatch)
                    {
                        newSource.Matches = 1;
                        newSource.NonMatches = 0;
                    }
                    else
                    {
                        newSource.Matches = 0;
                        newSource.NonMatches = 1;
                    }

                    this.Data.WordSources.Add(newSource);
                    this.Data.SaveChanges();
                }
                else //if exist i update the matches or nonMatches
                {
                    if (isMatch)
                    {
                        source.Matches++;
                    }
                    else
                    {
                        source.NonMatches++;
                    }

                    this.Data.WordSources.Update(source);
                    this.Data.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new WordsDataSourceException("Problem updating WordProbability.", ex);
            }

        }





        public void AddMatch(string word)
        {
            UpdateWordProbability(ICategorizedClassifierConstants.NEUTRAL_CATEGORY, word, true);
        }

        public void AddNonMatch(string word)
        {
            UpdateWordProbability(ICategorizedClassifierConstants.NEUTRAL_CATEGORY, word, false);
        }

        public int TrainModel()
        {
            BayesianClassifier classifier =
              new BayesianClassifier(this, new DefaultTokenizer(), new CustomizableStopWordProvider("DefaultStopWords.txt"));

            int countTrainedNews = 0;
            List<Category> mainCategories = this.Data.Categories.All().DistinctBy(s => s.BaseCategoryId).ToList();
            List<Category> allCategories = this.Data.Categories.All(new string[] { "NewsItems" }).ToList();

            //get all news that havent been used for classification and arent for test
            var newsItems = this.Data.NewsItems.All(new string[] { "Categories" }).Where(s => s.UsedForClassication != true && s.IsForTest != true).ToList();
            for (int i = 0; i < newsItems.Count; i++)
            {
                var article = newsItems[i];
                var category = article.Categories.FirstOrDefault();
                if (category != null)
                {
                    if (category.BaseCategory == null)
                    {
                        continue;
                    }

                    //string text = article.CleanContent; - this take to mach time 
                    string text = article.Header;

                    try
                    {
                        //teach this category with this content
                        classifier.TeachMatch(category.BaseCategory.Name, text, category.BaseCategoryId);

                        for (int j = 0; j < mainCategories.Count; j++)
                        {
                            if (mainCategories[j].BaseCategory == null)
                            {
                                continue;
                            }

                            if (category.Id != mainCategories[j].Id)
                            {
                                //teach each other category that is not match for this sentance
                                classifier.TeachNonMatch(mainCategories[j].BaseCategory.Name, text, mainCategories[j].BaseCategory.Id);
                            }
                        }

                        countTrainedNews++;
                        article.UsedForClassication = true;
                        this.Data.NewsItems.Update(article);
                        this.Data.SaveChanges();


                    }
                    catch (Exception ex)
                    {
                        BaseHelper.WriteInFile("errors.txt", ex.Message);

                    }

                }

            }

            //foreach (var category in allCategories)
            //{
            //    var newsItems = category.NewsItems.Where(s => s.UsedForClassication != true && s.IsForTest!=true).ToList();
            //    for (int i = 0; i < newsItems.Count; i++)
            //    {
            //        var article = category.NewsItems.ElementAt(i);

            //        //string text = article.CleanContent; - this take to mach time 

            //            string text = article.Header;

            //        if (category.BaseCategory == null)
            //        {
            //            continue;
            //        }

            //        try
            //        {


            //            //teach this category with this content
            //            classifier.TeachMatch(category.BaseCategory.Name, text, category.BaseCategoryId);

            //            for (int j = 0; j < mainCategories.Count; j++)
            //            {
            //                if (mainCategories[j].BaseCategory == null)
            //                {
            //                    continue;
            //                }

            //                if (category.Id != mainCategories[j].Id)
            //                {
            //                    //teach each other category that is not match for this sentance
            //                    classifier.TeachNonMatch(mainCategories[j].BaseCategory.Name, text, mainCategories[j].BaseCategory.Id);
            //                }
            //            }

            //            countTrainedNews++;
            //            article.UsedForClassication = true;
            //            this.Data.NewsItems.Update(article);
            //            this.Data.SaveChanges();


            //        }
            //        catch (Exception ex)
            //        {
            //            BaseHelper.WriteInFile("errors.txt", ex.Message);

            //        }

            //    }
            //}

            //foreach (var category in TrainingData)
            //{
            //    //something wrong with stemming
            //    //List<string> listStemmedData = LucenePorterStemmer.ExecuteSteamming(category.Value);  for (int i = 0; i < listStemmedData.Count; i++)

            //    for (int i = 0; i < category.Value.Count; i++)
            //    {
            //        string word = category.Value[i];
            //        classifier.TeachMatch(category.Key, word);

            //        if (category.Key==ICategorizedClassifierConstants.POSSITIVE_CATEGORY)
            //        {
            //            classifier.TeachNonMatch(ICategorizedClassifierConstants.NEGATIVE_CATEGORY, word);
            //            classifier.TeachNonMatch(ICategorizedClassifierConstants.NEUTRAL_CATEGORY, word);
            //        }
            //        else if (category.Key == ICategorizedClassifierConstants.NEGATIVE_CATEGORY)
            //        {
            //            classifier.TeachNonMatch(ICategorizedClassifierConstants.POSSITIVE_CATEGORY, word);
            //            classifier.TeachNonMatch(ICategorizedClassifierConstants.NEUTRAL_CATEGORY, word);
            //        }
            //        else
            //        {
            //            classifier.TeachNonMatch(ICategorizedClassifierConstants.NEGATIVE_CATEGORY, word);
            //            classifier.TeachNonMatch(ICategorizedClassifierConstants.POSSITIVE_CATEGORY, word);
            //        }

            //    }
            //}



            return countTrainedNews;
        }


        public string ClassifyArticle(int articleId)
        {
            BayesianClassifier classifier =
              new BayesianClassifier(this, new DefaultTokenizer(), new CustomizableStopWordProvider());

            NewsItem article = this.Data.NewsItems.FirstOrDefault(s => s.Id == articleId);
            List<Category> mainCategories = this.Data.Categories.All().DistinctBy(s => s.BaseCategoryId).ToList();
            string category = "";
            decimal? probResult = 0;
            decimal? maxProbResult = 0;
            for (int i = 0; i < mainCategories.Count; i++)
            {
                bool isMatch = classifier.IsMatch(mainCategories[i].Name, article.Header, ref probResult, mainCategories[i].Id);
                if (isMatch)
                {
                    category = mainCategories[i].Name;
                    break;
                }
                else
                {
                    if (probResult>maxProbResult)
                    {
                        maxProbResult = probResult;
                        category= mainCategories[i].Name;
                    }
                }
            }

            return category;
        }





        public void ClassifyDataSourceResult(DataSourceResult news)
        {
            BayesianClassifier classifier =
             new BayesianClassifier(this, new DefaultTokenizer(), new CustomizableStopWordProvider());
            List<Category> mainCategories = this.Data.Categories.All().DistinctBy(s => s.BaseCategoryId).ToList();

            var sequenceEnum = news.Data.GetEnumerator();

            while (sequenceEnum.MoveNext())
            {
                var article = (sequenceEnum.Current as NewsItemViewModel);

                decimal? categoryPosibiity = 0;
                decimal? maxPropability = 0;
                string maxProbCategory = "";
                for (int i = 0; i < mainCategories.Count; i++)
                {
                    bool isMatch = classifier.IsMatch(mainCategories[i].Name, article.Header, ref categoryPosibiity, mainCategories[i].Id);
                    if (isMatch)
                    {
                        article.ClassificationCategory = mainCategories[i].Name;
                        article.ClassificationProbability = categoryPosibiity.Value;
                        break;
                    }
                    else
                    {
                        if (categoryPosibiity > maxPropability)
                        {
                            maxPropability = categoryPosibiity;
                            maxProbCategory = mainCategories[i].Name;
                        }
                    }

                }

                if (string.IsNullOrEmpty( article.ClassificationCategory))
                {
                    article.ClassificationCategory = maxProbCategory;
                    article.ClassificationProbability = maxPropability.Value;
                }


            }
        }
    }
}