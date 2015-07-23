﻿namespace Uximagine.Magicurve.UI.Web.Test
{
    using System.Web.Http.Controllers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Uximagine.Magicurve.UI.Web.Controllers;
    using System.Collections.Generic;
    using Uximagine.Magicurve.Core.Shapes;

    /// <summary>
    /// Values controller test.
    /// </summary>
    [TestClass]
    public class ValuesControllerTest
    {
        /// <summary>
        /// Tests the edges.
        /// </summary>
        [TestMethod]
        public void TestEdges()
        {
            //// Arrange
            var controller = new ValuesController();
            var controllerCtx = new Mock<HttpControllerContext>();
            controller.ControllerContext = controllerCtx.Object;
            
            //// Act
            List<Control> image = controller.Edges();

            //// Assert
            Assert.IsNotNull(image);
        }

        /// <summary>
        /// Gets the test.
        /// </summary>
        [TestMethod]
        public void GetTest()
        {
            //// Arrange
            var valuesController = new ValuesController();

            //// Act

            List<Control> image = valuesController.Get(1);

            //// Assert
            Assert.IsNotNull(image);
        }

    }
}
