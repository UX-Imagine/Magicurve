    using System;
    using System.Collections.Generic;
using System.Diagnostics;
    using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Uximagine.Magicurve.CodeGenerator;
using Uximagine.Magicurve.Core.Models;
    using Uximagine.Magicurve.Core.Shapes;
using System;

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
        
       
        public void GenarateSimpleHTMLPage(string pageContent)
        {
           // var codeGenerator = new CodeGenerator.CodeGenerator();
            if (!Directory.Exists(dir)) // if it doesn't exist, create
                Directory.CreateDirectory(dir);

            //string createText = codeGenerator.CreateHeaderPart(html, body) + Environment.NewLine;
            // use Path.Combine to combine 2 strings to a path
            File.WriteAllText(Path.Combine(dir, "webPage.html"), pageContent);
        }

     
        public void GenarateCssHTMLPage(string pageContent)
        {
            if (!Directory.Exists(dir))  // if it doesn't exist, create
                Directory.CreateDirectory(dir);

            // use Path.Combine to combine 2 strings to a path
            File.WriteAllText(Path.Combine(dir, "styleWebPage.html"), pageContent);
        }

     
        public void GenarateResponsiveHTMLPage(string pageContent, string filename)
        {
            if (!Directory.Exists(dir))  // if it doesn't exist, create
                Directory.CreateDirectory(dir);

            // use Path.Combine to combine 2 strings to a path
            File.WriteAllText(Path.Combine(dir, filename), pageContent);
        }

        /// <summary>
        /// test simple html page and css applied pages with one sorting algorithm(y property sorting)
        /// </summary>
        [TestMethod]
        public void TestSimpleCssPages()
        {
            var testClass = new GeneratorTest();
           
            IGenerator generator = new SimpleCodeGenerator();
            
            IGenerator styleGenerator = new StylizedCodeGenerator();

            List<Control> controls = new List<Control>()
            {
                new Control(){  
                    Type = Core.Models.ControlType.ComboBox,
                    X = 50,
                    Y = 128
                },
                new Control(){ 
                    Type = Core.Models.ControlType.InputText,
                    X = 50,
                    Y = 100
                },
                new Button(){
                    X = 50,
                    Y = 72,
                    Value = "Click Me"

                },
                new Control(){
                    Type = Core.Models.ControlType.CheckBox,
                    Width = 28,
                    Height = 28,
                    X = 50,
                    Y = 150

                }

            };

            //without applying custom css file
            string result = generator.CreateHtmlCode(controls);
            Debug.Write(result);
            testClass.GenarateSimpleHTMLPage(result);

            //for style generator page
            string styleResult = styleGenerator.CreateHtmlCode(controls);
            Debug.Write(styleResult);
            testClass.GenarateCssHTMLPage(styleResult);
        }

        /// <summary>
        /// test responsive web page with new three of sorting algorithms for manual data
        /// </summary>
        [TestMethod]
        public void TestResponsiveManualData()
        {
            var testClass = new GeneratorTest();

            IGenerator responsiveGenerator = new ResponsiveCodeGenerator();

            SortHelper sortHelper = new SortHelper();
            
            List<Control> controls = new List<Control>()
            {
                    
                //check controls for DivAlgorithm
                new Button(){
                    X = 60,
                    Y = 80,
                    Height = 10,
                    Name = "C",
                    Value="C Button"
                },

                new Button(){
                    X = 30,
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

            //for resposive generator page
            string responsiveResult = responsiveGenerator.CreateHtmlCode(controls);
            Debug.Write(responsiveResult);
            testClass.GenarateResponsiveHTMLPage(responsiveResult, "responsiveWebPage.html");

            //test y sorting and DivAlgorithm method without x sorting
            List<Row> rowList = sortHelper.DivAlgorithm(sortHelper.SortListYProperty(controls));
            foreach (Row row in rowList)
            {
                foreach (Control item in row.Controls)
                {
                    Console.WriteLine("{0} {1} {2} {3} {4}", item.Type, item.X, item.Y, ((Button)item).Name, item.Height);
                }

            }

            Assert.AreEqual("A", ((Button)(rowList[0].Controls[0])).Name);
            Assert.AreEqual("B", ((Button)(rowList[1].Controls[0])).Name);
            Assert.AreEqual("C", ((Button)(rowList[2].Controls[0])).Name);
            Assert.AreEqual("D", ((Button)(rowList[2].Controls[1])).Name);
            Assert.AreEqual(2, rowList[2].Controls.Count);

            // test after x sorting
            List<Row> rowListX = sortHelper.SortListXProperty(rowList);
            foreach (Row row in rowListX)
            {
                foreach (Control item in row.Controls)
                {
                    Console.WriteLine("{0} {1} {2} {3} {4}", item.Type, item.X, item.Y, ((Button)item).Name, item.Height);
                }

            }

            Assert.AreEqual("A", ((Button)(rowListX[0].Controls[0])).Name);
            Assert.AreEqual("B", ((Button)(rowListX[1].Controls[0])).Name);
            Assert.AreEqual("D", ((Button)(rowListX[2].Controls[0])).Name);
            Assert.AreEqual("C", ((Button)(rowListX[2].Controls[1])).Name);
            Assert.AreEqual(2, rowListX[2].Controls.Count);

        }

        /// <summary>
        /// test responsive web page with new three of sorting algorithms for sample pages data
        /// </summary>
        [TestMethod]
        public void TestResponsiveSamplePagesData()
        {
            var testClass = new GeneratorTest();

            IGenerator responsiveGenerator = new ResponsiveCodeGenerator();

            SortHelper sortHelper = new SortHelper();

            List<Control> controls = new List<Control>()
            {

                //Check output html for sample-1.html data

                new Label(){
                    Width = 100,
                    Height = 24,
                    X = 39,
                    Y = 152,
                    Value = "Password"
                },

                new Button(){
                    Height = 24,
                    X = 88,
                    Y = 210,
                    Value = "Submit"
                },

                new Control(){
                    Type = ControlType.InputText,
                    Width = 100,
                    Height = 24,
                    X = 132,
                    Y = 102
                },

                new Label(){
                    Width = 74,
                    Height = 24,
                    X = 39,
                    Y = 60,
                    Value = "Name"
                },

                new Label(){
                    Width = 100,
                    Height = 24,
                    X = 39,
                    Y = 102,
                    Value = "Age"
                },

                new Label(){
                    Width = 127,
                    Height = 20,
                    X = 65,
                    Y = 10,
                    Value ="Sign Up"
                },

                new Control(){
                    Type = ControlType.InputText,
                    Width = 100,
                    Height = 24,
                    X = 132,
                    Y = 60
                },

                new Control(){
                    Type = ControlType.InputPassword,
                    Width = 100,
                    Height = 24,
                    X = 132,
                    Y = 152
                }
                
            };
           
            //for resposive generator page
            string responsiveResult = responsiveGenerator.CreateHtmlCode(controls);
            Debug.Write(responsiveResult);
            testClass.GenarateResponsiveHTMLPage(responsiveResult, "responsiveSample-1.html");

        }
    }
}
