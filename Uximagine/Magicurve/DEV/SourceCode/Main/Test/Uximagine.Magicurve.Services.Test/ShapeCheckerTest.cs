using NUnit.Framework;
using Uximagine.Magicurve.Image.Processing.ShapeCheckers;
using Accord;
using Uximagine.Magicurve.Core.Models;
using Should;
using System.Collections.Generic;
using AForge;

namespace Uximagine.Magicurve.Services.Test
{
    /// <summary>
    /// The shape checker test.
    /// </summary>
    [TestFixture]
    public class ShapeCheckerTest
    {
        /// <summary>
        /// Advanceds the shape checker test.
        /// </summary>
        [TestCase]
        public void AdvancedShapeCheckerTest()
        {
            //Arrange
            var checker = new UiShapeChecker();
            List<IntPoint> edgePoints = new List<IntPoint>
            {
                new IntPoint(1,1),
                new IntPoint(2,1),
                new IntPoint(3,1),
                new IntPoint(3,2),
                new IntPoint(3,3),
                new IntPoint(1,3)
            };

            //Act

            ControlType type = checker.GetControlType(edgePoints);

            //Assert
            type.ShouldEqual(ControlType.Button);
            checker.X.ShouldEqual(1);
            checker.Y.ShouldEqual(1);
            checker.Width.ShouldEqual(2);
            checker.Height.ShouldEqual(2);
        }
    }
}
