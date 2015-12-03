using System.Collections.Generic;
using System.Drawing;
using Uximagine.Magicurve.Core.Shapes;
using Uximagine.Magicurve.Image.Processing;

namespace Uximagine.Magicurve.Services.BusinessServices.UnitsOfWork
{
    /// <summary>
    /// This performs the unit of work for detecting the controls.
    /// </summary>
    internal class DetectControlsUnitOfWork : UnitOfWork
    {
        #region Properties - Instance Member - Public Members
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
        #endregion

        #region Methods - Instance Member - Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DetectControlsUnitOfWork"/> class.
        /// </summary>
        public DetectControlsUnitOfWork()
            : this(true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DetectControlsUnitOfWork"/> class.
        /// </summary>
        /// <param name="isReadOnly"><c>true</c> if this Unit of Work is read only;
        /// otherwise, <c>false</c>.</param>
        public DetectControlsUnitOfWork(bool isReadOnly)
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
            List<Control> controls = processor.Controls;
            this.Controls = controls;
            this.ImageResult = processor.ImageResult;
        }
    }
}
