using System;
using System.Runtime.Serialization;

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
