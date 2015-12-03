using Uximagine.Magicurve.Image.Processing;

namespace Uximagine.Magicurve.Services.BusinessServices.UnitsOfWork
{
    /// <summary>
    /// This will train the data.
    /// </summary>
    internal sealed class TrainDataUnitOfWork : UnitOfWork
    {
        /// <summary>
        /// Gets or sets a value indicating whether [force training].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [force training]; otherwise, <c>false</c>.
        /// </value>
        public bool ForceTraining { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrainDataUnitOfWork"/> class.
        /// </summary>
        /// <param name="isReadOnly"><c>true</c> if this Unit of Work is read only;
        /// otherwise, <c>false</c>.</param>
        public TrainDataUnitOfWork(bool isReadOnly) : base(isReadOnly)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrainDataUnitOfWork"/> class.
        /// </summary>
        public TrainDataUnitOfWork() : this(true)
        {
        }

        /// <summary>
        /// The actual Work to be done.
        /// </summary>
        protected override void Execute()
        {
            Trainer trainer = new Trainer()
            {
                ForceTraining = ForceTraining
            };

            trainer.Train(ConfigurationData.MinControlSize, ConfigurationData.SampleSize);
        }
    }
}
