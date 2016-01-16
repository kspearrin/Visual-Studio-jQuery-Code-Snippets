using System;
using System.IO;
using System.Xml;
using Xunit;

namespace Tests
{
    public class SnippetTests
    {
        // --- CONFIGURATION ---
        private const string SnippetPath = @"../../../jQueryCodeSnippets";
        private const string HelpUrl = "https://github.com/kspearrin/Visual-Studio-jQuery-Code-Snippets";
        private const string Version = "1.6.0";
        // --- END CONFIGURATION ---

        [Fact]
        public void SnippetTitlesAreCorrect()
        {
            foreach (var snippetFile in Directory.EnumerateFiles(SnippetPath, "*.snippet", SearchOption.AllDirectories))
            {
                var snippetName = Path.GetFileNameWithoutExtension(snippetFile);

                var snippetDoc = new XmlDocument();
                snippetDoc.Load(snippetFile);

                var titleNode = snippetDoc.GetElementsByTagName("Title");
                var title = titleNode[0].InnerText;
                Assert.True(snippetName == title);
            }
        }

        [Fact]
        public void SnippetShortcutsAreCorrect()
        {
            foreach (var snippetFile in Directory.EnumerateFiles(SnippetPath, "*.snippet", SearchOption.AllDirectories))
            {
                var snippetName = Path.GetFileNameWithoutExtension(snippetFile);

                var snippetDoc = new XmlDocument();
                snippetDoc.Load(snippetFile);

                var shortcutNode = snippetDoc.GetElementsByTagName("Shortcut");
                var shortcut = shortcutNode[0].InnerText;
                Assert.True(snippetName == shortcut);
            }
        }

        [Fact]
        public void SnippetsHaveDescriptions()
        {
            foreach (var snippetFile in Directory.EnumerateFiles(SnippetPath, "*.snippet", SearchOption.AllDirectories))
            {
                var snippetDoc = new XmlDocument();
                snippetDoc.Load(snippetFile);

                var descriptionNode = snippetDoc.GetElementsByTagName("Description");
                Assert.True(descriptionNode != null);

                var description = descriptionNode[0].InnerText;
                Assert.True(!string.IsNullOrWhiteSpace(description) && description.Length > 10);
            }
        }

        [Fact]
        public void SnippetsHaveAuthors()
        {
            foreach (var snippetFile in Directory.EnumerateFiles(SnippetPath, "*.snippet", SearchOption.AllDirectories))
            {
                var snippetDoc = new XmlDocument();
                snippetDoc.Load(snippetFile);

                var authorNode = snippetDoc.GetElementsByTagName("Author");
                Assert.True(authorNode != null);

                var author = authorNode[0].InnerText;
                Assert.True(!string.IsNullOrWhiteSpace(author) && author.Length > 5);
            }
        }

        [Fact]
        public void SnippetsHaveHelpUrls()
        {
            foreach (var snippetFile in Directory.EnumerateFiles(SnippetPath, "*.snippet", SearchOption.AllDirectories))
            {
                var snippetDoc = new XmlDocument();
                snippetDoc.Load(snippetFile);

                var urlNode = snippetDoc.GetElementsByTagName("HelpUrl");
                Assert.True(urlNode != null);

                var url = urlNode[0].InnerText;
                Assert.True(!string.IsNullOrWhiteSpace(url) && url == HelpUrl);
            }
        }

        [Fact]
        public void SnippetsAreProperFormattedXml()
        {
            foreach (var snippetFile in Directory.EnumerateFiles(SnippetPath, "*.snippet", SearchOption.AllDirectories))
            {
                var contents = File.ReadAllText(snippetFile);
                Assert.True(contents.Contains("<?xml version=\"1.0\" encoding=\"utf-8\"?>"));
            }
        }

        [Fact]
        public void SnippetsHaveCorrectVersion()
        {
            foreach (var snippetFile in Directory.EnumerateFiles(SnippetPath, "*.snippet", SearchOption.AllDirectories))
            {
                var snippetDoc = new XmlDocument();
                snippetDoc.Load(snippetFile);

                var snippetNode = snippetDoc.GetElementsByTagName("CodeSnippet");
                Assert.True(snippetNode != null);

                var formatAttr = snippetNode[0].Attributes["Format"];
                Assert.True(formatAttr != null);

                var format = formatAttr.InnerText;
                Assert.True(!string.IsNullOrWhiteSpace(format) && format == Version);
            }
        }
    }
}
