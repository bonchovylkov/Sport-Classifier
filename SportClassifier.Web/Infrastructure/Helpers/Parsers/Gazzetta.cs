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
    public class Gazzetta : SourceManager
    {
        public override string ReadAuthor(HtmlNode node)
        {
            return string.Empty;
        }

        public override string ReadContent(HtmlNode node)
        {
            var date = node.SelectSingleNode("//h5[@class='date']");
            if (date != null)
                date.Remove();
            var content = node.SelectSingleNode("//div[@id='read-speaker']");

            RemoveScripts(content);

            var header = node.SelectSingleNode("//h1");
            if (header != null)
                header.Remove();

            return CleanHtml(content.InnerHtml);
        }

        public override string ReadMainImage(HtmlNode node)
        {
            return ReadImages(node.OwnerDocument.DocumentNode, "");            
        }
    }
}
