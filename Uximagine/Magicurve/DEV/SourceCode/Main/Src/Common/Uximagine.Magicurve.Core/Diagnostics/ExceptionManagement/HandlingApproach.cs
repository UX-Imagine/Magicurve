using System;

namespace Uximagine.Magicurve.Core.Diagnostics.ExceptionManagement
{
    /// <summary>
    /// The handling approach for exceptions.
    /// </summary>
    [Serializable]
    public enum HandlingApproach
    {
        /// <summary>
        /// Re-throw the caught exception.
        /// </summary>
        Rethrow,

        /// <summary>
        /// Sink the caught exception.
        /// </summary>
        Sink,

        /// <summary>
        /// Wrap (and throw) the caught exception.
        /// </summary>
        Wrap,

        /// <summary>
        /// Swapping the caught exception with another exception.
        /// </summary>
        Swap
    }
}
