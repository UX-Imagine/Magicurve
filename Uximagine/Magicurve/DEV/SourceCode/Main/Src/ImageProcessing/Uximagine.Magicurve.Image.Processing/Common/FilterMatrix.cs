namespace Uximagine.Magicurve.Image.Processing.Common
{
    /// <summary>
    /// The filter matrices.
    /// </summary>
    public class FilterMatrix
    {
        /// <summary>
        /// Gets the gaussian3x3.
        /// </summary>
        /// <value>
        /// The gaussian3x3.
        /// </value>
        public static double[,] Gaussian3x3
        {
            get
            {
                return new double[,] { { 1, 2, 1 }, { 2, 4, 2 }, { 1, 2, 1 } };
            }
        }

        /// <summary>
        /// Gets the gaussian5x5 type1.
        /// </summary>
        /// <value>
        /// The gaussian5x5 type1.
        /// </value>
        public static double[,] Gaussian5x5Type1
        {
            get
            {
                return new double[,]
                           {
                               {
                                  2, 04, 05, 04, 2
                               }, {
                                     4, 09, 12, 09, 4
                                  }, {
                                        5, 12, 15, 12, 5
                                     }, {
                                           4, 09, 12, 09, 4
                                        },
                               {
                                  2, 04, 05, 04, 2
                               }
                           };
            }
        }

        /// <summary>
        /// Gets the gaussian5x5 type2.
        /// </summary>
        /// <value>
        /// The gaussian5x5 type2.
        /// </value>
        public static double[,] Gaussian5x5Type2
        {
            get
            {
                return new double[,]
                           {
                               {
                                  1, 4, 6, 4, 1
                               }, {
                                     4, 16, 24, 16, 4
                                  }, {
                                        6, 24, 36, 24, 6
                                     }, {
                                           4, 16, 24, 16, 4
                                        },
                               {
                                  1, 4, 6, 4, 1
                               }
                           };
            }
        }

        /// <summary>
        /// Gets the kirsch3x3 horizontal.
        /// </summary>
        /// <value>
        /// The kirsch3x3 horizontal.
        /// </value>
        public static double[,] Kirsch3x3Horizontal
        {
            get
            {
                return new double[,] { { 5, 5, 5 }, { -3, 0, -3 }, { -3, -3, -3 } };
            }
        }

        /// <summary>
        ///     Gets the kirsch3x3 vertical.
        /// </summary>
        /// <value>
        ///     The kirsch3x3 vertical.
        /// </value>
        public static double[,] Kirsch3x3Vertical
        {
            get
            {
                return new double[,] { { 5, -3, -3 }, { 5, 0, -3 }, { 5, -3, -3 } };
            }
        }

        /// <summary>
        ///     Gets the laplacian3 x3.
        /// </summary>
        /// <value>
        ///     The laplacian3 x3.
        /// </value>
        public static double[,] Laplacian3X3
        {
            get
            {
                return new double[,] { { -1, -1, -1 }, { -1, 8, -1 }, { -1, -1, -1 } };
            }
        }

        /// <summary>
        /// Gets the laplacian5 x5.
        /// </summary>
        /// <value>
        /// The laplacian5 x5.
        /// </value>
        public static double[,] Laplacian5X5
        {
            get
            {
                return new double[,]
                           {
                               {
                                  -1, -1, -1, -1, -1
                               }, 
                               {
                                  -1, -1, -1, -1, -1
                               }, 
                               {
                                  -1, -1, 24, -1, -1
                               },
                               {
                                  -1, -1, -1, -1, -1
                               }, 
                               {
                                  -1, -1, -1, -1, -1
                               }
                           };
            }
        }

        /// <summary>
        /// Gets the laplacian of gaussian.
        /// </summary>
        /// <value>
        /// The laplacian of gaussian.
        /// </value>
        public static double[,] LaplacianOfGaussian
        {
            get
            {
                return new double[,]
                           {
                               { 0, 0, -1, 0, 0 },
                               { 0, -1, -2, -1, 0 },
                               { -1, -2, 16, -2, -1 },
                               { 0, -1, -2, -1, 0 },
                               { 0, 0, -1, 0, 0 }
                           };
            }
        }

        /// <summary>
        /// Gets the prewitt3x3 horizontal.
        /// </summary>
        /// <value>
        /// The prewitt3x3 horizontal.
        /// </value>
        public static double[,] Prewitt3x3Horizontal
        {
            get
            {
                return new double[,] { { -1, 0, 1 }, { -1, 0, 1 }, { -1, 0, 1 } };
            }
        }

        /// <summary>
        /// Gets the prewitt3x3 vertical.
        /// </summary>
        /// <value>
        /// The prewitt3x3 vertical.
        /// </value>
        public static double[,] Prewitt3x3Vertical
        {
            get
            {
                return new double[,] { { 1, 1, 1 }, { 0, 0, 0 }, { -1, -1, -1 } };
            }
        }

        /// <summary>
        /// Gets the sobel3x3 horizontal.
        /// </summary>
        /// <value>
        /// The sobel3x3 horizontal.
        /// </value>
        public static double[,] Sobel3x3Horizontal
        {
            get
            {
                return new double[,]
                           {
                               { -1, 0, 1 },
                               { -2, 0, 2 },
                               { -1, 0, 1 }
                           };
            }
        }

        /// <summary>
        /// Gets the sobel3x3 vertical.
        /// </summary>
        /// <value>
        /// The sobel3x3 vertical.
        /// </value>
        public static double[,] Sobel3x3Vertical
        {
            get
            {
                return new double[,]
                           {
                               { +1, +2, +1 },
                               { +0, +0, +0 },
                               { -1, -2, -1 }
                           };
            }
        }

        /// <summary>
        /// Gets the sobel3x3 vertical.
        /// </summary>
        /// <value>
        /// The sobel3x3 vertical.
        /// </value>
        public static double[,] Sobel3x3Left
        {
            get
            {
                return new double[,]
                           {
                               { +1, 0, -1 },
                               { +2, 0, -2 },
                               { +1, 0, -1 }
                           };
            }
        }

        /// <summary>
        /// Gets the sobel3x3 vertical.
        /// </summary>
        /// <value>
        /// The sobel3x3 vertical.
        /// </value>
        public static double[,] Sobel3x3Right
        {
            get
            {
                return new double[,]
                           {
                               { -1, 0, +1 },
                               { -2, 0, +2 },
                               { -1, 0, +1 }
                           };
            }
        }
    }
}