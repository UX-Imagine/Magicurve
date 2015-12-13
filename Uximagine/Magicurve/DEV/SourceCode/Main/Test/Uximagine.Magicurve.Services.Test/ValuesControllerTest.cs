using Uximagine.Magicurve.Core.Models;

namespace Uximagine.Magicurve.Services.Test
{
    using System.Web.Http.Controllers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Uximagine.Magicurve.UI.Web.Controllers;
    using System.Collections.Generic;
    using Uximagine.Magicurve.Core.Shapes;
    using Uximagine.Magicurve.DataTransfer.Common;
    using Uximagine.Magicurve.DataTransfer.Requests;

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
            var controller = new ImagesController();
            var controllerCtx = new Mock<HttpControllerContext>();
            controller.ControllerContext = controllerCtx.Object;
            
            //// Act
            var image = controller.GetControls(new ControlsRequest());

            //// Assert
            Assert.IsNotNull(image);
        }

        /// <summary>
        /// Gets the test.
        /// </summary>
        [TestMethod]
        public async void GetTest()
        {
            //// Arrange
            var imagesController = new ImagesController();

            //// Act
            ImagesResult image = await imagesController.GetControls(new ControlsRequest());

            //// Assert
            Assert.IsNotNull(image);
        }

    }
}
