using HtmlAgilityPack;
using SportClassifier.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;

namespace SportClassifier.Web.Infrastructure.Helpers
{
     public abstract class SourceManager
    {
        public NewsItem ParseItem(HtmlDocument doc, string host)
        {
            var header = ReadTitle(doc.DocumentNode);
            var result = new NewsItem();
            result.Header = header;
            result.Author = ReadAuthor(doc.DocumentNode);
            long date;
            result.DatePublished =   long.TryParse(ReadPublishedDate(doc.DocumentNode),out date)?date:0;
            result.MainPic = ReadMainImage(doc.DocumentNode);
            RemoveTags(doc.DocumentNode.SelectNodes("//a").Where(d => d.Attributes.Contains("href") && d.Attributes["href"].Value.Contains("skybet")));
            RemoveTags(doc.DocumentNode.SelectNodes("//a").Where(d => d.Attributes.Contains("href") && d.Attributes["href"].Value.Contains("betfair")));
            RemoveTags(doc.DocumentNode.SelectNodes("//a").Where(d => d.Attributes.Contains("href") && d.Attributes["href"].Value.Contains("bet365")));
            RemoveTags(doc.DocumentNode.SelectNodes("//a").Where(d => d.Attributes.Contains("href") && d.Attributes["href"].Value.Contains("williamhill")));
            RemoveTags(doc.DocumentNode.SelectNodes("//a").Where(d => d.Attributes.Contains("href") && d.Attributes["href"].Value.Contains("paddypower")));
            RemoveTags(doc.DocumentNode.SelectNodes("//a").Where(d => d.Attributes.Contains("href") && d.Attributes["href"].Value.Contains("coral.co.uk")));
            RemoveTags(doc.DocumentNode.SelectNodes("//a").Where(d => d.Attributes.Contains("href") && d.Attributes["href"].Value.Contains("betvictor")));
            RemoveTags(doc.DocumentNode.SelectNodes("//a").Where(d => d.Attributes.Contains("href") && d.Attributes["href"].Value.Contains("doubleclick")));
            RemoveTags(doc.DocumentNode.SelectNodes("//a").Where(d => d.Attributes.Contains("href") && d.Attributes["href"].Value.Contains("victor.com")));
            RemoveTags(doc.DocumentNode.SelectNodes("//a").Where(d => d.Attributes.Contains("href") && !d.Attributes["href"].Value.Contains("http://")));
            RemoveTags(doc.DocumentNode.Descendants().Where(d => d.Attributes.Contains("data-bookmaker-url")));

            result.Content = ReadContent(doc.DocumentNode);
            result.CleanContent = BaseHelper.ScrubHtml(result.Content);
            return result;
        }
        public IEnumerable<HtmlNode> GetNodeByXpathAndClass(HtmlNode node, string xpath, string cl)
        {
            return node.SelectNodes(xpath).Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains(cl));
        }
        public void RemoveBetting(HtmlNode node)
        {
            RemoveTags(node.SelectNodes("//a").Where(d => d.Attributes.Contains("href") && d.Attributes["href"].Value.Contains("skybet")));
        }
        public static string ReadImages(HtmlNode doc, string host)
        {
            HtmlNodeCollection images = doc.SelectNodes("//img");

            List<KeyValuePair<HtmlNode, int>> realImages = new List<KeyValuePair<HtmlNode, int>>();

            foreach (HtmlNode image in images)
            {
                string img = image.GetAttributeValue("src", string.Empty);
                if (string.IsNullOrEmpty(img))
                    img = image.GetAttributeValue("data-src", string.Empty);
                if (!string.IsNullOrEmpty(img) && !img.Contains("adserver.adtech"))
                {
                    if (!img.StartsWith("http"))
                        if (img.First() == '/')
                            img = host + img;
                        else
                            img = host + "/" + img;

                    if (Uri.IsWellFormedUriString(img, UriKind.Absolute))
                    {
                        try
                        {
                            var client = new WebClient();
                            Image imageReal;
                            using (var stream = client.OpenRead(HttpUtility.HtmlDecode(img)))
                            {
                                imageReal = Bitmap.FromStream(stream);
                            }
                            if (imageReal.Width > 200 && imageReal.Height > 200)
                            {
                                realImages.Add(new KeyValuePair<HtmlNode, int>(image, imageReal.Height * imageReal.Width));
                            }
                            else
                                image.Remove();
                        }
                        catch (Exception e)
                        {
                           //TODO handle exp
                        }
                    }
                    else
                        image.Remove();
                }
            }
            var result = realImages.OrderByDescending(k => k.Value).Select(s => s.Key).FirstOrDefault();
            if (result != null)
                result.Remove();
            return result != null ? HttpUtility.HtmlDecode(result.Attributes["src"].Value) : null;
        }
        public string CleanHtml(string html)
        {
            // start by completely removing all unwanted tags 
            html = Regex.Replace(html, @"<[/]?(font|span|xml|del|ins|[ovwxp]:\w+)[^>]*?>", "", RegexOptions.IgnoreCase);
            // then run another pass over the html (twice), removing unwanted attributes 
            html = Regex.Replace(html, @"<([^>]*)(?:class|lang|style|size|face|[ovwxp]:\w+)=(?:'[^']*'|""[^""]*""|[^>]+)([^>]*)>", "<$1$2>", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"<([^>]*)(?:class|lang|style|size|face|[ovwxp]:\w+)=(?:'[^']*'|""[^""]*""|[^>]+)([^>]*)>", "<$1$2>", RegexOptions.IgnoreCase);
            return html;
        }
        public void RemoveScripts(HtmlNode content)
        {
            if (content != null)
            {
                var scripts = content.SelectNodes("//script");
                if (scripts != null)
                    foreach (var item in scripts)
                    {
                        item.Remove();
                    }
            }
        }
        public void RemoveTags(HtmlNode content, string xpat)
        {
            var forDelete = content.SelectNodes(xpat);
            RemoveTags(forDelete);
        }
        public void RemoveTags(IEnumerable<HtmlNode> nodes)
        {
            if (nodes != null)
            {
                foreach (var item in nodes.ToList())
                {
                    item.Remove();
                }
            }
        }
        public virtual string ReadTitle(HtmlNode node)
        {
            return string.Empty;
        }
        public abstract string ReadAuthor(HtmlNode node);
        public virtual string ReadPublishedDate(HtmlNode node)
        {
            return string.Empty;
        }
        public abstract string ReadContent(HtmlNode node);
        public abstract string ReadMainImage(HtmlNode node);
    }
}