using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Uximagine.Magicurve.CodeGenerator;
using Uximagine.Magicurve.Core.Models;
using Uximagine.Magicurve.Core.Shapes;

namespace Uximagine.Magicurve.Services.Test
{
    [TestClass]
    public class EvaluationTest
    {
        private readonly string dir = @"E:\Level 4\Project\Code Generation\evaluation";

        public void GenerateSampleHTML(string pageContent, string filename)
        {
            if (!Directory.Exists(dir))  // if it doesn't exist, create
                Directory.CreateDirectory(dir);

            // use Path.Combine to combine 2 strings to a path
            File.WriteAllText(Path.Combine(dir, filename), pageContent);
        }

        [TestMethod]
        public void TestSamples1()
        {
            EvaluationTest evaluation = new EvaluationTest();

            IGenerator responsiveGenerator = new ResponsiveCodeGenerator();

            SortHelper sortHelper = new SortHelper();

            List<Control> test_1_sample = new List<Control>() { 

                new Button(){
                    X = 88,
                    Y = 210,
                    Height = 24,
                    Width = 70,
                    Value = "Button"
                },

                new Control(){
                    Type = ControlType.CheckBox,
                    X = 88,
                    Y = 310,
                    Width = 13,
                    Height = 24
                }
            };

            //for resposive generator page
            string test_1 = responsiveGenerator.CreateHtmlCode(test_1_sample, 900);
            Debug.Write(test_1);

            evaluation.GenerateSampleHTML(test_1, "test-1-sample.html");
        }

        [TestMethod]
        public void TestSamples2()
        {
            EvaluationTest evaluation = new EvaluationTest();

            IGenerator responsiveGenerator = new ResponsiveCodeGenerator();

            SortHelper sortHelper = new SortHelper();

            List<Control> test_2_sample = new List<Control>() { 

                new Button(){
                    X = 88,
                    Y = 210,
                    Height = 24,
                    Width = 70,
                    Value = "Button"
                },

                new Control(){
                    Type = ControlType.CheckBox,
                    X = 88,
                    Y = 310,
                    Width = 13,
                    Height = 24
                },

                new Control(){
                    Type = ControlType.RadioButton,
                    X = 88,
                    Y = 300,
                    Height = 13,
                    Width = 13
                },

                new Control(){
                    Type = ControlType.InputText,
                    X = 88,
                    Y = 410,
                    Height = 26,
                    Width = 174
                }
            };

            //for resposive generator page
            string test_2 = responsiveGenerator.CreateHtmlCode(test_2_sample, 900);
            Debug.Write(test_2);

            evaluation.GenerateSampleHTML(test_2, "test-2-sample.html");
        }

        [TestMethod]
        public void TestSamples3()
        {
            EvaluationTest evaluation = new EvaluationTest();

            IGenerator responsiveGenerator = new ResponsiveCodeGenerator();

            SortHelper sortHelper = new SortHelper();

            List<Control> test_3_sample = new List<Control>() { 

                new Button(){
                    X = 88,
                    Y = 210,
                    Height = 24,
                    Width = 70,
                    Value = "Button"
                },

                new Control(){
                    Type = ControlType.CheckBox,
                    X = 88,
                    Y = 310,
                    Width = 13,
                    Height = 24
                },

                new Control(){
                    Type = ControlType.RadioButton,
                    X = 88,
                    Y = 300,
                    Height = 13,
                    Width = 13
                },

                new Control(){
                    Type = ControlType.InputText,
                    X = 88,
                    Y = 410,
                    Height = 26,
                    Width = 174
                },

                new Control(){
                    Type = ControlType.ComboBox,
                    X = 180,
                    Y = 104,
                    Width = 100,
                    Height = 20
                },

                new Control(){
                    Type = ControlType.Image,
                    X = 88,
                    Y = 500,
                    Height = 40,
                    Width = 60
                },
            };

            //for resposive generator page
            string test_3 = responsiveGenerator.CreateHtmlCode(test_3_sample, 900);
            Debug.Write(test_3);

            evaluation.GenerateSampleHTML(test_3, "test-3-sample.html");
        }

        [TestMethod]
        public void TestSamples4()
        {
            EvaluationTest evaluation = new EvaluationTest();

            IGenerator responsiveGenerator = new ResponsiveCodeGenerator();

            SortHelper sortHelper = new SortHelper();

            List<Control> test_4_sample = new List<Control>() { 

                new Button(){
                    X = 88,
                    Y = 210,
                    Height = 24,
                    Width = 70,
                    Value = "Button"
                },

                new Control(){
                    Type = ControlType.CheckBox,
                    X = 88,
                    Y = 310,
                    Width = 13,
                    Height = 24
                },

                new Control(){
                    Type = ControlType.RadioButton,
                    X = 88,
                    Y = 300,
                    Height = 13,
                    Width = 13
                },

                new Control(){
                    Type = ControlType.InputText,
                    X = 88,
                    Y = 410,
                    Height = 26,
                    Width = 174
                },

                new Control(){
                    Type = ControlType.ComboBox,
                    X = 180,
                    Y = 104,
                    Width = 100,
                    Height = 20
                },

                new Control(){
                    Type = ControlType.Image,
                    X = 88,
                    Y = 500,
                    Height = 40,
                    Width = 60
                },

                new Control(){
                    Type = ControlType.TextArea,
                    X = 42,
                    Y = 500,
                    Width = 164,
                    Height = 34
                },

                new Paragraph(){
                    X = 200,
                    Y = 250,
                    Content = "This is two sentnces paragraph. Need to place correct way",
                    Width = 380
                
                }
            };

            //for resposive generator page
            string test_4 = responsiveGenerator.CreateHtmlCode(test_4_sample, 900);
            Debug.Write(test_4);

            evaluation.GenerateSampleHTML(test_4, "test-4-sample.html");
        }
    }
}
