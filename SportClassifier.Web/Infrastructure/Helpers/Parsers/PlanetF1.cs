using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportClassifier.Web.Infrastructure.Helpers.Parsers
{
    public class PlanetF1 : SourceManager
    {
        public override string ReadAuthor(HtmlNode node)
        {           
            return string.Empty;
        }

        public override string ReadContent(HtmlNode node)
        {
            var content = GetNodeByXpathAndClass(node, "//div", "base-article-body").FirstOrDefault();
            RemoveScripts(content);          
            return CleanHtml(content.InnerHtml);
        }

        public override string ReadMainImage(HtmlNode node)
        {
            return ReadImages(node, "");
        }
    }
}
