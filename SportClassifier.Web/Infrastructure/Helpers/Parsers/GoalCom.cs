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
    public class GoalCom : SourceManager
    {
        public override string ReadAuthor(HtmlNode node)
        {
            var author = node.SelectSingleNode("//h2[@class='author']");
            if (author != null)
            return author.InnerText;
            return string.Empty;
        }

        public override string ReadContent(HtmlNode node)
        {
            var content = node.SelectSingleNode("//div[@itemprop='articleBody']");
            if (content == null)
                content = node.SelectSingleNode("//div[@id='article_content']");
            if (content != null)
            {
                RemoveScripts(content);

                return CleanHtml(content.InnerHtml);
            }
            else
            {
                return null;
            }
        }

        public override string ReadMainImage(HtmlNode node)
        {
            return ReadImages(node.OwnerDocument.DocumentNode, "");            
        }
    }
}
