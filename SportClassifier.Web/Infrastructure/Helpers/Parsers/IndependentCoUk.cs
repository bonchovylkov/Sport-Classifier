﻿using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace SportClassifier.Web.Infrastructure.Helpers.Parsers
{
    public class IndependentCoUk : SourceManager
    {     
        public override string ReadAuthor(HtmlNode node)
        {
            var author = node.SelectSingleNode("//span[@class='authorName']");
            if (author != null)
                return author.InnerText;
            return string.Empty;
        }

        public override string ReadContent(HtmlNode node)
        {
            var content = node.SelectNodes("//div").FirstOrDefault(d =>
   d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("articleContent"));

            RemoveScripts(content);

            return CleanHtml(content.InnerHtml);            
        }

        public override string ReadMainImage(HtmlNode node)
        {
            return ReadImages(node.OwnerDocument.DocumentNode.SelectSingleNode("//div[@id='main']"), "");            
        }
    }
}
