using System.Drawing;
using Uximagine.Magicurve.Image.Processing.Common;

namespace Uximagine.Magicurve.Image.Processing.Helpers
{
    /// <summary>
    /// Edge filter helper.
    /// </summary>
    public static class EdgeFilterHelper
    {
        /// <summary>
        /// Sobels the specified image.
        /// </summary>
        /// <param name="image">
        /// The image.
        /// </param>
        /// <returns>
        /// The soble applied image.
        /// </returns>
        public static Bitmap ApplySobel(this Bitmap image)
        {
            ConvolutionMatrix martrix = new ConvolutionMatrix()
            {
                TopLeft = 1,
                TopMid = 2,
                TopRight = 1,
                MidLeft = 0,
                Pixel = 0,
                MidRight = 0,
                BottomLeft = -1,
                BottomMid = -2,
                BottomRight = -1,
                Factor = 1
            };

            return image.Convolution3By3(martrix);
        }

        /// <summary>
        /// Applies the prewitt.
        /// </summary>
        /// <param name="image">
        /// The image.
        /// </param>
        /// <returns>
        /// The filtered image.
        /// </returns>
        public static Bitmap ApplyPrewitt(this Bitmap image)
        {
            ConvolutionMatrix martrix = new ConvolutionMatrix()
            {
                TopLeft = 1,
                TopMid = 1,
                TopRight = 1,
                MidLeft = 0,
                Pixel = 0,
                MidRight = 0,
                BottomLeft = -1,
                BottomMid = -1,
                BottomRight = -1,
                Factor = 1
            };

            return image.Convolution3By3(martrix);
        }

        /// <summary>
        /// Applies the kirsh.
        /// </summary>
        /// <param name="image">
        /// The image.
        /// </param>
        /// <returns>
        /// The image.
        /// </returns>
        public static Bitmap ApplyKirsh(this Bitmap image)
        {
            ConvolutionMatrix martrix = new ConvolutionMatrix()
            {
                TopLeft = 5,
                TopMid = 5,
                TopRight = 5,
                MidLeft = -3,
                Pixel = -3,
                MidRight = -3,
                BottomLeft = -3,
                BottomMid = -3,
                BottomRight = -3,
                Factor = 1
            };

            return image.Convolution3By3(martrix);
        }
    }
}
