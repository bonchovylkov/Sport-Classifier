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
    public class TelegraphCoUk : SourceManager
    {       
        public override string ReadAuthor(HtmlNode node)
        {
            var author = node.SelectSingleNode("//a[@rel='author']");
            if (author != null)
                return author.InnerText;
            return string.Empty;
        }

        public override string ReadContent(HtmlNode node)
        {
            var content = node.SelectSingleNode("//div[@id='mainBodyArea']");

            RemoveScripts(content);

            RemoveTags(content, "//div[@id='tmg-related-links']");

            return CleanHtml(content.InnerHtml);            
        }

        public override string ReadMainImage(HtmlNode node)
        {
            return ReadImages(node.OwnerDocument.DocumentNode, "");            
        }
    }
}
