﻿#region Imports
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Accord.Imaging;
using Accord.Imaging.Filters;
using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using NUnit.Framework;
using Should;
using Uximagine.Magicurve.Core.Models;
using Uximagine.Magicurve.Core.Shapes;
using Uximagine.Magicurve.Image.Processing.Detectors;
using Uximagine.Magicurve.Image.Processing.Helpers; 
#endregion

namespace Uximagine.Magicurve.Services.Test.Image
{
    /// <summary>
    ///     The edge detection algorithm tests.
    /// </summary>
    [TestFixture]
    public class EdgeTests
    {
        /// <summary>
        ///     Tests the blobs.
        /// </summary>
        [TestCase(@"D:/Data/test/inputs/template4.jpg", 2, 1, 11, 0)]
        [TestCase(@"D:/Data/test/inputs/image_03.jpg", 0, 0, 1, 0)]
        [TestCase(@"D:/Data/test/inputs/template3.jpg", 0, 0, 0, 1)]
        [TestCase(@"D:/Data/test/inputs/combo_10.jpg", 0, 0, 0, 1)]
        public void TestBLobs(string fileName, int radioCount, int iFrameCount, int buttonCount, int comboCount = 0)
        {
            Bitmap image = new Bitmap(fileName); // Lena's picture

            image = Grayscale.CommonAlgorithms.BT709.Apply(image);

            image.Save(@"D:/Data/test/outputs/grayscale.jpg");

            Threshold threshold = new Threshold();
            threshold.ApplyInPlace(image);

            image.Save(@"D:/Data/test/outputs/threshold.jpg");

            Median median = new Median();
            median.ApplyInPlace(image);

            //var correctFormatImage = image.ConvertToFormat(PixelFormat.Format24bppRgb);

            Invert invert = new Invert();
            invert.ApplyInPlace(image);
            image.Save(@"D:/Data/test/outputs/invert.jpg");

            IBlobDetector blobDetector = new HullBlobDetector();
            blobDetector.ProcessImage(image);

            Bitmap result = blobDetector.GetImage();
            result.Save(@"D:/Data/test/outputs/blobs.jpg");

            List<Control> controls = blobDetector.GetShapes();
            controls.Where(t => t.Type != ControlType.None).ToList().ForEach(
                t => Debug.WriteLine("{0} {1} {2} {3} {4}", t.Type, t.Width, t.Height, t.X, t.Y));

            controls.Count(t => t.Type == ControlType.RadioButton).ShouldEqual(radioCount);
            controls.Count(t => t.Type == ControlType.Iframe).ShouldEqual(iFrameCount);
            controls.Count(t => t.Type == ControlType.Button).ShouldEqual(buttonCount);
        }

        /// <summary>
        ///     Tests the hough transform.
        /// </summary>
        [TestCase(@"D:/Data/test/inputs/template.jpg")]
        public void TestHoughTransformBasic(string fileName)
        {
            Bitmap image = new Bitmap(fileName); // Lena's picture

            image = Grayscale.CommonAlgorithms.BT709.Apply(image);

            Threshold threshold = new Threshold();
            threshold.ApplyInPlace(image);

            Median median = new Median();
            median.ApplyInPlace(image);

            //var correctFormatImage = image.ConvertToFormat(PixelFormat.Format24bppRgb);

            Invert invert = new Invert();
            invert.ApplyInPlace(image);

            Bitmap result = image.HorizontalEdges();
            result.Save(@"D:/Data/test/outputs/hEdges" + fileName.Split('/').Last());

            HoughLineTransformation lineTransform = new HoughLineTransformation();
            // apply Hough line transofrm
            lineTransform.ProcessImage(result);
            Bitmap houghLineImage = lineTransform.ToBitmap();
            houghLineImage.Save(@"D:/Data/test/outputs/hough" + fileName.Split('/').Last());

            Debug.WriteLine("Lines {0}", lineTransform.GetLinesByRelativeIntensity(.5).Count());
            Debug.WriteLine("Lines {0}", lineTransform.GetLinesByRelativeIntensity(.75).Count());

            foreach (HoughLine line in lineTransform.GetLinesByRelativeIntensity(0.75))
            {
                Debug.WriteLine(line.Theta);
            }
        }

        /// <summary>
        ///     Tests the erroson.
        /// </summary>
        [TestCase(@"D:/Data/test/inputs/template4.jpg")]
        public void TestCrop(string fileName)
        {
            Bitmap image = new Bitmap(fileName); // Lena's picture

            image = Grayscale.CommonAlgorithms.BT709.Apply(image);

            image.Save(@"D:/Data/test/outputs/gray_" + fileName.Split('/').Last());

            Threshold threshold = new Threshold();
            threshold.ApplyInPlace(image);

            image.Save(@"D:/Data/test/outputs/thresh_" + fileName.Split('/').Last());

            Median median = new Median();
            median.ApplyInPlace(image);

            Invert invert = new Invert();
            invert.ApplyInPlace(image);

            IBlobDetector blobDetector = new HullBlobDetector();
            blobDetector.ProcessImage(image);

            Bitmap result = blobDetector.GetImage();
            List<Control> controls = blobDetector.GetShapes();

            result.Save(@"D:/Data/test/outputs/blob_" + fileName.Split('/').Last());

            controls.Where(t => t.Type != ControlType.None).ToList().ForEach(
                t => Debug.WriteLine("{0} {1} {2} {3} {4}", t.Type, t.Width, t.Height, t.X, t.Y));

            int index = 0;

            controls.Where(t => t.Type == ControlType.Button && t.Width > 25 && t.Height > 25).ToList().ForEach(
                t =>
                {
                    Bitmap cropped = image.Crop(t.EdgePoints);
                    cropped.Save(@"D:/Data/test/outputs/croped_" + index++ + fileName.Split('/').Last());
                });


            Debug.WriteLine(controls);
        }

        /// <summary>
        ///     Tests the horizontal lines.
        /// </summary>
        [TestCase(@"D:/Data/test/inputs/template4.jpg")]
        public void TestHorizontalLines(string fileName)
        {
            Bitmap image = new Bitmap(fileName); // Lena's picture

            image = Grayscale.CommonAlgorithms.BT709.Apply(image);

            image.Save(@"D:/Data/test/outputs/gray_" + fileName.Split('/').Last());

            Threshold threshold = new Threshold();
            threshold.ApplyInPlace(image);

            image.Save(@"D:/Data/test/outputs/thresh_" + fileName.Split('/').Last());

            Median median = new Median();
            median.ApplyInPlace(image);

            Invert invert = new Invert();
            invert.ApplyInPlace(image);

            IBlobDetector blobDetector = new HullBlobDetector();
            blobDetector.ProcessImage(image);

            Bitmap result = blobDetector.GetImage();
            List<Control> controls = blobDetector.GetShapes();

            result.Save(@"D:/Data/test/outputs/blob_" + fileName.Split('/').Last());

            controls.Where(t => t.Type != ControlType.None).ToList().ForEach(
                t => Debug.WriteLine("{0} {1} {2} {3} {4}", t.Type, t.Width, t.Height, t.X, t.Y));

            Bitmap cropped =
                image.Crop(
                    controls.Where(t => t.Type == ControlType.Button && t.Width > 100 && t.Height > 100).ToList()[0]
                        .EdgePoints);
            cropped.Save(@"D:/Data/test/outputs/croped_" + fileName.Split('/').Last());

            ((Bitmap) cropped.Clone()).HorizontalEdges().Save(@"D:/Data/test/outputs/h_" + fileName.Split('/').Last());

            short[,] se =
            {
                {0, 0, 0},
                {1, 1, 1},
                {0, 0, 0}
            };

            // create filter
            HitAndMiss filter = new HitAndMiss(se, HitAndMiss.Modes.HitAndMiss);
            // apply the filter

            image = Grayscale.CommonAlgorithms.BT709.Apply(cropped);
            filter.ApplyInPlace(image);
            image.Save(@"D:/Data/test/outputs/hitNmiss_" + fileName.Split('/').Last());
        }

        /// <summary>
        ///     Tests the horizontal lines.
        /// </summary>
        [TestCase(@"D:/Data/test/inputs/template4.jpg", 100, ControlType.Button)]
        [TestCase(@"D:/Data/test/inputs/image_05.jpg", 25, ControlType.Button)]
        [TestCase(@"D:/Data/test/inputs/image_06.jpg", 25, ControlType.Button)]
        [TestCase(@"D:/Data/test/inputs/image_07.jpg", 25, ControlType.Button)]
        [TestCase(@"D:/Data/test/inputs/radio_06.jpg", 25, ControlType.RadioButton)]
        [TestCase(@"D:/Data/test/inputs/combo_10.jpg", 25, ControlType.ComboBox)]
        [TestCase(@"D:/Data/test/inputs/combo_06.jpg", 25, ControlType.ComboBox)]
        [TestCase(@"D:/Data/test/inputs/combo_07.jpg", 25, ControlType.ComboBox)]
        [TestCase(@"D:/Data/test/inputs/combo_08.jpg", 25, ControlType.ComboBox)]
        [TestCase(@"D:/Data/test/inputs/combo_09.jpg", 25, ControlType.ComboBox)]
        public void TestHoughLines(string fileName, int size, ControlType type)
        {
            Bitmap image = new Bitmap(fileName); // Lena's picture

            image = Grayscale.CommonAlgorithms.BT709.Apply(image);

            Threshold threshold = new Threshold();
            threshold.ApplyInPlace(image);

            Median median = new Median();
            median.ApplyInPlace(image);

            Invert invert = new Invert();
            invert.ApplyInPlace(image);

            IBlobDetector blobDetector = new HullBlobDetector();
            blobDetector.ProcessImage(image);

            List<Control> controls = blobDetector.GetShapes();
            controls.Where(t => t.Type != ControlType.None).ToList().ForEach(
                t => Debug.WriteLine("{0} {1} {2} {3} {4}", t.Type, t.Width, t.Height, t.X, t.Y));

            Bitmap cropped = image.Crop(controls.Where(t => t.Width > size && t.Height > size).ToList()[0].EdgePoints);
            cropped.Save(@"D:/Data/test/outputs/croped_" + fileName.Split('/').Last());

            Bitmap horizontal = ((Bitmap) cropped.Clone()).HorizontalEdges();
            horizontal.Save(@"D:/Data/test/outputs/h_" + fileName.Split('/').Last());

            Bitmap vertical = ((Bitmap) cropped.Clone()).VerticalEdges();
            vertical.Save(@"D:/Data/test/outputs/v_" + fileName.Split('/').Last());

            image = Grayscale.CommonAlgorithms.BT709.Apply(horizontal);

            HoughLineTransformation hlt = new HoughLineTransformation();
            hlt.ProcessImage(image);
            Debug.WriteLine("horizontal lines : {0}", hlt.GetLinesByRelativeIntensity(.5).Count());

            image = Grayscale.CommonAlgorithms.BT709.Apply(vertical);

            HoughLineTransformation hlt2 = new HoughLineTransformation();
            hlt2.ProcessImage(image);

            Debug.WriteLine("vertical lines : {0}", hlt2.GetLinesByRelativeIntensity(.5).Count());
            foreach (HoughLine line in hlt2.GetLinesByRelativeIntensity(.5))
            {
                Debug.WriteLine(
                    "theta:{0}, intensity: {1}, radius : {2}, relative intensity: {3} ",
                    line.Theta,
                    line.Intensity,
                    line.Radius,
                    line.RelativeIntensity);
            }
        }

        /// <summary>
        ///     Tests the hough theta.
        /// </summary>
        /// <param name="fileName">
        ///     Name of the file.
        /// </param>
        [TestCase(@"D:/Data/test/inputs/template4.jpg")]
        public void TestHoughTheta(string fileName)
        {
            Bitmap image = GetCropped(fileName, 100);
            image = Grayscale.CommonAlgorithms.BT709.Apply(image);

            HoughLineTransformation hlt = new HoughLineTransformation();
            hlt.ProcessImage(image);

            Debug.WriteLine(hlt.GetLinesByRelativeIntensity(.5).Count());
            foreach (HoughLine line in hlt.GetLinesByRelativeIntensity(.4))
            {
                Debug.WriteLine(
                    "theta : {0}",
                    line.Theta
                    );
            }
        }

        /// <summary>
        ///     Gets the cropped.
        /// </summary>
        /// <param name="fileName">
        ///     Name of the file.
        /// </param>
        /// <param name="minSize">
        ///     The size.
        /// </param>
        /// <returns>
        ///     The cropped image.
        /// </returns>
        private static Bitmap GetCropped(string fileName, int minSize)
        {
            Bitmap image = new Bitmap(fileName); // Lena's picture

            image = Grayscale.CommonAlgorithms.BT709.Apply(image);

            Threshold threshold = new Threshold();
            threshold.ApplyInPlace(image);

            Median median = new Median();
            median.ApplyInPlace(image);

            Invert invert = new Invert();
            invert.ApplyInPlace(image);

            IBlobDetector blobDetector = new HullBlobDetector();
            blobDetector.ProcessImage(image);

            List<Control> controls = blobDetector.GetShapes();
            controls.Where(t => t.Type != ControlType.None).ToList().ForEach(
                t => Debug.WriteLine("{0} {1} {2} {3} {4}", t.Type, t.Width, t.Height, t.X, t.Y));

            Bitmap cropped = image.Crop(controls.Where(
                t => t.Width > minSize && t.Height > minSize).ToList()[0].EdgePoints);
            cropped.Save(@"D:/Data/test/outputs/croped_" + fileName.Split('/').Last());

            return cropped;
        }

        /// <summary>
        ///     Gets the cropped.
        /// </summary>
        /// <param name="fileName">
        ///     Name of the file.
        /// </param>
        /// <param name="minSize">
        ///     The size.
        /// </param>
        /// <returns>
        ///     The cropped image.
        /// </returns>
        private static IEnumerable<Bitmap> GetCroppedAll(string fileName, int minSize)
        {
            Bitmap image = new Bitmap(fileName); // Lena's picture

            image = Grayscale.CommonAlgorithms.BT709.Apply(image);

            Threshold threshold = new Threshold();
            threshold.ApplyInPlace(image);

            Median median = new Median();
            median.ApplyInPlace(image);

            Invert invert = new Invert();
            invert.ApplyInPlace(image);

            IBlobDetector blobDetector = new HullBlobDetector();
            blobDetector.ProcessImage(image);

            List<Control> controls = blobDetector.GetShapes();
            controls.Where(t => t.Type != ControlType.None).ToList().ForEach(
                t => Debug.WriteLine("{0} {1} {2} {3} {4}", t.Type, t.Width, t.Height, t.X, t.Y));
            List<Bitmap> cropped = new List<Bitmap>();

            var index = 0;

            controls.Where(
                t => t.Width > minSize && t.Height > minSize).ToList().ForEach(
                    t =>
                    {
                        Bitmap crop = image.Crop(t.EdgePoints);
                        cropped.Add(crop);
                        crop.Save(@"D:/Data/test/outputs/croped_" + index++ + fileName.Split('/').Last());
                    });

            return cropped;
        }

        [TestCase(@"D:/Data/test/inputs/image_06.jpg")]
        public void CornerDetect(string fileName)
        {
            Bitmap image = GetCropped(fileName, 100);

            // create corners detector's instance
            SusanCornersDetector scd = new SusanCornersDetector();
            // process image searching for corners
            List<IntPoint> corners = scd.ProcessImage(image);

            CornersMarker filter = new CornersMarker(scd, Color.Red);
            // apply the filter
            filter.ApplyInPlace(image);

            image.Save(@"D:/Data/test/outputs/corners_" + fileName.Split('/').Last());

            // process points
            foreach (IntPoint corner in corners)
            {
                Debug.WriteLine("{0}, {1}", corner.X, corner.Y);
            }
        }

        [TestCase(@"D:/Data/test/inputs/image_06.jpg", 1f, 100)]
        [TestCase(@"D:/Data/test/inputs/button/button_06.jpg", 4.2f, 50)]
        [TestCase(@"D:/Data/test/inputs/image_06.jpg", 2f, 100)]
        [TestCase(@"D:/Data/test/inputs/image_06.jpg", 4f, 100)]
        [TestCase(@"D:/Data/test/inputs/combo_06.jpg", 4f, 100)]
        [TestCase(@"D:/Data/test/inputs/mix.jpg", 4f, 75)]
        [TestCase(@"D:/Data/test/inputs/combo_12.jpg", 4.2f, 100)]
        [TestCase(@"D:/Data/test/inputs/image_06.jpg", 10f, 100)]
        public void HarrisCornerDetect(string fileName, float multiflier, int minSize)
        {
            Bitmap image = GetCropped(fileName, minSize);

            // create corners detector's instance
            HarrisCornersDetector hcd = new HarrisCornersDetector();
            //hcd.Threshold = hcd.Threshold*multiflier;
            hcd.K = hcd.K*multiflier;
            // process image searching for corners
            List<IntPoint> corners = hcd.ProcessImage(image);

            CornersMarker filter = new CornersMarker(hcd, Color.Red);
            // apply the filter
            filter.ApplyInPlace(image);

            image.Save(@"D:/Data/test/outputs/h_corners_" + multiflier + fileName.Split('/').Last());

            // process points
            foreach (IntPoint corner in corners)
            {
                Debug.WriteLine("{0}, {1}", corner.X, corner.Y);
            }
        }

        [TestCase(@"D:/Data/test/inputs/mix.jpg", 4.3f, 25)]
        [TestCase(@"D:/Data/test/inputs/Image.jpeg", 4.3f, 25)]
        public void HarrisCornerDetectAll(string fileName, float multiflier, int minSize)
        {
            IEnumerable<Bitmap> images = GetCroppedAll(fileName, minSize);

            // create corners detector's instance
            HarrisCornersDetector hcd = new HarrisCornersDetector();
            //hcd.Threshold = hcd.Threshold*multiflier;
            hcd.K = hcd.K*multiflier;
            // process image searching for corners
            var index = 0;
            foreach (Bitmap image in images)
            {
                List<IntPoint> corners = hcd.ProcessImage(image);

                CornersMarker filter = new CornersMarker(hcd, Color.Red);
                // apply the filter
                filter.ApplyInPlace(image);

                image.Save(@"D:/Data/test/outputs/h_corners_" + index++ + "_" + multiflier + fileName.Split('/').Last());

                // process points
                foreach (IntPoint corner in corners)
                {
                    Debug.WriteLine("{0}, {1}", corner.X, corner.Y);
                }
            }
        }

        /// <summary>
        ///     Cannies the test.
        /// </summary>
        [Test]
        public void CannyTest()
        {
            Bitmap image = new Bitmap(@"D:/Data/test/inputs/template.jpg"); // Lena's picture

            image.SetResolution(60f, 60f);

            image = new Bitmap(image, 1024, 768);

            image = Grayscale.CommonAlgorithms.BT709.Apply(image);

            image.Save(@"D:/Data/test/outputs/grayscale.jpg");

            Threshold threshold = new Threshold(79);
            threshold.ApplyInPlace(image);

            image.Save(@"D:/Data/test/outputs/threshold.jpg");

            //Dilatation dilatation = new Dilatation();
            // apply the filter
            //dilatation.ApplyInPlace(image);
            //image.Save("dilate.jpg");

            Median median = new Median(25);
            median.ApplyInPlace(image);
            image.Save(@"D:/Data/test/outputs/median.jpg");

            Bitmap overlay = AForge.Imaging.Image.Clone(image).ConvertToFormat(PixelFormat.Format24bppRgb);

            EuclideanColorFiltering euclidean = new EuclideanColorFiltering
            {
                CenterColor = new RGB(Color.White), //Pure White
                Radius = 0, //Increase this to allow off-whites
                FillColor = new RGB(Color.White) //Replacement Colour
            };

            euclidean.ApplyInPlace(overlay);

            overlay.Save(@"D:/Data/test/outputs/overlay.jpg");

            Merge merge = new Merge(overlay);
            Bitmap mergeImage = merge.Apply(image.ConvertToFormat(PixelFormat.Format24bppRgb));
            mergeImage.Save(@"D:/Data/test/outputs/merged.jpg");

            // Skeletonization.
            SimpleSkeletonization filter = new SimpleSkeletonization();
            Bitmap skeliton = filter.Apply(image);
            skeliton.Save(@"D:/Data/test/outputs/skeliton.jpg");

            Closing closing = new Closing();
            closing.ApplyInPlace(skeliton);
            skeliton.Save(@"D:/Data/test/outputs/closing.jpg");

            // Create a new Canny's edge detector:
            CannyEdgeDetector edgeDetector = new CannyEdgeDetector(20, 100, 1.4);
            // Compute the image edges
            Bitmap edges = edgeDetector.Apply(image);
            edges.Save(@"D:/Data/test/outputs/canny.jpg");

            SobelEdgeDetector sobel = new SobelEdgeDetector();
            Bitmap sobelImage = sobel.Apply(image);
            sobelImage.Save(@"D:/Data/test/outputs/sobel.jpg");
        }

        /// <summary>
        ///     Kirsches the test.
        /// </summary>
        [Test]
        public void KirschTest()
        {
            Bitmap image = new Bitmap(@"D:/Data/test/inputs/template.jpg"); // Lena's picture

            // Create a new Kirsch's edge detector:
            KirschEdgeDetector kirsch = new KirschEdgeDetector();

            // Compute the image edges
            Bitmap edges = kirsch.Apply(image);

            // Save File
            edges.Save(@"D:/Data/test/outputs/kirsch_edges.jpg");
        }

        /// <summary>
        ///     Tests the b lobs files.
        /// </summary>
        [Test]
        public void TestBLobsFiles()
        {
            string[] files = Directory.GetFiles(@"D:/Data/test/inputs");
            var index = 0;

            foreach (var file in files)
            {
                Bitmap image = new Bitmap(file); // Lena's picture

                IBlobDetector blobDetector = new BlobDetector();

                image = Grayscale.CommonAlgorithms.BT709.Apply(image);

                image.Save(@"D:/Data/test/outputs/grayscale_" + index + ".jpg");

                Threshold threshold = new Threshold();
                threshold.ApplyInPlace(image);

                image.Save(@"D:/Data/test/outputs/threshold_" + index + ".jpg");

                Median median = new Median();
                median.ApplyInPlace(image);

                Bitmap correctFormatImage = image.ConvertToFormat(PixelFormat.Format24bppRgb);

                Invert invert = new Invert();
                invert.ApplyInPlace(correctFormatImage);

                blobDetector.ProcessImage(correctFormatImage);

                Bitmap result = blobDetector.GetImage();
                List<Control> controls = blobDetector.GetShapes();
                Debug.WriteLine(controls);

                result.Save(@"D:/Data/test/outputs/blobs_" + index + ".jpg");
                index++;
            }
        }

        /// <summary>
        ///     Tests the erroson.
        /// </summary>
        [Test]
        public void TestErroson()
        {
            Bitmap image = new Bitmap(@"D:/Data/test/inputs/template3.jpg"); // Lena's picture

            image = Grayscale.CommonAlgorithms.BT709.Apply(image);

            image.Save(@"D:/Data/test/outputs/grayscale.jpg");

            Threshold threshold = new Threshold();
            threshold.ApplyInPlace(image);

            image.Save(@"D:/Data/test/outputs/threshold.jpg");

            Median median = new Median();
            median.ApplyInPlace(image);

            Erosion erosion = new Erosion();
            erosion.ApplyInPlace(image);
            image.Save(@"D:/Data/test/outputs/erosion.jpg");

            Bitmap correctFormatImage = image.ConvertToFormat(PixelFormat.Format24bppRgb);

            Invert invert = new Invert();
            invert.ApplyInPlace(correctFormatImage);

            IBlobDetector blobDetector = new HullBlobDetector();
            blobDetector.ProcessImage(correctFormatImage);

            Bitmap result = blobDetector.GetImage();
            List<Control> controls = blobDetector.GetShapes();
            Debug.WriteLine(controls);

            result.Save(@"D:/Data/test/outputs/blobs.jpg");
        }

        /// <summary>
        ///     Tests the fill holes.
        /// </summary>
        [Test]
        public void TestFillHoles()
        {
            Bitmap image = new Bitmap(@"D:/Data/test/inputs/image_01.jpg"); // Lena's picture

            //image.SetResolution(60f, 60f);

            //image = new Bitmap(image, 1024, 768);

            image = Grayscale.CommonAlgorithms.BT709.Apply(image);

            image.Save(@"D:/Data/test/outputs/grayscale.jpg");

            Threshold threshold = new Threshold();
            threshold.ApplyInPlace(image);

            image.Save(@"D:/Data/test/outputs/threshold.jpg");

            Median median = new Median();
            median.ApplyInPlace(image);
            image.Save(@"D:/Data/test/outputs/median.jpg");

            FillHoles filholes = new FillHoles
            {
                MaxHoleHeight = 20,
                MaxHoleWidth = 20,
                CoupledSizeFiltering = false
            };

            // apply the filter
            Bitmap filledImage = filholes.Apply(image);
            filledImage.Save(@"D:/Data/test/outputs/filled.jpg");
        }

        /// <summary>
        ///     Tests the hough transform.
        /// </summary>
        [Test]
        public void TestHoughTransform()
        {
            Bitmap image = new Bitmap(@"D:/Data/test/inputs/image_02.jpg"); // Lena's picture

            image = Grayscale.CommonAlgorithms.BT709.Apply(image);

            image.Save(@"D:/Data/test/outputs/grayscale.jpg");

            Threshold threshold = new Threshold();
            threshold.ApplyInPlace(image);

            image.Save(@"D:/Data/test/outputs/threshold.jpg");

            Median median = new Median();
            median.ApplyInPlace(image);

            Bitmap correctFormatImage = image.ConvertToFormat(PixelFormat.Format24bppRgb);

            Invert invert = new Invert();
            invert.ApplyInPlace(correctFormatImage);

            HoughLineTransformation lineTransform = new HoughLineTransformation();

            // apply Hough line transofrm
            lineTransform.ProcessImage(image);
            Bitmap houghLineImage = lineTransform.ToBitmap();
            houghLineImage.Save(@"D:/Data/test/outputs/hough.jpg");

            foreach (HoughLine line in lineTransform.GetLinesByRelativeIntensity(0.75))
            {
                Debug.WriteLine(line.Theta);
            }
        }
    }
}