namespace Uximagine.Magicurve.Services.Test
{
    using Uximagine.Magicurve.DataTransfer.Requests;
    using Uximagine.Magicurve.DataTransfer.Responses;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Uximagine.Magicurve.Services;
   
    using Moq;

    /// <summary>
    /// The processing service test.
    /// </summary>
    [TestClass]
    public class ProcessingServiceTest
    {
        /// <summary>
        /// The service
        /// </summary>
        private Mock<IProcessingService> service;

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            this.service = new Mock<IProcessingService>();
            var request = new Mock<ProcessRequestDto>();
            var response = new Mock<ProcessResponseDto>();
            this.service.Setup(ser => ser.ProcessImage(request.Object))
                .Returns(response.Object);
        }

        /// <summary>
        /// Gets the edged image URL.
        /// </summary>
        [TestMethod]
        public void GetEdgedImageUrl()
        {
            //// Arrange
            var request = new ProcessRequestDto() { ImagePath = "image" };

            //// Act
            var result = this.service.Object.ProcessImage(request);

            //// Assert
            Assert.IsNotNull((result));
        }
    }
}
