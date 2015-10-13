using System.Collections.Generic;
using System.Drawing;
using Uximagine.Magicurve.Image.Processing;
using Uximagine.Magicurve.Core.Shapes;

namespace Uximagine.Magicurve.Services.BusinessServices.UnitsOfWork
{
    /// <summary>
    /// This performs the unit of work for detecting the edges.
    /// </summary>
    internal class DetectEdgesUnitOfWork : UnitOfWork
    {
        /// <summary>
        /// Gets or sets the image path.
        /// </summary>
        /// <value>
        /// The image path.
        /// </value>
        public string ImagePath
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        public List<Control> Controls
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>
        /// The image.
        /// </value>
        public Bitmap ImageResult
        {
            get;
            set;
        } 

        #region Methods - Instance Member - Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DetectEdgesUnitOfWork"/> class.
        /// </summary>
        public DetectEdgesUnitOfWork()
            : this(true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DetectEdgesUnitOfWork"/> class.
        /// </summary>
        /// <param name="isReadOnly"><c>true</c> if this Unit of Work is read only;
        /// otherwise, <c>false</c>.</param>
        public DetectEdgesUnitOfWork(bool isReadOnly)
            : base(isReadOnly)
        {
        }

        #endregion

        /// <summary>
        /// The actual Work to be done.
        /// </summary>
        protected override void Execute()
        {
            Processor processor = new Processor();
            processor.ProcessImage(this.ImagePath);
            this.Controls = processor.Controls;
            this.ImageResult = processor.ImageResult;
        }
    }
}
