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
    public class SportsYahoo : SourceManager
    {        
        public override string ReadAuthor(HtmlNode node)
        {
            var author = node.SelectSingleNode("//a[@rel='author']");
            if (author != null)
                return author.InnerText;
            else
            {
                author = node.SelectSingleNode("//meta[@itemprop='author']");
            }
            return string.Empty;
        }

        public override string ReadContent(HtmlNode node)
        {
            var content = node.SelectSingleNode("//div[@id='mediaarticlebody']");
            if (content == null)
                content = node.SelectSingleNode("//div[@itemprop='articleBody']");
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
