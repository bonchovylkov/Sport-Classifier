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
    public class SBNation : SourceManager
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
            var content = GetNodeByXpathAndClass(node, "//div", "m-entry__body").FirstOrDefault();
           
            RemoveScripts(content);
            RemoveTags(GetNodeByXpathAndClass(node, "//div", "yog-ycb"));            

            return content.InnerHtml;            
        }

        public override string ReadMainImage(HtmlNode node)
        {
            return ReadImages(node.OwnerDocument.DocumentNode, "");            
        }
    }
}
