namespace Uximagine.Magicurve.CodeGenerator.SelfHost
{
    using Microsoft.Owin.Hosting;
    using static System.Console;

    /// <summary>
    /// The program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The base address.
        /// </summary>
        private static string baseAddress = "http://localhost:9000";

        /// <summary>
        /// The main.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        public static void Main(string[] args)
        {
            WebApp.Start(url: baseAddress);
            ReadLine();
        }
    }
}
