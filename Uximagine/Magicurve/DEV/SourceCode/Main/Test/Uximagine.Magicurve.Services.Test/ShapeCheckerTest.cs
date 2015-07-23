using System;
using NUnit.Framework;
using Uximagine.Magicurve.Image.Processing.ShapeCheckers;
using AForge;
using Uximagine.Magicurve.Core.Models;
using Should;
using System.Collections.Generic;

namespace Uximagine.Magicurve.Services.Test
{
    [TestFixture]
    public class ShapeCheckerTest
    {
        [TestCase]
        public void AdvancedShapeCheckerTest()
        {
            //Arrange
            AdvancedShapeChecker checker = new UIShapeChecker();
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
            type.ShouldEqual(ControlType.Button);
            checker.X.ShouldEqual(1);
            checker.Y.ShouldEqual(1);
            checker.Width.ShouldEqual(2);
            checker.Height.ShouldEqual(2);

            //Assert
        }
    }
}
