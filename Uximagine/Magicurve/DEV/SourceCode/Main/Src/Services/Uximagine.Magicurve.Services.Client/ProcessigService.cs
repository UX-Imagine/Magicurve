using System;
using Uximagine.Magicurve.DataTransfer.Requests;

namespace Uximagine.Magicurve.Services.Client
{
    public class ProcessigService : IProcessingService
    {
        public string GetData(int value)
        {
            throw new NotImplementedException();
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            throw new NotImplementedException();
        }

        public string GetProcessedImageUrl(ProcessImageRequest request)
        {
            IProcessingService service = this.GetService();

            return service.GetProcessedImageUrl(request);
        }

        public IProcessingService GetService()
        {
            ServiceFactory factory = new ServiceFactory();

            return factory.GetProcessingService();
        }

    }
}
