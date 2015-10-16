using System;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using Uximagine.Magicurve.DataTransfer.Requests;

namespace Uximagine.Magicurve.Services.Test
{
    /// <summary>
    /// The file service tests.
    /// </summary>
    [TestClass]
    public class FileServiceTests
    {
        /// <summary>
        /// Saves the test.
        /// </summary>
        [TestMethod]
        public void SaveTest()
        {
            IFileService service = ServiceFactory.GetFileService();
            var result = service.SaveFile(new FileSaveRequest()
            {
                Image = new Bitmap("testimage")
            });

            result.Id.ShouldBeType(typeof(Guid));
        }
    }
}