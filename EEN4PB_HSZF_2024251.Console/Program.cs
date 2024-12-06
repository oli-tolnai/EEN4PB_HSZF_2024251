using EEN4PB_HSZF_2024251.Persistence.MsSql;
using EEN4PB_HSZF_2024251.Application;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ConsoleTools;
using System.IO;
using System;
using EEN4PB_HSZF_2024251.Model;
using EEN4PB_HSZF_2024251;



namespace EEN4PB_HSZF_2024251
{
    public class Program
    {
        static void Main(string[] args)
        {

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddScoped<RailwayLinesDbContext>();
                    services.AddSingleton<IRailwayLinesDataProvider, RailwayLinesDataProvider>();
                    services.AddSingleton<IRailwayLinesLogic, RailwayLinesLogic>();

                    services.AddSingleton<IServicesDataProvider, ServicesDataProvider>();
                    services.AddSingleton<IServicesLogic, ServicesLogic>();

                })
                .Build();
            host.Start();

            using IServiceScope serviceScope = host.Services.CreateScope();

            IRailwayLinesLogic railwayLogic = host.Services.GetRequiredService<IRailwayLinesLogic>();

            IServicesLogic servicesLogic = host.Services.GetRequiredService<IServicesLogic>();

            Menu.FirstMenu(railwayLogic, servicesLogic);
            ;


            var i = Console.ReadLine();
            var a = railwayLogic.FindById(i);
            servicesLogic.ConsoleCreateAndAddService(a, "buda", "pest", 1, 0, "IC");




            ;
        }
    }
}
