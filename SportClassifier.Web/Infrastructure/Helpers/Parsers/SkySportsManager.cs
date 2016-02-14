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
    public class SkySportsManager : SourceManager
    {       
        public override string ReadAuthor(HtmlNode node)
        {
            return string.Empty;
        }

        public override string ReadContent(HtmlNode node)
        {
            var content = node.SelectSingleNode("//div[@class='v5-art-body']");
            if (content == null)
                content = node.SelectSingleNode("//div[@itemprop='articleBody']");

            RemoveTags(content, "//ul[@class='art-betlink']");
            RemoveTags(content, "//div[@id='sky-bet-accordian']");
            RemoveTags(GetNodeByXpathAndClass(content,"//div","-skybet"));

            RemoveScripts(content);
   

            return CleanHtml(content.InnerHtml);            
        }

        public override string ReadMainImage(HtmlNode node)
        {
            return ReadImages(node, "");
        }
    }
}
