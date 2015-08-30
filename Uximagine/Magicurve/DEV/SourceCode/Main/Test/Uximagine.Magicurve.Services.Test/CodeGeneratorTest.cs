

namespace Uximagine.Magicurve.Services.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Uximagine.Magicurve.CodeGenerator;

    [TestClass]
    class CodeGeneratorTest
    {
        string path = @"E:\Level 4\Project\Code Generation\second example\webPage.html";
        public string html = "html";
        public string title = "<title>";
        public string head = "<head>";
        public string body = "body";

        [TestMethod]
        public void TestCreateHtmlCode()
        {

            CodeGenerator codeGenerator = new CodeGenerator();
            // This text is added only once to the file. 
            if (!File.Exists(path))
            {
                // Create a file to write to. 
                string createText = codeGenerator.CreateHtmlCode(html, body) + Environment.NewLine;
                File.WriteAllText(path, createText);
            }

        }
    }
}
