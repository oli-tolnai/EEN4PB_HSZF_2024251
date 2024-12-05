using EEN4PB_HSZF_2024251.Model;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEN4PB_HSZF_2024251.Persistence.MsSql
{
    public interface IServicesDataProvider
    {
        /*public Service GetServiceById(string id);

        public List<Service> GetServices();

        public List<Service> GetRailwayLineServices(string lineNumber, string lineName);

        //Service CRUD operations
        public void CreateService(string railwayLineId, Service service);

        public IEnumerable<Service> ReadAllServices();

        public void UpdateService(string id, Service service);

        public void DeleteService(string id);*/

        public void CreateService(Service service);

        public Service FindById(string id);

        public IQueryable<Service> GetAll();

        public void CreateService(string railwayLineId, Service service);

    }

    public class ServicesDataProvider : IServicesDataProvider
    {
        private readonly RailwayLinesDbContext ctx;

        public ServicesDataProvider(RailwayLinesDbContext ctx)
        {
            this.ctx = ctx;
        }

        public void CreateService(Service service)
        {
            ctx.Set<Service>().Add(service);
            ctx.SaveChanges();
        }

        public void CreateService(string railwayLineId, Service service)
        {
            //TODO: ServiceDatProvider then Logic
            throw new NotImplementedException();
        }

        public Service FindById(string id)
        {
            return ctx.Set<Service>().FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<Service> GetAll()
        {
            return ctx.Set<Service>();
        }



        /*
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
            return ctx.Services.ToList();
        }

        public void UpdateService(string id, Service service)
        {
            //update service
            var s = GetServiceById(id);
            if (service.From != null)
            {
                s.From = service.From;
            }
            if (service.To != null)
            {
                s.To = service.To;
            }
            if ((service.DelayAmount).GetType() == typeof(int))
            {
                s.DelayAmount = service.DelayAmount;
            }
            if (service.TrainNumber != 0)
            {
                s.TrainNumber = service.TrainNumber;
            }
            if (service.TrainType != null)
            {
                s.TrainType = service.TrainType;
            }
            ctx.SaveChanges();

        }*/
    }
}
