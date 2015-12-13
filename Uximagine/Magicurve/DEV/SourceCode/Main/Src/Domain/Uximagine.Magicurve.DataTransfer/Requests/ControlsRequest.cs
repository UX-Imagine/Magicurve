using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uximagine.Magicurve.DataTransfer.Requests
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The controls request.
    /// </summary>
    [DataContract]
    [Serializable]
    public class ControlsRequest
    {
        /// <summary>
        /// Gets or sets the file URL.
        /// </summary>
        /// <value>
        /// The file URL.
        /// </value>
        [DataMember]
        public string FileUrl { get; set; }
    }
}
