using Uximagine.Magicurve.Services.BusinessServices.UnitsOfWork;

namespace Uximagine.Magicurve.Services.BusinessServices
{
    /// <summary>
    /// The type from which all business services must be derived.
    /// </summary>
    /// <remarks>
    /// This is an implementation of the PoEAA Layer Super type pattern.
    /// </remarks>
    public abstract class BusinessService
    {
        #region Methods - Instance Member

        #region Methods - Instance Member - BusinessService Members

        /// <summary>
        /// Does the given Unit of Work.
        /// </summary>
        /// <param name="unitOfWork">
        /// The unit of work.
        /// </param>
        protected virtual void DoWork(UnitOfWork unitOfWork)
        {
            unitOfWork.DoWork();
        }

        #endregion

        #endregion
    }
}
