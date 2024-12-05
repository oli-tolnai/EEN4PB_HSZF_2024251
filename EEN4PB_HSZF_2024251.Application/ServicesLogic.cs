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
    }

    public class ServicesLogic : IServicesLogic
    {
        private readonly IServicesDataProvider serviceDataProvider;

        public ServicesLogic(IServicesDataProvider serviceDataProvider)
        {
            this.serviceDataProvider = serviceDataProvider;
        }
    }
}
