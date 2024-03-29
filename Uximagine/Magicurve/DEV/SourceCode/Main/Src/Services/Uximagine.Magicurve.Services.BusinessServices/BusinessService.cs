﻿using Uximagine.Magicurve.Services.BusinessServices.UnitsOfWork;

namespace Uximagine.Magicurve.Services.BusinessServices
{
    using System.Threading.Tasks;

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
        /// <param name="unitOfWork">The unit of work.</param>
        /// <returns>
        /// The task.
        /// </returns>
        protected virtual async Task DoWork(UnitOfWork unitOfWork)
        {
            await unitOfWork.DoWork();
        }

        #endregion

        #endregion
    }
}
