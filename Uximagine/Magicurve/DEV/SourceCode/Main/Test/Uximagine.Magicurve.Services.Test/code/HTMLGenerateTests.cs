namespace Uximagine.Magicurve.Services.Test.Code
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using NUnit.Framework;

    using CodeGenerator;
    using Core.Shapes;
    using Magicurve.Image.Processing;

    /// <summary>
    /// The HTML generate tests.
    /// </summary>
    [TestFixture]
    public class HtmlGenerateTests
    {
        /// <summary>
        /// The Directory.
        /// </summary>
        private const string Directory = @"D:/Data/test/outputs";

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [TestFixtureSetUp]
        public void Setup()
        {
            Trainer trainer = new Trainer();
            Trainer.IsTesting = true;
            trainer.Train(50, 32);
        }

        /// <summary>
        /// Generates the HTML test.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        [TestCase(@"D:/Data/test/inputs/template4.jpg")]
        [TestCase(@"D:\Data\test\inputs\classify/test1.jpg")]
        public void GenerateHtmlTest(string fileName)
        {
            Processor processor = new Processor();
            processor.ProcessImage(fileName);
            List<Control> controls = processor.Controls;

            IGenerator generator = new ResponsiveCodeGenerator();
            string html = generator.CreateHtmlCode(processor.Controls, processor.ImageWidth, processor.ImageHeight);

            File.WriteAllText(Path.Combine(Directory, fileName.Split('/').Last().Split('.').FirstOrDefault() + ".html"), html);
        }


        /// <summary>
        /// Generates the HTML test.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        [TestCase(@"D:/Data/test/inputs/template4.jpg")]
        [TestCase(@"D:\Data\test\inputs\classify/test1.jpg")]
        public void SimpleGenerateHtmlTest(string fileName)
        {
            Processor processor = new Processor();
            processor.ProcessImage(fileName);

            IGenerator generator = new SimpleCodeGenerator();
            string html = generator.CreateHtmlCode(processor.Controls, processor.ImageWidth, processor.ImageHeight);

            File.WriteAllText(Path.Combine(Directory, fileName.Split('/').Last().Split('.').FirstOrDefault() + ".html"), html);
        }
    }
}
