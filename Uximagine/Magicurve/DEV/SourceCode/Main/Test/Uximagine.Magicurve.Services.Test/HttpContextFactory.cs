namespace Uximagine.Magicurve.Services.Test
{
    using System;
    using System.Web;

    /// <summary>
    /// Factory method to allow us to mock HttpContext when testing
    /// </summary>
    public class HttpContextFactory
    {
        /// <summary>
        /// The ctx
        /// </summary>
        private static HttpContextBase _context;

        /// <summary>
        /// Gets the current.
        /// </summary>
        /// <value>
        /// The current.
        /// </value>
        /// <exception cref="System.InvalidOperationException">HttpContext not available</exception>
        public static HttpContextBase Current
        {
            get
            {
                if (_context != null)
                {
                    return _context;
                }

                if (HttpContext.Current == null)
                {
                    throw new InvalidOperationException("HttpContext not available");
                }

                return new HttpContextWrapper(HttpContext.Current);
            }
        }

        /// <summary>
        /// Sets the current ctx.
        /// </summary>
        /// <param name="ctx">
        /// The ctx.
        /// </param>
        public static void SetCurrentContext(HttpContextBase ctx)
        {
            _context = ctx;
        }
    }
}
