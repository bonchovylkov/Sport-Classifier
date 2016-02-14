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
    public class EspnFC : SourceManager
    {              

        public override string ReadAuthor(HtmlNode node)
        {
            var author = node.SelectSingleNode("//cite");
            if (author != null)
                return author.InnerText;
            return string.Empty;
        }

     
        public override string ReadContent(HtmlNode node)
        {
            var content = node.SelectSingleNode("//div[@class='bg-opaque pad-16 article']");
            if (content == null)
                content = node.SelectSingleNode("//article");


            RemoveScripts(content);
            RemoveTags(content, "//div");

            return CleanHtml(content.InnerHtml);
        }

        public override string ReadMainImage(HtmlNode node)
        {
            return ReadImages(node, "");            
        }
    }
}
