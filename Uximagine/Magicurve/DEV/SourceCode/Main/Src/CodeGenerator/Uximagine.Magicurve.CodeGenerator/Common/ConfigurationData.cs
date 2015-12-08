namespace Uximagine.Magicurve.CodeGenerator.Common
{
    using System.Configuration;

    /// <summary>
    /// The configuration data.
    /// </summary>
    public static class ConfigurationData
    {
        /// <summary>
        /// The default page width.
        /// </summary>
        public static int DefaultPageWidth => int.Parse(ConfigurationManager.AppSettings["defaultWidth"]);

        /// <summary>
        /// The default page height.
        /// </summary>
        public static int DefaultPageHeight => int.Parse(ConfigurationManager.AppSettings["defaultHeight"]);
    }
}
