namespace Uximagine.Magicurve.Image.Processing.Detectors
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;

    using AForge;

    /// <summary>
    /// The line counter.
    /// </summary>
    public class LineCounter
    {
        /// <summary>
        /// The line strut.
        /// </summary>
        public struct Line
        {
            /// <summary>
            /// Gets or sets the points.
            /// </summary>
            /// <value>
            /// The points.
            /// </value>
            public List<IntPoint> Points { get; set; }
        }

        /// <summary>
        /// Gets or sets the minimum points count.
        /// </summary>
        /// <value>
        /// The minimum points count.
        /// </value>
        public int MinimumPointsCount { get; set; } = 20;

        /// <summary>
        /// Gets or sets the minimum distance error.
        /// </summary>
        /// <value>
        /// The minimum distance error.
        /// </value>
        public int MinimumDistanceError { get; set; } = 5;

        /// <summary>
        /// Gets or sets the horizontal lines count.
        /// </summary>
        /// <value>
        /// The horizontal lines count.
        /// </value>
        public int HorizontalLinesCount => this.HorizontalLines.Count;

        /// <summary>
        /// Gets or sets the vertical lines count.
        /// </summary>
        /// <value>
        /// The vertical lines count.
        /// </value>
        public int VerticalLinesCount => this.VerticalLines.Count;

        /// <summary>
        /// Gets or sets the horizontal lines.
        /// </summary>
        /// <value>
        /// The horizontal lines.
        /// </value>
        public List<Line> HorizontalLines { get; set; }

        /// <summary>
        /// Gets or sets the horizontal lines.
        /// </summary>
        /// <value>
        /// The horizontal lines.
        /// </value>
        public List<Line> VerticalLines { get; set; }

        public LineCounter()
        {
            this.HorizontalLines = new List<Line>();
            this.VerticalLines = new List<Line>();
        }

        /// <summary>
        /// Processes the edges.
        /// </summary>
        /// <param name="points">
        /// The points.
        /// </param>
        public void ProcessEdges(List<IntPoint> points)
        {
            Line hLine = new Line { Points = new List<IntPoint> { points[0] } };
            Line vLine = new Line { Points = new List<IntPoint> { points[0] } };

            for (int i = 1; i < points.Count; i++)
            {
                if (Math.Abs(hLine.Points.Last().Y - points[i].Y) < this.MinimumDistanceError)
                {
                    hLine.Points.Add(points[i]);
                }
                else
                {
                    if (hLine.Points.Count > this.MinimumPointsCount)
                    {
                        this.HorizontalLines.Add(hLine);
                    }

                    hLine = new Line() { Points = new List<IntPoint>() { points[i] } };
                }

                if (Math.Abs(vLine.Points.Last().X - points[i].X) < this.MinimumDistanceError)
                {
                    vLine.Points.Add(points[i]);
                }
                else
                {
                    if (vLine.Points.Count > this.MinimumPointsCount)
                    {
                        this.VerticalLines.Add(hLine);
                    }

                    vLine = new Line() { Points = new List<IntPoint>() { points[i] } };
                }
            }
        }

        /// <summary>
        /// Processes the image.
        /// </summary>
        /// <param name="image">
        /// The image.
        /// </param>
        public unsafe void ProcessImage(Bitmap image)
        {
            BitmapData data = image.LockBits(
                new Rectangle(0, 0, image.Width, image.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format24bppRgb);

            int stride = data.Stride;
            int wdith = data.Width*3;
            int offset = stride - wdith;
            IntPtr scan0 = data.Scan0;

            byte* ptr = (byte*)(void*)scan0;

            for (int y = 0; y < image.Height; ++y)
            {
                byte blue = ptr[0];
                byte green = ptr[1];
                byte red = ptr[2];

                Line hLine = new Line { Points = new List<IntPoint>() };

                for (int x = 0; x < image.Width; ++x)
                {
                    if (ptr[0] == 255 && ptr[3] == 255)
                    {
                        hLine.Points.Add(new IntPoint(x, y));
                    }
                    else
                    {
                        if (hLine.Points.Count > this.MinimumPointsCount)
                        {
                            this.HorizontalLines.Add(hLine);
                        }

                        hLine = new Line() { Points = new List<IntPoint>() };
                    }

                    ptr += 3;
                }

                ptr += offset;
            }

            image.UnlockBits(data);

            for (int i = 0; i < this.HorizontalLinesCount - 1; i++)
            {
                if (this.HorizontalLines[i].Points[0].Y + 1 == this.HorizontalLines[i + 1].Points[0].Y)
                {
                    this.HorizontalLines.Remove(this.HorizontalLines[i]);
                }
            }

        }

    }
}
