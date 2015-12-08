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
        /// The directory.
        /// </summary>
        private string directory = @"E:/Data";

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
        public void GenerateHtmlTest(string fileName)
        {
            Processor processor = new Processor();
            processor.ProcessImage(fileName);
            List<Control> controls = processor.Controls;

            IGenerator generator = new ResponsiveCodeGenerator();
            string html = generator.CreateHtmlCode(processor.Controls, processor.ImageWidth);

            File.WriteAllText(Path.Combine(this.directory, fileName.Split('/').Last().Split('.').FirstOrDefault() + ".html"), html);
        }
    }
}
