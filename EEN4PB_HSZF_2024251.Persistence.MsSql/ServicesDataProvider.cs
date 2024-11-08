using EEN4PB_HSZF_2024251.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEN4PB_HSZF_2024251.Persistence.MsSql
{
    public interface IServicesDataProvider
    {
        Service GetServiceById(string id);

        List<Service> GetServices();

        List<Service> GetRailwayLineServices(string lineNumber, string lineName);

        //Service CRUD operations
        void CreateService(string railwayLineId, Service service);

        IEnumerable<Service> ReadAllServices();

        void UpdateService(Service service);

        void DeleteService(string id);

    }

    public class ServicesDataProvider : IServicesDataProvider
    {
        private readonly RailwayLinesDbContext ctx;

        public ServicesDataProvider(RailwayLinesDbContext ctx)
        {
            this.ctx = ctx;
        }

        public Service GetServiceById(string id)
        {
            return ctx.Services.FirstOrDefault(x => x.Id == id);
        }

        public List<Service> GetServices()
        {
            return ctx.Services.ToList();
        }

        public List<Service> GetRailwayLineServices(string lineNumber, string lineName)
        {
            var servicesList = new List<Service>();

            foreach (var railwayLine in ctx.RailwayLines)
            {
                if (railwayLine.LineNumber == lineNumber && railwayLine.LineName == lineName)
                {
                    servicesList.AddRange(railwayLine.Services);
                }
            }

            return servicesList;
        }

        public void CreateService(string railwayLineId, Service service)
        {
            service.RailwayLineId = railwayLineId;
            ctx.Services.Add(service);
            ctx.SaveChanges();

        }

        public void DeleteService(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Service> ReadAllServices()
        {
            throw new NotImplementedException();
        }

        public void UpdateService(Service service)
        {
            throw new NotImplementedException();
        }
    }
}
