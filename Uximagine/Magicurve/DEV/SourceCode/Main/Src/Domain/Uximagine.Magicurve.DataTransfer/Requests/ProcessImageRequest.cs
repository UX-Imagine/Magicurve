using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Uximagine.Magicurve.DataTransfer.Requests
{
    [DataContract]
    [Serializable]
    public class ProcessImageRequest
    {
        [DataMember]
        public string ImagePath { get; set; }

        [DataMember]
        public string ServerPath { get; set; }
    }
}
