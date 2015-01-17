using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Hosting;
using System.Web.Http;
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
            Bitmap img = Processor.ProcessImage(
                new System.Drawing.Bitmap(HostingEnvironment.MapPath("~/Content/images/test.png")));
            img.Save(HostingEnvironment.MapPath("~/Content/images/test2.png"));
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
