using System.Collections.Generic;
using System.Drawing;
using Uximagine.Magicurve.Core.Shapes;
using Uximagine.Magicurve.Image.Processing;

namespace Uximagine.Magicurve.Services.BusinessServices.UnitsOfWork
{
    using System;
    using System.Threading.Tasks;

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

        /// <summary>
        /// Gets or sets the image width.
        /// </summary>
        /// <value>
        /// The image width.
        /// </value>
        public int ImageWidth
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the height of the image.
        /// </summary>
        /// <value>
        /// The height of the image.
        /// </value>
        public int ImageHeight { get; internal set; }

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
        /// The execute.
        /// </summary>
        /// <exception cref="NotImplementedException">
        /// </exception>
        protected override void Execute()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// The actual Work to be done.
        /// </summary>
        protected override async Task ExecuteAsync()
        {
            IFileService fileService = ServiceFactory.GetFileService();
            using (Bitmap image = await fileService.LoadImageFile(this.ImagePath))
            {

                Processor processor = new Processor();
                processor.ProcessImage(image);
                List<Control> controls = processor.Controls;
                this.Controls = controls;
                this.ImageResult = processor.ImageResult;
                this.ImageWidth = processor.ImageWidth;
                this.ImageHeight = processor.ImageHeight;
            }
        }
    }
}
