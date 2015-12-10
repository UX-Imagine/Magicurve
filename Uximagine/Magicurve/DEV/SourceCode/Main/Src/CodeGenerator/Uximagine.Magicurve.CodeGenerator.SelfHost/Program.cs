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
            WriteLine("code service is runnung at http://localhost:9000.");
            WriteLine("use a rest client to access http://localhost:9000/api/code/controls to get demo request.");
            WriteLine("use a rest client to access http://localhost:9000/api/code/get to get HTML code.");
            WriteLine("Hit Enter to exit.");
            ReadLine();
        }
    }
}
