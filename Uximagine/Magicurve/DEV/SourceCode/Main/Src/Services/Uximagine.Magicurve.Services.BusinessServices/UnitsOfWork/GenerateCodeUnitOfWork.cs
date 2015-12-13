using System.Collections.Generic;
using Uximagine.Magicurve.CodeGenerator;
using Uximagine.Magicurve.Core.Shapes;

namespace Uximagine.Magicurve.Services.BusinessServices.UnitsOfWork
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Generate code unit of work.
    /// </summary>
    internal class GenerateCodeUnitOfWork : UnitOfWork
    {
        #region Properties - Instance Member

        /// <summary>
        /// Gets or sets the controls.
        /// </summary>
        /// <value>
        /// The controls.
        /// </value>
        public List<Control> Controls { get; set; }

        /// <summary>
        /// Gets or sets the image Width.
        /// </summary>
        /// <value>
        /// The image Width.
        /// </value>
        public int ImageWidth { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the height of the image.
        /// </summary>
        /// <value>
        /// The height of the image.
        /// </value>
        public int ImageHeight { get; internal set; }

        #endregion

        #region Methods - Instance Member
        #region Methods - Instance Member - (constructors)
        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateCodeUnitOfWork"/> class.
        /// </summary>
        /// <param name="isReadOnly"><c>true</c> if this Unit of Work is read only;
        /// otherwise, <c>false</c>.</param>
        public GenerateCodeUnitOfWork(bool isReadOnly) : base(isReadOnly)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateCodeUnitOfWork"/> class.
        /// </summary>
        public GenerateCodeUnitOfWork() : this(true)
        {
        }

        #endregion
       
        /// <summary>
        /// The actual Work to be done.
        /// </summary>
        protected override void Execute()
        {
            IGenerator codeGenerator = CodeGeneratorFactory.GetCodeGenerator();
            this.Code = codeGenerator.CreateHtmlCode(this.Controls, this.ImageWidth, this.ImageHeight);
        }

        /// <summary>
        /// The execute async.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        protected override Task ExecuteAsync()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
