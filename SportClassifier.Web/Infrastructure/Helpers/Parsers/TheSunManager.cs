using HtmlAgilityPack;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace SportClassifier.Web.Infrastructure.Helpers.Parsers
{
    public class TheSunManager : SourceManager
    {        
        public override string ReadAuthor(HtmlNode node)
        {
            var author = node.SelectNodes("//div[@id='headline']//div").FirstOrDefault(a => a.Attributes.Contains("class") &&
                a.Attributes["class"].Value.Contains("flag-author"));
            if (author != null)
            {
                author.ParentNode.Remove();
                return author.InnerText;
            }
            return string.Empty;
        }

        public override string ReadContent(HtmlNode node)
        {
            var content = node.SelectSingleNode("//div[@id='bodyText']");

            RemoveTags(content, "//div[@class='related-stories']");

            RemoveTags(content, "//object");


            return CleanHtml(content.InnerHtml);            
            
        }

        public override string ReadMainImage(HtmlNode node)
        {
            return ReadImages(node.OwnerDocument.DocumentNode, "");            
        }
    }
}
