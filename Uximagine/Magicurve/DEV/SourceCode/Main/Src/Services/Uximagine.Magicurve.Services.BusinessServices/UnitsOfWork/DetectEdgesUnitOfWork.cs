using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Uximagine.Magicurve.CodeGenerator;
using Uximagine.Magicurve.Core.Models;
using Uximagine.Magicurve.Core.Shapes;
using Uximagine.Magicurve.Image.Processing;

namespace Uximagine.Magicurve.Services.BusinessServices.UnitsOfWork
{
    /// <summary>
    /// This performs the unit of work for detecting the edges.
    /// </summary>
    internal class DetectEdgesUnitOfWork : UnitOfWork
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
        public List<Row> Controls
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
            List<Control> controls = processor.Controls;
            this.Controls = GetRows(controls);
            this.ImageResult = processor.ImageResult;
        }

        /// <summary>
        /// Gets the rows.
        /// </summary>
        /// <param name="list">
        /// The list.
        /// </param>
        /// <returns>
        /// The rows.
        /// </returns>
        private List<Row> GetRows(List<Control> list)
        {
            list = list.OrderBy(c => c.Y).ToList();
            List<Row> listOfList = new List<Row>();
            double maxHeight = list[0].Height;
            int rowIndex = 0;
            listOfList.Add(new Row()
            {
                Controls = new List<Control>()
                    {
                        list[0]
                    },
                RowIndex = 0,
                Height = maxHeight
            });

            for (int i = 1; i <= (list.Count) - 1; i++)
            {
                int previousY = list[i - 1].Y;
                int currentY = list[i].Y;

                if (currentY > previousY + maxHeight)
                {
                    maxHeight = list[i].Height;
                    listOfList.Add(new Row()
                    {
                        Controls = new List<Control>()
                        {
                            list[i]
                        },
                        RowIndex = ++rowIndex,
                        Height = maxHeight
                    });

                }
                else
                {

                    if (list[i].Height > maxHeight)
                    {
                        maxHeight = list[i].Height;
                    }

                    listOfList[rowIndex].Controls.Add(list[i]);
                    listOfList[rowIndex].Height = maxHeight;
                }
            }

            return listOfList;
        }
    }
}
