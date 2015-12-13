using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uximagine.Magicurve.Core
{
    using System.Drawing;
    using System.IO;
    using System.Net.Mime;

    /// <summary>
    /// The extensions.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// The image to byte 2.
        /// </summary>
        /// <param name="image">
        /// The image.
        /// </param>
        /// <returns>
        /// The <see cref="MemoryStream"/>.
        /// </returns>
        public static MemoryStream ToStream(this Bitmap image)
        {
            MemoryStream stream = new MemoryStream();

            image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            
            stream.Close();
            return stream;
        }
    }
}
