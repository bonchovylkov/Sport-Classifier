using Argotic.Syndication;
using SportClassifier.Data;
using SportClassifier.Models;
using SportClassifier.Web.Infrastructure.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace SportClassifier.Web.Infrastructure.Services
{
    public class CrowlingService : BaseService, ICrowlingService
    {
        public CrowlingService(IUowData data)
            : base(data)
        {

        }
        public void Crow()
        {
            List<Source> sources = this.Data.Sources.All(new string[] { "Category", "SourceWebsite" }).ToList();



            foreach (var source in sources)
            {
                if (!(source.LastUpdated.HasValue && source.LastUpdated.Value > DateTime.Now.AddMinutes(30)) && source.IsActive)
                {
                }
                  //  ReadSource(source);
            }


        }

        //private void ReadSource(Source source)
        //{
        //    var count = 0;
        //    var client = new WebClient();
        //    try
        //    {
        //        using (var stream = client.OpenRead(source.StreamUrl))
        //        {
        //            try
        //            {
        //                #region RssFeed
        //                RssFeed feed = new RssFeed();
        //                feed.Load(stream);
        //                foreach (var i in feed.Channel.Items)
        //                {
        //                    try
        //                    {
        //                        if (ReadFeedItem(i, source))
        //                            count++;
        //                        Thread.Sleep(500);
        //                    }

        //                    catch (Exception e)
        //                    {
        //                        var failed = new FailedUrl();

        //                        failed.Url = i.Link.ToString();
        //                        failed.Exception = e.Message;
        //                        this.Data.FailedUrls.Add(failed);
        //                        this.Data.SaveChanges();
        //                        // Logger.SendMailForError(e, "", "", string.Format("Cannot Parse Article With Url - {0}", i.Link.ToString()));
        //                        continue;
        //                    }
        //                }
        //                #endregion RssFeed
        //            }
        //            catch (FormatException f)
        //            {
        //                #region AtomFeed
        //                AtomFeed afeed = new AtomFeed();
        //                using (var astream = client.OpenRead(source.StreamUrl))
        //                {
        //                    afeed.Load(astream);
        //                    foreach (var i in afeed.Entries)
        //                    {
        //                        try
        //                        {
        //                            if (ReadFeedItem(i, source))
        //                                count++;
        //                            Thread.Sleep(500);
        //                        }

        //                        catch (Exception e)
        //                        {
        //                            var failed = new FailedUrl();
        //                            failed.PartitionKey = Guid.NewGuid().ToString();
        //                            failed.RowKey = failed.PartitionKey;
        //                            failed.Url = i.Links.FirstOrDefault().Uri.ToString();
        //                            failed.Exception = e.Message;
        //                            BaseDAL.InsertEntity<FailedUrls>("FailedUrls", failed);
        //                            Logger.SendMailForError(e, "", "", string.Format("Cannot Parse Article With Url - {0}", i.Links.FirstOrDefault().Uri.ToString()));
        //                            continue;
        //                        }
        //                    }
        //                }
        //                #endregion AtomFeed
        //            }
        //            using (var db = new SportNetEntities())
        //            {
        //                var ed = db.Sources.Find(source.Id);
        //                ed.LastUpdated = DateTime.Now;
        //                db.SaveChanges();
        //            }
        //        }
        //        try
        //        {
        //            if (count > 0)
        //                CacheManager.Clear(CacheRegions.News);
        //        }
        //        catch (Exception e)
        //        {
        //            Logger.SendMailForError(e, "Error Clearing Cache for News");
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        var failed = new FailedUrl();
        //        failed.PartitionKey = Guid.NewGuid().ToString();
        //        failed.RowKey = failed.PartitionKey;
        //        failed.Url = source.StreamUrl;
        //        failed.Exception = e.Message;
        //        BaseDAL.InsertEntity<FailedUrls>("FailedUrls", failed); ;
        //    }
        //    Console.WriteLine(source.Name);
        //}

        //private bool ReadFeedItem(RssItem i, Source source)
        //{

        //    //Check if there is another article with the same title

        //    NewsItem oldItem = Data.NewsItems.All().FirstOrDefault(n => n.Title.Trim().ToLower() == i.Title.Trim().ToLower());

        //    if (oldItem != null && new DateTime(oldItem.DatePublished).Date == i.PublicationDate.Date)
        //    {
        //        if (!oldItem.Categories.Any(c => c.Id == source.))
        //        {
        //            oldItem.Categories.Add(db.Categories.Find(source.fkCategory));
        //            db.SaveChanges();
        //        }
        //        //Parse the article                        
        //        var forUpdateArticle = NewsManager.GetArticle(oldItem.RealId);
        //        if (forUpdateArticle == null && string.IsNullOrEmpty(forUpdateArticle.Content))
        //        {
        //            forUpdateArticle = NewsManager.ParseArticle(i.Link.ToString(), source.SourceWebsite.Name);
        //            NewsManager.InsertNewsItem(forUpdateArticle);
        //        }
        //        return false;
        //    }

        //    //Parse the article
        //    var article = NewsManager.ParseArticle(i.Link.ToString(), source.SourceWebsite.Name);
        //    if (article == null || string.IsNullOrEmpty(article.Content))
        //    {
        //        throw new Exception(string.Format("Connot Parse Article url : {0}", i.Link.ToString()));
        //    }
        //    if (string.IsNullOrEmpty(article.Href))
        //        article.Href = i.Link.ToString();
        //    //Check if there is already an article
        //    var id = BaseDAL.GetHash(article.Href);
        //    var oldArticle = NewsManager.GetArticle(id);
        //    //If its new one
        //    if (oldArticle == null)
        //    {
        //        article.Media = source.SourceWebsite.Name;

        //        if (string.IsNullOrEmpty(article.PublishedDate))
        //            article.PublishedDate = (i.PublicationDate == DateTime.MinValue ? DateTime.Now : i.PublicationDate).ToString("dd MMMM yyyy HH:mm");
        //        if (string.IsNullOrEmpty(article.Header))
        //        {
        //            article.Header = Regex.Replace(i.Title, @"<img\s[^>]*>(?:\s*?</img>)?", "", RegexOptions.IgnoreCase);
        //            article.PartitionKey = id;
        //            article.RowKey = article.PartitionKey;
        //        }
        //        using (var db = new SportNetEntities())
        //        {
        //            var newsitem = db.NewsItems.Create();
        //            newsitem.RealId = article.PartitionKey;
        //            newsitem.Title = article.Header;
        //            newsitem.MainPic = article.MainPic;
        //            newsitem.DatePublished = (i.PublicationDate == DateTime.MinValue ? DateTime.Now : i.PublicationDate).Ticks;
        //            newsitem.Categories.Add(db.Categories.Find(source.fkCategory));
        //            db.NewsItems.Add(newsitem);
        //            //Save Changes
        //            db.SaveChanges();
        //            article.RealId = newsitem.Id;
        //            try
        //            {
        //                NewsManager.InsertNewsItem(article);
        //            }
        //            catch (Exception e)
        //            {
        //                db.NewsItems.Remove(newsitem);
        //                db.SaveChanges();
        //                Logger.SendMailForError(e, "Reading Source", "1", i.Link.ToString());
        //            }
        //        }
        //        return true;
        //    }
        //    //if we already have it
        //    else
        //    {
        //        using (var db = new SportNetEntities())
        //        {
        //            var newsitem = db.NewsItems.Find(oldArticle.RealId);
        //            if (!newsitem.Categories.Any(c => c.Id == source.fkCategory))
        //            {
        //                newsitem.Categories.Add(db.Categories.Find(source.fkCategory));
        //                db.SaveChanges();
        //            }
        //        }
        //    }
        //    return false;
        //}
    }
}