using System.Collections.Generic;
using System.Drawing;
using System.Web.Hosting;
using System.Web.Http;
using Uximagine.Magicurve.Core.Models;
using Uximagine.Magicurve.Image.Processing;

namespace Uximagine.Magicurve.UI.Web.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            Processor processor = new Processor();
            string imgPath;

            switch (id)
            {
                case 2:
                    imgPath = HostingEnvironment.MapPath(
                        "~/Content/images/tri.png");
                    break;
                default:
                    imgPath = HostingEnvironment.MapPath(
                        "~/Content/images/test.png");
                    break;
            }
            Bitmap img = processor.ProcessImage(new Bitmap(imgPath));
                

            img.Save(HostingEnvironment.MapPath("~/Content/images/test2.png"));

            IControl shape = processor.Shape;

            if (id.Equals(2))
            {
                return shape.Type.ToString();
            }

            return "/Content/images/test2.png";

            
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
