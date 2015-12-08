namespace Uximagine.Magicurve.CodeGenerator.Common
{
    using System.Collections.Specialized;
    using System.Configuration;

    /// <summary>
    /// The configuration data.
    /// </summary>
    public static class ConfigurationData
    {
        /// <summary>
        /// Gets the default width of the page.
        /// </summary>
        /// <value>
        /// The default width of the page.
        /// </value>
        public static int DefaultPageWidth
        {
            get
            {
                NameValueCollection values = (NameValueCollection)ConfigurationManager.GetSection("CodeConfig");
                string value = values["defaultPageWidth"];
                return int.Parse(value);
            }
        }

        /// <summary>
        /// Gets the default height of the page.
        /// </summary>
        /// <value>
        /// The default height of the page.
        /// </value>
        public static int DefaultPageHeight
        {
            get
            {
                NameValueCollection values = (NameValueCollection)ConfigurationManager.GetSection("CodeConfig");
                string value = values["defaultPageHeight"];
                return int.Parse(value);
            }
        }

        /// <summary>
        /// Gets the default height of the row.
        /// </summary>
        /// <value>
        /// The default height of the row.
        /// </value>
        public static double DefaultRowHeight
        {
            get
            {
                NameValueCollection values = (NameValueCollection)ConfigurationManager.GetSection("CodeConfig");
                string height = values["defaultRowHeight"];
                return double.Parse(height);
            }
        }

        /// <summary>
        /// Gets the default margin top.
        /// </summary>
        /// <value>
        /// The default margin top.
        /// </value>
        public static string DefaultMarginTop
        {
            get
            {
                NameValueCollection values = (NameValueCollection)ConfigurationManager.GetSection("CodeConfig");
                string value = values["defaultMarginTop"];
                return value;
            }
        }
    }
}
