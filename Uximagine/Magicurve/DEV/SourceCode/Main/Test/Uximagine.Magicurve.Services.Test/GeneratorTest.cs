    using System;
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
        public void TestMethod2(string pageContent)
        {
            StylizedCodeGenerator codeGenerator = new StylizedCodeGenerator();
            if (!Directory.Exists(dir))  // if it doesn't exist, create
                Directory.CreateDirectory(dir);

            //string createText = codeGenerator.CreateHeaderPart(html, body) + Environment.NewLine;
            // use Path.Combine to combine 2 strings to a path
            File.WriteAllText(Path.Combine(dir, "styleWebPage.html"), pageContent);
        }

        [TestMethod]
        public void TestMethod3(string pageContent)
        {
            ResponsiveCodeGenerator codeGenerator = new ResponsiveCodeGenerator();
            if (!Directory.Exists(dir))  // if it doesn't exist, create
                Directory.CreateDirectory(dir);

            //string createText = codeGenerator.CreateHeaderPart(html, body) + Environment.NewLine;
            // use Path.Combine to combine 2 strings to a path
            File.WriteAllText(Path.Combine(dir, "responsiveWebPage.html"), pageContent);
        }

        [TestMethod]
        public void CodeGenTest()
        {
            var testClass = new GeneratorTest();
           
            IGenerator generator = new CodeGenerator.CodeGenerator();
            
            IGenerator styleGenerator = new StylizedCodeGenerator();

            IGenerator responsiveGenerator = new ResponsiveCodeGenerator();

            SortHelper sortHelper = new SortHelper();
            
            List<Control> controls = new List<Control>()
            {
                //new Control(){ 
                //    //Edges = new List<AForge.IntPoint>(){},
                //    //Height = 5,
                //    //Width = 10, 
                //    Type = Core.Models.ControlType.ComboBox,
                //    X = 50,
                //    Y = 128
                //},
                //new Control(){ 
                //    //Edges = new List<AForge.IntPoint>(){},
                //    //Height = 6,
                //    //Width = 20, 
                //    Type = Core.Models.ControlType.InputText,
                //    X = 50,
                //    Y = 100
                //},
                //new Button(){
                //    X = 50,
                //    Y = 72,
                //    Value = "Click Me"
                    
                //},
                //new Control(){
                //    Type = Core.Models.ControlType.CheckBox,
                //    Width = 28,
                //    Height = 28,
                //    X = 50,
                //    Y = 150
                    
                
                //}

                //check controls for DivAlgorithm
                new Button(){
                    X = 30,
                    Y = 80,
                    Height = 10,
                    Name = "C",
                    Value="C Button"
                },

                new Button(){
                    X = 60,
                    Y = 85,
                    Height = 10,
                    Name = "D",
                    Value="D Button"
                },

                new Button(){
                    X = 50,
                    Y = 55,
                    Height = 5,
                    Name = "B",
                    Value="B Button"
                },

                new Button(){
                    X = 10,
                    Y = 50,
                    Height = 4,
                    Name = "A",
                    Value="A Button"
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
            //without applying custom css file
            var result = generator.CreateHtmlCode(controls);

            Debug.Write(result);

            testClass.TestMethod1(result);

            //for style generator page
            var styleResult = styleGenerator.CreateHtmlCode(controls);

            Debug.Write(styleResult);

            testClass.TestMethod2(styleResult);

            //for resposive generator page
            var responsiveResult = responsiveGenerator.CreateHtmlCode(controls);

            Debug.Write(responsiveResult);

            testClass.TestMethod3(responsiveResult);


            //test x and y sort function
            //List<Control> sortXY = sortHelper.SortListYProperty(controls);

            //foreach (Control con in sortXY)
            //{
            //    Console.WriteLine(((Button)con).Name);
            //}

            //test DivAlgorithm method
            List<Row> rowList = sortHelper.DivAlgorithm(sortHelper.SortListYProperty(controls));
            foreach(Row row in rowList){
                foreach (Control item in row.Controls)
                {
                    Console.WriteLine(@"{0} {1} {2} {3} {4}",item.Type, item.X, item.Y, ((Button)item).Name,item.Height);
                }
                
            }
           
            Assert.AreEqual("A",((Button)(rowList[0].Controls[0])).Name);
            Assert.AreEqual("B", ((Button)(rowList[1].Controls[0])).Name);
            Assert.AreEqual("C", ((Button)(rowList[2].Controls[0])).Name);
            Assert.AreEqual("D", ((Button)(rowList[2].Controls[1])).Name);
            Assert.AreEqual(2, rowList[2].Controls.Count);

           // Assert.AreEqual(result, "<htm></html>");
        }
    }
}
