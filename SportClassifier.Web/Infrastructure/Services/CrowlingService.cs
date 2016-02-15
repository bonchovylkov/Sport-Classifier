using Argotic.Syndication;
using HtmlAgilityPack;
using SportClassifier.Data;
using SportClassifier.Models;
using SportClassifier.Web.Infrastructure.Helpers;
using SportClassifier.Web.Infrastructure.Helpers.Parsers;
using SportClassifier.Web.Infrastructure.Services.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;

namespace SportClassifier.Web.Infrastructure.Services
{
    public class CrowlingService : BaseService, ICrowlingService
    {
        public CrowlingService(IUowData data)
            : base(data)
        {

        }
        public int Crow(bool? IsForTest=null)
        {
            //get all without football
            //List<Source> sources = this.Data.Sources.All(new string[] { "Category", "SourceWebsite" }).Where(s=>s.Category.BaseCategoryId!=1).ToList();
            List<Source> sources = this.Data.Sources.All(new string[] { "Category", "SourceWebsite" }).Where(s=>s.Category.BaseCategoryId!=1).ToList();


            int downloadedNews = 0;
            foreach (var source in sources)
            {
                if (!(source.LastUpdated.HasValue && source.LastUpdated.Value > DateTime.Now.AddMinutes(30)) && source.IsActive)
                {
                   downloadedNews += ReadSource(source,IsForTest);
                }

            }

            return downloadedNews;


        }

        private int ReadSource(Source source,bool? IsForTest)
        {
            var count = 0;
            var client = new WebClient();
            try
            {
                using (var stream = client.OpenRead(source.StreamUrl))
                {
                    try
                    {
                        #region RssFeed
                        RssFeed feed = new RssFeed();
                        feed.Load(stream);
                        foreach (var i in feed.Channel.Items)
                        {
                            try
                            {
                                if (ReadFeedItem(i, source,IsForTest))
                                    count++;
                                Thread.Sleep(500);
                            }

                            catch (Exception e)
                            {
                                var failed = new FailedUrl();

                                failed.Url = i.Link.ToString();
                                failed.Exception = e.Message;
                                this.Data.FailedUrls.Add(failed);
                                this.Data.SaveChanges();
                                continue;
                            }
                        }
                        #endregion RssFeed
                    }
                    catch (FormatException f)
                    {
                        #region AtomFeed
                        AtomFeed afeed = new AtomFeed();
                        using (var astream = client.OpenRead(source.StreamUrl))
                        {
                            afeed.Load(astream);
                            foreach (var i in afeed.Entries)
                            {
                                try
                                {
                                    if (ReadFeedItem(i, source,IsForTest))
                                        count++;
                                    Thread.Sleep(500);
                                }

                                catch (Exception e)
                                {
                                    var failed = new FailedUrl();

                                    failed.Url = i.Links.FirstOrDefault().ToString();
                                    failed.Exception = e.Message;
                                    this.Data.FailedUrls.Add(failed);
                                    this.Data.SaveChanges();
                                    continue;
                                }
                            }
                        }
                        #endregion AtomFeed
                    }

                    Source s = this.Data.Sources.FirstOrDefault(d => d.Id == source.Id);
                    s.LastUpdated = DateTime.Now;
                    this.Data.Sources.Update(s);
                    this.Data.SaveChanges();

                }
                //try
                //{
                //    if (count > 0)
                //        CacheManager.Clear(CacheRegions.News);
                //}
                //catch (Exception e)
                //{
                //    //TODO handle exc
                //}
            }
            catch (Exception e)
            {
                //TODO handle exc
            }

           
            Console.WriteLine(source.Name);
             return count;
        }

        private bool ReadFeedItem(RssItem i, Source source,bool? IsForTest)
        {

            //Check if there is another article with the same title

            NewsItem oldItem = Data.NewsItems.All().FirstOrDefault(n => n.Title.Trim().ToLower() == i.Title.Trim().ToLower());

            if (oldItem != null && new DateTime(oldItem.DatePublished).Date == i.PublicationDate.Date)
            {
                UpdateOldItem(i.Link.ToString(), source, oldItem);
                return false;
            }

            //Parse the article
            var article = ParseArticle(i.Link.ToString(), source.SourceWebsite.Name);

            if (article == null || string.IsNullOrEmpty(article.Content))
            {
                throw new Exception(string.Format("Connot Parse Article url : {0}", i.Link.ToString()));
            }

            if (string.IsNullOrEmpty(article.Href))
                article.Href = i.Link.ToString();

            //Check if there is already an article
            var oldArticle = this.Data.NewsItems.All().FirstOrDefault(s => s.Href == article.Href);

            //If its new one
            if (oldArticle == null)
            {
                article.Media = source.SourceWebsite.Name;

                if (string.IsNullOrEmpty(article.Header))
                {
                    article.Header = Regex.Replace(i.Title, @"<img\s[^>]*>(?:\s*?</img>)?", "", RegexOptions.IgnoreCase);
                }

                //newsitem.RealId = article.PartitionKey;
                article.Title = article.Header;
                article.CleanContent = BaseHelper.ScrubHtml(article.Content);
                article.DatePublished = (i.PublicationDate == DateTime.MinValue ? DateTime.Now : i.PublicationDate).Ticks;
                article.Categories.Add(this.Data.Categories.All().FirstOrDefault(s => s.Id == source.CategoryId));
                article.UsedForClassication = false;
                article.IsForTest = IsForTest.HasValue ? IsForTest.Value : false;
                this.Data.NewsItems.Add(article);
                this.Data.SaveChanges();

                return true;
            }
            //if we already have it
            else
            {

                if (!oldArticle.Categories.Any(c => c.Id == source.CategoryId))
                {
                    oldArticle.Categories.Add(this.Data.Categories.All().FirstOrDefault(s => s.Id == source.CategoryId));
                    this.Data.NewsItems.Update(article);
                    this.Data.SaveChanges();
                }

            }
            return false;
        }

        private void UpdateOldItem(string url, Source source, NewsItem oldItem)
        {
            if (!oldItem.Categories.Any(c => c.Id == source.CategoryId))
            {
                oldItem.Categories.Add(this.Data.Categories.All().FirstOrDefault(s => s.Id == source.CategoryId));
                this.Data.SaveChanges();
            }
            //Parse the article                        
            var forUpdateArticle = this.Data.NewsItems.All().FirstOrDefault(s => s.Id == oldItem.Id);

            if (forUpdateArticle == null && string.IsNullOrEmpty(forUpdateArticle.Content))
            {
                forUpdateArticle = ParseArticle(url, source.SourceWebsite.Name);
                this.Data.NewsItems.Update(forUpdateArticle);
                this.Data.SaveChanges();
            }
        }

        private bool ReadFeedItem(AtomEntry i, Source source,bool? IsForTest)
        {

            //Check if there is another article with the same title
            var url = i.Links.FirstOrDefault(l => l.ContentType == "text/html");


            NewsItem oldItem = this.Data.NewsItems.FirstOrDefault(s => s.Title.Trim().ToLower() == i.Title.Content.Trim().ToLower());

            if (oldItem != null && new DateTime(oldItem.DatePublished).Date == i.UpdatedOn.Date)
            {
                UpdateOldItem(url.Uri.ToString(), source, oldItem);

                return false;
            }

            //Parse the article
            var article = ParseArticle(url.Uri.ToString(), source.SourceWebsite.Name);
            if (article == null)
            {
                throw new Exception(string.Format("Connot Parse Article url : {0}", url));
            }

            if (string.IsNullOrEmpty(article.Href))
                article.Href = url.Uri.ToString();

            //Check if there is already an article
            var oldArticle = this.Data.NewsItems.All().FirstOrDefault(s => s.Href == article.Href);
            //If its new one
            if (oldArticle == null)
            {
                article.Media = source.SourceWebsite.Name;

                if (string.IsNullOrEmpty(article.Header))
                {
                    article.Header = Regex.Replace(i.Title.Content, @"<img\s[^>]*>(?:\s*?</img>)?", "", RegexOptions.IgnoreCase);
                }

                article.Title = article.Header;
                article.MainPic = article.MainPic;
                article.DatePublished = (i.UpdatedOn == DateTime.MinValue ? DateTime.Now : i.UpdatedOn).Ticks;
                article.Categories.Add(this.Data.Categories.All().FirstOrDefault(s => s.Id == source.CategoryId));
                article.UsedForClassication = false;
                article.IsForTest = IsForTest.HasValue ? IsForTest.Value : false;
                this.Data.NewsItems.Add(article);
                this.Data.SaveChanges();

                return true;
            }
            //if we already have it
            else
            {
                  if (!oldArticle.Categories.Any(c => c.Id == source.CategoryId))
                {
                    oldArticle.Categories.Add(this.Data.Categories.All().FirstOrDefault(s => s.Id == source.CategoryId));
                    this.Data.NewsItems.Update(article);
                    this.Data.SaveChanges();
                }
            }
            return false;
        }

        private NewsItem ParseArticle(string url, string source)
        {
            var uri = new Uri(url);
            var manager = GetManager(source);
            var doc = new HtmlDocument();

            CustomWebClient client = new CustomWebClient();

            using (var stream = new StreamReader(client.OpenRead(url)))
            {

                var encoding = doc.DetectEncoding(stream);
                using (var stream2 = client.OpenRead(url))
                {
                    if (encoding == null)
                        encoding = Encoding.UTF8;
                    doc.Load(stream2, encoding);
                }
            }
            var item = manager.ParseItem(doc, uri.Host);
            item.Href = client.ResponseUri.ToString();
            return item;
        }

        public static SourceManager GetManager(string host)
        {
            switch (host.ToLower().Replace("www.", "").Replace("www1.", ""))
            {
                case "thesun.co.uk": return new TheSunManager();
                case "skysports.com": return new SkySportsManager();
                case "bleacherreport.com": return new BleacherReport();
                case "telegraph.co.uk": return new TelegraphCoUk();
                case "independent.co.uk": return new IndependentCoUk();
                case "football365.com": return new Football365();
                case "dailymail.co.uk": return new DailyMailManager();
                case "mirror.co.uk": return new MirrorCoUK();
                case "goal.com": return new GoalCom();
                case "msn.foxsports.com": return new FoxSports();
                case "sports.yahoo.com": return new SportsYahoo();
                case "marca.com": return new Marca();
                case "bundesliga.com": return new Bundesliga();
                case "english.gazzetta.it": return new Gazzetta();
                case "espnfc.com": return new EspnFC();
                case "planetf1.com": return new PlanetF1();
                case "sbnation": return new SBNation();
                case "caughtoffside": return new CaughtOffside();
                default: return null;
            }
        }

        public static string GetLogo(string source)
        {
            switch (source)
            {
                case "TheSun.co.uk": return "/img/logos/sun-logo_1618485a.jpg";
                case "Skysports.com": return "/img/logos/site-logo.png";
                case "BleacherReport.com": return "/img/logos/Bleachreport.png";
                case "Telegraph.Co.Uk": return "/img/logos/The-Telegraph-logo.png";
                case "Independent.Co.Uk": return "/img/logos/independent_masthead.png";
                case "Football365.com": return "/img/logos/football365.gif";
                case "DailyMail.co.uk": return "/img/logos/dailymail.jpg";
                case "Mirror.co.uk": return "/img/logos/mirror_logo.png";
                case "Goal.com": return "/img/logos/goal.com.jpg";
                case "FoxSports.com": return "/img/logos/FoxSoccer_Logo.png";
                case "Yahoo.com": return "/img/logos/Yahoo__Sports-logo-E6972D9374-seeklogo.gif";
                case "Marca.com": return "/img/logos/marca-com.png";
                case "Bundesliga.com": return "/img/logos/bl_logo_l.png";
                case "Gazzetta.it": return "/img/logos/Gazzeta.png";
                case "espnfc.com": return "/img/logos/fc-logo-header-fp.png";
                default: return source;
            }
        }
    }
}