using System;

namespace Uximagine.Magicurve.Core
{
    /// <summary>
    /// Provides helper functionality for date/time operations.
    /// </summary>
    public static class DateTimeHelper
    {

        /// <summary>
        /// Gets the now.
        /// </summary>
        /// <value>
        /// The now.
        /// </value>
        public static DateTime Now
        {
            get
            {
                return DateTimeHelper.UtcNow;
            }
        }

        /// <summary>
        /// Gets the UTC now.
        /// </summary>
        /// <value>
        /// The UTC now.
        /// </value>
        public static DateTime UtcNow
        {
            get
            {
                return DateTime.UtcNow;
            }
        }

        /// <summary>
        /// Gets the local now.
        /// </summary>
        /// <value>
        /// The local now.
        /// </value>
        public static DateTime LocalNow
        {
            get
            {
                return DateTime.Now;
            }
        }
    }
}
