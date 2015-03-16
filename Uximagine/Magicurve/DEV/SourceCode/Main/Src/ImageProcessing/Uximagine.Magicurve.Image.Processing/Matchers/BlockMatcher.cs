using AForge;
using AForge.Imaging;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace Uximagine.Magicurve.Image.Processing.Matchers
{
    /// <summary>
    /// The block matcher.
    /// </summary>
    public class BlockMatcher : IMatcher
    {
        /// <summary>
        /// Gets or sets the template.
        /// </summary>
        /// <value>
        /// The template.
        /// </value>
        public System.Drawing.Bitmap Template
        {
            get;
            set;
        }

        /// <summary>
        /// Matches the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>
        /// Matched results.
        /// </returns>
        public System.Drawing.Bitmap Match(System.Drawing.Bitmap sourceImage)
        {
            // collect reference points using corners detector (for example)
            SusanCornersDetector scd = new SusanCornersDetector(30, 18);
            List<IntPoint> points = scd.ProcessImage(sourceImage);

            // create block matching algorithm's instance
            ExhaustiveBlockMatching bm = new ExhaustiveBlockMatching(8, 12);
            // process images searching for block matchings
            List<BlockMatch> matches = bm.ProcessImage(sourceImage, points, this.Template);

            // draw displacement vectors
            BitmapData data = sourceImage.LockBits(
                new Rectangle(0, 0, sourceImage.Width, sourceImage.Height),
                ImageLockMode.ReadWrite, sourceImage.PixelFormat);

            foreach (BlockMatch match in matches)
            {
                // highlight the original point in source image
                Drawing.FillRectangle(data,
                    new Rectangle(match.SourcePoint.X - 1, match.SourcePoint.Y - 1, 3, 3),
                    Color.Yellow);
                // draw line to the point in search image
                Drawing.Line(data, match.SourcePoint, match.MatchPoint, Color.Red);

                // check similarity
                if (match.Similarity > 0.98f)
                {
                    // process block with high similarity somehow special
                    Drawing.FillRectangle(data,
                    new Rectangle(match.SourcePoint.X - 1, match.SourcePoint.Y - 1, 3, 3),
                    Color.Blue);
                }
            }
            sourceImage.UnlockBits(data);
            
            return sourceImage;
        }
    }
}
