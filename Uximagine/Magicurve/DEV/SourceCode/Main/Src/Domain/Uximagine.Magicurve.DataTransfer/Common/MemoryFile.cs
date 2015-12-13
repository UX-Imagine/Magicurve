namespace Uximagine.Magicurve.DataTransfer.Common
{
    using System;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Web;

    /// <summary>
    /// The memory file.
    /// </summary>
    [Serializable]
    [DataContract]
    public class MemoryFile : HttpPostedFileBase
    {
        /// <summary>
        /// The stream
        /// </summary>
        private readonly Stream stream;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryFile"/> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="fileName">Name of the file.</param>
        public MemoryFile(Stream stream, string contentType, string fileName)
        {
            this.stream = stream;
            this.ContentType = contentType;
            this.FileName = fileName;
        }

        /// <summary>
        /// When overridden in a derived class, gets the size of an uploaded file, in bytes.
        /// </summary>
        public override int ContentLength
        {
            get
            {
                return (int)stream.Length;
            }
        }

        /// <summary>
        /// When overridden in a derived class, gets the MIME content type of an uploaded file.
        /// </summary>
        public override string ContentType { get; }

        /// <summary>
        /// When overridden in a derived class, gets the fully qualified name of the file on the client.
        /// </summary>
        public override string FileName { get; }

        /// <summary>
        /// When overridden in a derived class, gets a <see cref="T:System.IO.Stream" /> object that points to an uploaded file to prepare for reading the contents of the file.
        /// </summary>
        public override Stream InputStream => this.stream;

        /// <summary>
        /// Saves as.
        /// </summary>
        /// <param name="filename">The filename.</param>
        public override void SaveAs(string filename)
        {
            using (FileStream file = File.Open(filename, FileMode.CreateNew)) stream.CopyTo(file);
        }
    }
}