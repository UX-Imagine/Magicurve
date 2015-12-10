namespace Uximagine.Magicurve.CodeGenerator.SelfHost.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    using Uximagine.Magicurve.Core.Models;
    using Uximagine.Magicurve.Core.Shapes;
    using Uximagine.Magicurve.DataTransfer.Requests;

    /// <summary>
    /// The code controller.
    /// </summary>
    [RoutePrefix("api/code")]
    public class CodeController : ApiController
    {
        /// <summary>
        /// The get code.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>
        /// The <see cref="string" />.
        /// </returns>
        [Route("get")]
        [HttpPost]
        public string GetCode(GenerateCodeRequest request)
        {
            IGenerator codeGenerator = CodeGeneratorFactory.GetCodeGenerator();
            string code = codeGenerator.CreateHtmlCode(
                request.Controls, 
                request.ImageWidth, 
                request.ImageHeight);
            return code;
        }

        /// <summary>
        /// The get fake controls.
        /// </summary>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        [Route("controls")]
        public List<Control> GetFakeControls()
        {
            List<Control> controls = new List<Control>()
                                         {
                                             new Control()
                                                 {
                                                     Width = 10,
                                                     Height = 10,
                                                     X = 10,
                                                     Y = 10,
                                                     Type = ControlType.Button
                                                 },
                                             new Control()
                                                 {
                                                     Width = 20,
                                                     Height = 30,
                                                     X = 40,
                                                     Y = 50,
                                                     Type = ControlType.CheckBox
                                                 }
                                         };
            return controls;
        }
    }
}
