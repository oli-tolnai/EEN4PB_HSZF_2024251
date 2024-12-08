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

        public void ConsoleCreateAndAddService(RailwayLine railwayline, Service service);

    }

    public class ServicesDataProvider : IServicesDataProvider
    {
        private readonly RailwayLinesDbContext ctx;

        public ServicesDataProvider(RailwayLinesDbContext ctx)
        {
            this.ctx = ctx;
        }

        public void ConsoleCreateAndAddService(RailwayLine railwayline, Service service)
        {
            CreateService(service);
            railwayline.Services.Add(service);
            ctx.SaveChanges();
        }

        public void CreateService(Service service)
        {
            ctx.Set<Service>().Add(service);
            ctx.SaveChanges();
        }

        public Service FindById(string id)
        {
            return ctx.Set<Service>().FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<Service> GetAll()
        {
            return ctx.Set<Service>();
        }

    }
}
