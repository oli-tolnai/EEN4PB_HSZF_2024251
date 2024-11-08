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
        Service GetServiceById(int id);

        List<Service> GetServices();

        //Service CRUD operations
        void CreateService(Service service);

        IEnumerable<Service> ReadAllServices();

        void UpdateService(Service service);

        void DeleteService(int id);

    }

    public class ServicesDataProvider
    {
    }
}
