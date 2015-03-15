using AForge.Imaging;
using System.Drawing;
using System.Drawing.Imaging;

namespace Uximagine.Magicurve.Image.Processing.Detectors
{
    /// <summary>
    /// The template detector.
    /// </summary>
    public class TemplateDetector : IDetector
    {
        /// <summary>
        /// Gets or sets the template image.
        /// </summary>
        /// <value>
        /// The template image.
        /// </value>
        public Bitmap TemplateImage { get; set; }

        /// <summary>
        /// Detects the specified source image.
        /// </summary>
        /// <param name="sourceImage">
        /// The source image.
        /// </param>
        /// <returns>
        /// The detected templates.
        /// </returns>
        public Bitmap Detect(Bitmap sourceImage)
        {
            // create template matching algorithm's instance
            ExhaustiveTemplateMatching tm = new ExhaustiveTemplateMatching(0.9f);
            // find all matchings with specified above similarity
            TemplateMatch[] matchings = tm.ProcessImage(sourceImage, TemplateImage);
            // highlight found matchings
            BitmapData data = sourceImage.LockBits(
                new Rectangle(0, 0, sourceImage.Width, sourceImage.Height),
                ImageLockMode.ReadWrite, sourceImage.PixelFormat);
            foreach (TemplateMatch m in matchings)
            {
                Drawing.Rectangle(data, m.Rectangle, Color.White);
                // do something else with matching
            }
            sourceImage.UnlockBits(data);

            return sourceImage;
        }
    }
}
