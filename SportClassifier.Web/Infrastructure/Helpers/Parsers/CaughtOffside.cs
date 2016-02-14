using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace SportClassifier.Web.Infrastructure.Helpers.Parsers
{
    public class CaughtOffside : SourceManager
    {
        public override string ReadAuthor(HtmlNode node)
        {
            var author = node.SelectSingleNode("//a[@rel='author']");
            if (author != null)
                return author.InnerText;
            return string.Empty;
        }
        public override string ReadPublishedDate(HtmlNode node)
        {
            var date = node.OwnerDocument.DocumentNode.SelectSingleNode("/html/head/meta[10]");
            if (date != null)
                return date.Attributes["Content"].Value;
            return string.Empty;
        }
        public override string ReadTitle(HtmlNode node)
        {
            return HttpUtility.HtmlDecode(node.OwnerDocument.DocumentNode.SelectSingleNode("/html/head/title").InnerText);
        }
        public override string ReadContent(HtmlNode node)
        {
            RemoveTags(node.SelectNodes("//*[@id='taboola-bottom-main-column']"));
            var content = node.SelectSingleNode("//*[@id='news-story']/article");

            RemoveScripts(content);
            return content.InnerHtml;
        }

        public override string ReadMainImage(HtmlNode node)
        {
            return ReadImages(node.OwnerDocument.DocumentNode, "");
        }
    }
}
