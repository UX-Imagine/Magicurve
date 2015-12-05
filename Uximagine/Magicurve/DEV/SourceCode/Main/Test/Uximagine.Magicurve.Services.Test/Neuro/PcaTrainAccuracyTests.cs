#region Imports

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using Should;
using Uximagine.Magicurve.Core.Models;
using Uximagine.Magicurve.Core.Shapes;
using Uximagine.Magicurve.Image.Processing;

#endregion

namespace Uximagine.Magicurve.Services.Test.Neuro
{
    /// <summary>
    ///     Neural network training tests.
    /// </summary>
    [TestFixture]
    public class PcaTrainAccuracyTests
    {
        [TestFixtureSetUp]
        public void Setup()
        {
           
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            Debug.WriteLine("Test finished.");
        }

        [TestCase(@"D:\Data\test\inputs\classify\test1.jpg",
            1,
            4,
            0,
            7,
            1,
            2,
            1
            )]
        public void TestAccuracy(
            string fileName, 
            int buttons,
            int parah, 
            int combos, 
            int texts, 
            int labels, 
            int radios, 
            int images)
        {
            Processor processor = new Processor();
            processor.ProcessImage(fileName);
            List<Control> controls = processor.Controls;

            controls.Count(t => t.Type == ControlType.Button).ShouldEqual(buttons);
            controls.Count(t => t.Type == ControlType.Button).ShouldEqual(buttons);
            controls.Count(t => t.Type == ControlType.ComboBox).ShouldEqual(combos);
            controls.Count(t => t.Type == ControlType.InputText).ShouldEqual(texts);
            controls.Count(t => t.Type == ControlType.Label).ShouldEqual(labels);
            controls.Count(t => t.Type == ControlType.RadioButton).ShouldEqual(radios);
            controls.Count(t => t.Type == ControlType.Image).ShouldEqual(images);
        }
       
    }
}