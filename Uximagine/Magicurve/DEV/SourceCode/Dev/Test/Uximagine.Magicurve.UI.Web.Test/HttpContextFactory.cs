namespace Uximagine.Magicurve.UI.Web.Test
{
    using System;
    using System.Web;

    /// <summary>
    /// Factory method to allow us to mock HttpContext when testing
    /// </summary>
    public class HttpContextFactory
    {
        /// <summary>
        /// The context
        /// </summary>
        private static HttpContextBase context;

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
                if (HttpContextFactory.context != null)
                {
                    return context;
                }

                if (HttpContext.Current == null)
                {
                    throw new InvalidOperationException("HttpContext not available");
                }

                return new HttpContextWrapper(HttpContext.Current);
            }
        }

        /// <summary>
        /// Sets the current context.
        /// </summary>
        /// <param name="context">The context.</param>
        public static void SetCurrentContext(HttpContextBase context)
        {
            HttpContextFactory.context = context;
        }
    }
}
