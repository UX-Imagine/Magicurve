namespace Uximagine.Magicurve.Neuro.Processing
{
    using System.Configuration;

    /// <summary>
    /// The configuration data.
    /// </summary>
    internal static class ConfigurationData
    {
        /// <summary>
        /// Gets a value indicating whether [load machine from file].
        /// </summary>
        /// <value>
        /// <c>true</c> if [load machine from file]; otherwise, <c>false</c>.
        /// </value>
        public static bool LoadMachineFromFile
        {
            get
            {
                string load = ConfigurationManager.AppSettings["loadMachine"];
                return load == "true";
            }
        }

        /// <summary>
        /// Gets the machine URL.
        /// </summary>
        /// <value>
        /// The machine URL.
        /// </value>
        public static string MachineUrl
        {
            get
            {
                string url = ConfigurationManager.AppSettings["machineUrl"];
                return url;
            }
        }
    }
}
