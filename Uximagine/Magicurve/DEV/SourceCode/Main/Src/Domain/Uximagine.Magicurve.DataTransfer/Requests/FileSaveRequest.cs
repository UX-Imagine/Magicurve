using System.Drawing;

namespace Uximagine.Magicurve.DataTransfer.Requests
{
    using System.IO;
    using System.Runtime.Serialization;
    using System.Web;

    /// <summary>
    /// The file save request.
    /// </summary>
    [DataContract]
    public class FileSaveRequest
    {
        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>
        /// The image.
        /// </value>
        [DataMember]
        public HttpPostedFileBase Image { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [DataMember]
        public string Name { get; set; }

    }
}