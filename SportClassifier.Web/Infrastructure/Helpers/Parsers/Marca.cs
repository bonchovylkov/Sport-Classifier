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
    public class Marca : SourceManager
    {
        public override string ReadAuthor(HtmlNode node)
        {
            var author = node.SelectSingleNode("//span[@class='nombre']");
            if (author != null)
            return author.InnerText;
            return string.Empty;
        }

        public override string ReadContent(HtmlNode node)
        {
            var content = node.SelectSingleNode("//div[@class='cuerpo_articulo']");

            RemoveScripts(content);

            RemoveTags(content, "//div[@id='fotos_noticia']");

            return CleanHtml(content.InnerHtml);            
        }

        public override string ReadMainImage(HtmlNode node)
        {
            return ReadImages(node.OwnerDocument.DocumentNode, "");            
        }
    }
}
