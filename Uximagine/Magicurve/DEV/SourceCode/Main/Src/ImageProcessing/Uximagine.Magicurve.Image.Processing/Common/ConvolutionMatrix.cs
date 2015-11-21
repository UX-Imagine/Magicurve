namespace Uximagine.Magicurve.Image.Processing.Common
{
    /// <summary>
    /// The convolution matrix.
    /// </summary>
    public class ConvolutionMatrix
    {
        public int TopLeft { get; set; } = 0;
        public int TopMid { get; set; } = 0;
        public int TopRight { get; set; } = 0;
        public int MidLeft { get; set; } = 0;
        public int Pixel { get; set; } = 1;
        public int MidRight { get; set; } = 0;
        public int BottomLeft { get; set; } = 0;
        public int BottomMid { get; set; } = 0;
        public int BottomRight { get; set; } = 0;
        public int Factor { get; set; } = 1;
        public int Offset { get; set; } = 0;
        
        /// <summary>
        /// Sets all pixels.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        public void SetAll(int value)
        {
            TopLeft = TopMid = TopRight = value;
            MidLeft = Pixel = MidRight = value;
            BottomLeft = BottomMid = BottomRight = value;
        }
    }
}
