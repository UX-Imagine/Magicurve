using System.Threading.Tasks;
using Uximagine.Magicurve.DataTransfer.Requests;
using Uximagine.Magicurve.Services;

namespace Uximagine.Magicurve.UI.Web.Common
{
    /// <summary>
    /// Start up point for training.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public static void Init()
        {
            
                        // Do not wait for this call.
                        new Startup().Train(force: false);
        }

        /// <summary>
        /// Trains this instance.
        /// </summary>
        /// <param name="force">if set to <c>true</c> [force].</param>
        /// <returns>
        /// When task is completed.
        /// </returns>
        private async Task Train(bool force)
        {
            IProcessingService service = ServiceFactory.GetProcessingService();
            TrainRequest request = new TrainRequest()
            {
                ForceTraining = force
            };

            await service.Train(request);
        }
    }
}