using System;
using System.IO;
using System.Xml;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class SnippetTests
    {
        // --- CONFIGURATION ---
        private const string SnippetPath = @"../../../jQueryCodeSnippets";
        private const string HelpUrl = "https://github.com/kspearrin/Visual-Studio-jQuery-Code-Snippets";
        private const string Version = "1.5.0";
        // --- END CONFIGURATION ---

        [Test]
        public void SnippetTitlesAreCorrect()
        {
            foreach (var snippetFile in Directory.EnumerateFiles(SnippetPath, "*.snippet", SearchOption.AllDirectories))
            {
                var snippetName = Path.GetFileNameWithoutExtension(snippetFile);

                var snippetDoc = new XmlDocument();
                snippetDoc.Load(snippetFile);

                var titleNode = snippetDoc.GetElementsByTagName("Title");
                var title = titleNode[0].InnerText;
                Assert.IsTrue(snippetName == title);
            }
        }

        [Test]
        public void SnippetShortcutsAreCorrect()
        {
            foreach (var snippetFile in Directory.EnumerateFiles(SnippetPath, "*.snippet", SearchOption.AllDirectories))
            {
                var snippetName = Path.GetFileNameWithoutExtension(snippetFile);

                var snippetDoc = new XmlDocument();
                snippetDoc.Load(snippetFile);

                var shortcutNode = snippetDoc.GetElementsByTagName("Shortcut");
                var shortcut = shortcutNode[0].InnerText;
                Assert.IsTrue(snippetName == shortcut);
            }
        }

        [Test]
        public void SnippetsHaveDescriptions()
        {
            foreach (var snippetFile in Directory.EnumerateFiles(SnippetPath, "*.snippet", SearchOption.AllDirectories))
            {
                var snippetDoc = new XmlDocument();
                snippetDoc.Load(snippetFile);

                var descriptionNode = snippetDoc.GetElementsByTagName("Description");
                Assert.IsTrue(descriptionNode != null);

                var description = descriptionNode[0].InnerText;
                Assert.IsTrue(!string.IsNullOrWhiteSpace(description) && description.Length > 10);
            }
        }

        [Test]
        public void SnippetsHaveAuthors()
        {
            foreach (var snippetFile in Directory.EnumerateFiles(SnippetPath, "*.snippet", SearchOption.AllDirectories))
            {
                var snippetDoc = new XmlDocument();
                snippetDoc.Load(snippetFile);

                var authorNode = snippetDoc.GetElementsByTagName("Author");
                Assert.IsTrue(authorNode != null);

                var author = authorNode[0].InnerText;
                Assert.IsTrue(!string.IsNullOrWhiteSpace(author) && author.Length > 5);
            }
        }

        [Test]
        public void SnippetsHaveHelpUrls()
        {
            foreach (var snippetFile in Directory.EnumerateFiles(SnippetPath, "*.snippet", SearchOption.AllDirectories))
            {
                var snippetDoc = new XmlDocument();
                snippetDoc.Load(snippetFile);

                var urlNode = snippetDoc.GetElementsByTagName("HelpUrl");
                Assert.IsTrue(urlNode != null);

                var url = urlNode[0].InnerText;
                Assert.IsTrue(!string.IsNullOrWhiteSpace(url) && url == HelpUrl);
            }
        }

        [Test]
        public void SnippetsAreProperFormattedXml()
        {
            foreach (var snippetFile in Directory.EnumerateFiles(SnippetPath, "*.snippet", SearchOption.AllDirectories))
            {
                var contents = File.ReadAllText(snippetFile);
                Assert.IsTrue(contents.Contains("<?xml version=\"1.0\" encoding=\"utf-8\"?>"));
            }
        }

        [Test]
        public void SnippetsHaveCorrectVersion()
        {
            foreach (var snippetFile in Directory.EnumerateFiles(SnippetPath, "*.snippet", SearchOption.AllDirectories))
            {
                var snippetDoc = new XmlDocument();
                snippetDoc.Load(snippetFile);

                var snippetNode = snippetDoc.GetElementsByTagName("CodeSnippet");
                Assert.IsTrue(snippetNode != null);

                var formatAttr = snippetNode[0].Attributes["Format"];
                Assert.IsTrue(formatAttr != null);

                var format = formatAttr.InnerText;
                Assert.IsTrue(!string.IsNullOrWhiteSpace(format) && format == Version);
            }
        }
    }
}
