using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Uximagine.Magicurve.CodeGenerator;
using Uximagine.Magicurve.Core.Models;
using Uximagine.Magicurve.Core.Shapes;

namespace Uximagine.Magicurve.Services.Test
{
    [TestClass]
    public class GeneratorTest
    {
        private readonly string dir = @"E:\Level 4\Project\Code Generation\second example";
        public string body = "body";
        public string head = "<head>";
        public string html = "html";
        public string title = "<title>";

        [TestMethod]
        public void TestMethod1(string pageContent)
        {
            var codeGenerator = new CodeGenerator.CodeGenerator();
            if (!Directory.Exists(dir)) // if it doesn't exist, create
                Directory.CreateDirectory(dir);

            //string createText = codeGenerator.CreateHeaderPart(html, body) + Environment.NewLine;
            // use Path.Combine to combine 2 strings to a path
            File.WriteAllText(Path.Combine(dir, "webPage.html"), pageContent);
        }

        [TestMethod]
        public void CodeGenTest()
        {
            var testClass = new GeneratorTest();

            IGenerator generator = new CodeGenerator.CodeGenerator();

            var controls = new List<Control>
            {
                new Control
                {
                    //Edges = new List<AForge.IntPoint>(){},
                    //Height = 5,
                    //Width = 10, 
                    Type = ControlType.ComboBox,
                    X = 50,
                    Y = 128
                },
                new Control
                {
                    //Edges = new List<AForge.IntPoint>(){},
                    //Height = 6,
                    //Width = 20, 
                    Type = ControlType.InputText,
                    X = 50,
                    Y = 100
                },
                new Button
                {
                    X = 50,
                    Y = 72,
                    Value = "Click Me"
                }
            };

            //Control button = new Control()
            //{
            //    Edges = new List<AForge.IntPoint>() { },
            //    Height = 5,
            //    Width = 10,
            //    Type = Core.Models.ControlType.Button,
            //    X = 5,
            //    Y = 22
            //};

            //controls.Add(button);

            // var result = generator.CreateHtmlCode(controls);

            //checking sorting based on Y value and return sorted Name values of controls
            //var query =
            //    from con in controls
            //    orderby con.Y
            //    select con;

            //foreach (var name in query)
            //{
            //    Console.Write(name.Name);
            //    Console.Write("\n");
            //}


            //When above sorting code implement inside CodeGenerator.createHtmlCode method then test it as below
            var result = generator.CreateHtmlCode(controls);

            Debug.Write(result);

            testClass.TestMethod1(result);


            // Assert.AreEqual(result, "<htm></html>");
        }
    }
}