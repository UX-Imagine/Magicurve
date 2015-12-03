using System;
using System.Runtime.Serialization;

namespace Uximagine.Magicurve.DataTransfer.Requests
{
    /// <summary>
    /// Training the data is requested through this class.
    /// </summary>
    [Serializable]
    [DataContract]
    public class TrainRequest
    {
        /// <summary>
        /// Gets or sets a value indicating whether [force training].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [force training]; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool ForceTraining { get; set; }
    }
}
