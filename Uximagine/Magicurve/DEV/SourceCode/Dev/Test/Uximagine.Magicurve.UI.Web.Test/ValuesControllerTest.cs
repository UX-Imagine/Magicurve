namespace Uximagine.Magicurve.UI.Web.Test
{
    using System.Web.Http.Controllers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Uximagine.Magicurve.UI.Web.Controllers;

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
            string image = controller.Edges();

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
            string image = valuesController.Get(1);

            //// Assert
            Assert.IsNotNull(image);
        }

    }
}
