using System;
using System.IO;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class SnippetTests
    {
        // --- CONFIGURATION ---
        // update path to local project directory
        private string m_path = @"C:\Projects\Visual-Studio-jQuery-Code-Snippets\jQueryCodeSnippets";
        private string m_helpUrl = "https://github.com/kspearrin/Visual-Studio-jQuery-Code-Snippets";
        private string m_version = "1.4.0";
        // --- END CONFIGURATION ---

        [TestMethod]
        public void SnippetTitlesAreCorrect()
        {
            foreach (var snippetFile in Directory.EnumerateFiles(m_path, "*.snippet", SearchOption.AllDirectories))
            {
                var filePaths = snippetFile.Split(new string[] { "\\" }, StringSplitOptions.None);
                var fileName = filePaths[filePaths.Length - 1];
                var snippetName = fileName.Split('.')[0];

                var snippetDoc = new XmlDocument();
                snippetDoc.Load(snippetFile);

                var titleNode = snippetDoc.GetElementsByTagName("Title");
                var title = titleNode[0].InnerText;
                Assert.IsTrue(snippetName == title);
            }
        }

        [TestMethod]
        public void SnippetShortcutsAreCorrect()
        {
            foreach (var snippetFile in Directory.EnumerateFiles(m_path, "*.snippet", SearchOption.AllDirectories))
            {
                var filePaths = snippetFile.Split(new string[] { "\\" }, StringSplitOptions.None);
                var fileName = filePaths[filePaths.Length - 1];
                var snippetName = fileName.Split('.')[0];

                var snippetDoc = new XmlDocument();
                snippetDoc.Load(snippetFile);

                var shortcutNode = snippetDoc.GetElementsByTagName("Shortcut");
                var shortcut = shortcutNode[0].InnerText;
                Assert.IsTrue(snippetName == shortcut);
            }
        }

        [TestMethod]
        public void SnippetsHaveDescriptions()
        {
            foreach (var snippetFile in Directory.EnumerateFiles(m_path, "*.snippet", SearchOption.AllDirectories))
            {
                var snippetDoc = new XmlDocument();
                snippetDoc.Load(snippetFile);

                var descriptionNode = snippetDoc.GetElementsByTagName("Description");
                Assert.IsTrue(descriptionNode != null);

                var description = descriptionNode[0].InnerText;
                Assert.IsTrue(!string.IsNullOrWhiteSpace(description) && description.Length > 10);
            }
        }

        [TestMethod]
        public void SnippetsHaveAuthors()
        {
            foreach (var snippetFile in Directory.EnumerateFiles(m_path, "*.snippet", SearchOption.AllDirectories))
            {
                var snippetDoc = new XmlDocument();
                snippetDoc.Load(snippetFile);

                var authorNode = snippetDoc.GetElementsByTagName("Author");
                Assert.IsTrue(authorNode != null);

                var author = authorNode[0].InnerText;
                Assert.IsTrue(!string.IsNullOrWhiteSpace(author) && author.Length > 5);
            }
        }

        [TestMethod]
        public void SnippetsHaveHelpUrls()
        {
            var helpUrl = "https://github.com/kspearrin/Visual-Studio-jQuery-Code-Snippets";

            foreach (var snippetFile in Directory.EnumerateFiles(m_path, "*.snippet", SearchOption.AllDirectories))
            {
                var snippetDoc = new XmlDocument();
                snippetDoc.Load(snippetFile);

                var urlNode = snippetDoc.GetElementsByTagName("HelpUrl");
                Assert.IsTrue(urlNode != null);

                var url = urlNode[0].InnerText;
                Assert.IsTrue(!string.IsNullOrWhiteSpace(url) && url == helpUrl);
            }
        }

        [TestMethod]
        public void SnippetsAreProperFormattedXml()
        {
            foreach (var snippetFile in Directory.EnumerateFiles(m_path, "*.snippet", SearchOption.AllDirectories))
            {
                var contents = File.ReadAllText(snippetFile);
                Assert.IsTrue(contents.Contains("<?xml version=\"1.0\" encoding=\"utf-8\"?>"));
            }
        }

        [TestMethod]
        public void SnippetsHaveCorrectVersion()
        {
            foreach (var snippetFile in Directory.EnumerateFiles(m_path, "*.snippet", SearchOption.AllDirectories))
            {
                var snippetDoc = new XmlDocument();
                snippetDoc.Load(snippetFile);

                var snippetNode = snippetDoc.GetElementsByTagName("CodeSnippet");
                Assert.IsTrue(snippetNode != null);

                var formatAttr = snippetNode[0].Attributes["Format"];
                Assert.IsTrue(formatAttr != null);

                var format = formatAttr.InnerText;
                Assert.IsTrue(!string.IsNullOrWhiteSpace(format) && format == m_version);
            }
        }
    }
}
