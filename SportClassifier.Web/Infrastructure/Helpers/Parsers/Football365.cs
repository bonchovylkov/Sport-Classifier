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
    public class Football365 : SourceManager
    {      

        public override string ReadAuthor(HtmlNode node)
        {
            return string.Empty;
        }

       

        public override string ReadContent(HtmlNode node)
        {

            var content = node.SelectNodes("//div").FirstOrDefault(d =>
    d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("base-article-body"));

            RemoveScripts(content);

            RemoveTags(GetNodeByXpathAndClass(content,"//div","base-article-col"));
            RemoveTags(GetNodeByXpathAndClass(content, "//div", "OUTBRAIN"));
            RemoveTags(GetNodeByXpathAndClass(content, "//div", "base-article-comment"));
            RemoveTags(GetNodeByXpathAndClass(content, "//div", "v5-art-tools"));
            RemoveTags(GetNodeByXpathAndClass(content, "//div", "base-art-tools"));          
            return CleanHtml(content.InnerHtml);            
        }

        public override string ReadMainImage(HtmlNode node)
        {
            return ReadImages(node, "");            
        }
    }
}
