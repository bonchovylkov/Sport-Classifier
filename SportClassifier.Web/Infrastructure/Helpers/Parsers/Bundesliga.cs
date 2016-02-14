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
    public class Bundesliga : SourceManager
    {

       
        public override string ReadAuthor(HtmlNode node)
        {
            return string.Empty;
        }
     

        public override string ReadContent(HtmlNode node)
        {
            var content = node.SelectSingleNode("//div[@id='contentLeft']");

            RemoveScripts(content);
            RemoveTags(content, "//div[@class='infoBar']");
            RemoveTags(content, "//div[@class='mixedGallery']");


            var itemsForRemove = GetNodeByXpathAndClass(content, "//div", "socialBar");
            RemoveTags(itemsForRemove);

            return CleanHtml(content.InnerHtml);
        }

        public override string ReadMainImage(HtmlNode node)
        {            
            return ReadImages(node, "");            
        }
    }
}
