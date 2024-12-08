using EEN4PB_HSZF_2024251.Model;
using EEN4PB_HSZF_2024251.Persistence.MsSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEN4PB_HSZF_2024251.Application
{
    public interface IServicesLogic
    {
        public void ConsoleCreateAndAddService(RailwayLine railwayline, string from, string to, int trainNumber, int delayAmount, string trainType);

        public IQueryable<Service> GetAllServices();

        public event Action LowestDelayEventHandler;
    }

    

    public class ServicesLogic : IServicesLogic
    {
        public event Action LowestDelayEventHandler;

        private readonly IServicesDataProvider provider;

        public ServicesLogic(IServicesDataProvider serviceDataProvider)
        {
            this.provider = serviceDataProvider;
        }

        public void ConsoleCreateAndAddService(RailwayLine railwayline, string from, string to, int trainNumber, int delayAmount, string trainType)
        {
            // Check if the new service has the lowest delayAmount among the other services in the same railwayline
            var lowestDelayAmount = provider.GetAll()
                .Where(s => s.RailwayLineId == railwayline.Id)
                .Min(s => s.DelayAmount);
            
            if (delayAmount < lowestDelayAmount)
            {
                LowestDelayEventHandler?.Invoke();
            }

            Service newservice = new Service(from, to, trainNumber, delayAmount, trainType);
            newservice.RailwayLineId = railwayline.Id;
            provider.ConsoleCreateAndAddService(railwayline, newservice);

        }

        public IQueryable<Service> GetAllServices()
        {
            return provider.GetAll();
        }
    }
}
