using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Drawing;

namespace SportClassifier.Web.Infrastructure.Helpers.Parsers
{
    public class DailyMailManager : SourceManager
    {        

        public override string ReadAuthor(HtmlNode node)
        {
            var author = node.SelectSingleNode("//a[@class='author']");
            if (author != null)
            {
                author.ParentNode.Remove();
                return author.InnerText;
            }
            return string.Empty;
        }

        public override string ReadContent(HtmlNode node)
        {
            var content = node.SelectSingleNode("//div[@id='js-article-text']");



            RemoveTags(content, "//div[@id='articleIconLinksContainer']");
            RemoveTags(content, "//div[@class='relatedItemsTopBorder']");
            RemoveTags(content, "//div[@class='relatedItems']");
            RemoveTags(content, "//div[@class='shareArticles']");
            RemoveScripts(content);
            RemoveTags(content, "//div[@id='most-read-news-wrapper']");

            RemoveTags(content, "//div[@id='wideCommentAdvert']");
            RemoveTags(content, "//div[@id='reader-comments']");
            RemoveTags(content, "//div[@id='most-watched-videos-wrapper']");
            RemoveTags(GetNodeByXpathAndClass(content, "//div", "floatRHS"));

            return CleanHtml(content.InnerHtml);

        }

        public override string ReadMainImage(HtmlNode node)
        {
            return ReadImages(node, "");            
        }
    }
}
