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
    public class BleacherReport : SourceManager
    {             
        public override string ReadAuthor(HtmlNode node)
        {
            var author = node.SelectSingleNode("//a[@class='author']");
            if (author != null)
                return author.InnerText;
            return string.Empty;
        }
      
        public override string ReadContent(HtmlNode node)
        {
            var content = node.SelectSingleNode("//div[@class='article-body']");
            if (content == null)
                content = node.SelectSingleNode("//div[@class='slide-description']");

            RemoveScripts(content);
            RemoveTags(GetNodeByXpathAndClass(node,"//div","poll_module"));
            return CleanHtml(content.InnerHtml);
        }

        public override string ReadMainImage(HtmlNode node)
        {
            return ReadImages(node, "");            
        }
    }
}
