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
    public class FoxSports : SourceManager
    {     
        public override string ReadContent(HtmlNode node)
        {
            var content = node.SelectSingleNode("//div[@class='fs-article']");
            if (content == null)
                content = node.SelectSingleNode("//div[@id='fsn_v3_main']");
            if (content == null)
                return null;
            RemoveScripts(content);
            RemoveTags(content, "//div[@id='story-top-container']");

            RemoveTags(content, "//div[@class='parentWrapper']");
            return CleanHtml(content.InnerHtml);
        }

        public override string ReadMainImage(HtmlNode node)
        {
            return ReadImages(node.OwnerDocument.DocumentNode, "");            
        }

        public override string ReadAuthor(HtmlNode node)
        {
            return string.Empty;
        }
    }
}
