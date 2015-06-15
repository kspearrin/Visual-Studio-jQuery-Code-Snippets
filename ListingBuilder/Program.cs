using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ListingBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            string readPath = @"../../../jQueryCodeSnippets";
            string listingFilePath = @"../../../SnippetListing.html";
            StringBuilder tableSb = new StringBuilder();

            tableSb.AppendLine("<table>");
            tableSb.AppendLine("<thead>");
            tableSb.AppendLine("<tr><th>Type</th><th>Shortcut</th><th>Description</th></tr>");
            tableSb.AppendLine("</thead>");
            tableSb.AppendLine("<tbody>");

            foreach (var snippetFile in Directory.EnumerateFiles(readPath, "*.snippet", SearchOption.AllDirectories).OrderBy(i => i))
            {
                var snippetDoc = new XmlDocument();
                snippetDoc.Load(snippetFile);

                var shortcutNode = snippetDoc.GetElementsByTagName("Shortcut");
                var shortcut = shortcutNode[0].InnerText;

                var descriptionNode = snippetDoc.GetElementsByTagName("Description");
                var description = descriptionNode[0].InnerText;

                tableSb.AppendLine(string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td></tr>", snippetFile.Contains("\\HTML") ? "HTML" : "JavaScript", shortcut, description));
            }

            tableSb.AppendLine("</tbody>");
            tableSb.AppendLine("</table>");

            using (StreamWriter writer = new StreamWriter(listingFilePath, false))
            {
                writer.WriteLine(tableSb.ToString());
            }
        }
    }
}
