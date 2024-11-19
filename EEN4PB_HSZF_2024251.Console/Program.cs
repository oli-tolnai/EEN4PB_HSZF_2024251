using EEN4PB_HSZF_2024251.Persistence.MsSql;
using EEN4PB_HSZF_2024251.Application;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ConsoleTools;
using System.IO;
using System;
using EEN4PB_HSZF_2024251.Model;
namespace EEN4PB_HSZF_2024251
{
    public class Program
    {
        static string PathInput()
        {
            System.Console.Write("Please enter the path of the JSON file: ");
            return System.Console.ReadLine();
        }

        static void Main(string[] args)
        {
            /*
            var ctx = new RailwayLinesDbContext();
            var RdataProvider = new RailwayLinesDataProvider(PathInput(), ctx);

            var servicesDataProvider = new ServicesDataProvider(ctx);
            var q1 = servicesDataProvider.GetServices();
            var id = q1[1].Id;
            var q2 = servicesDataProvider.GetServiceById(id);
            var q3 = servicesDataProvider.GetRailwayLineServices("120A", "BP-Keleti->Szolnoks");

            */
            


            
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddScoped<RailwayLinesDbContext>();
                    services.AddSingleton<IRailwayLinesDataProvider>(provider => new RailwayLinesDataProvider(PathInput(), provider.GetRequiredService<RailwayLinesDbContext>()));
                    services.AddSingleton<IServicesDataProvider, ServicesDataProvider>();

                })
                .Build();
            host.Start();

            using IServiceScope serviceScope = host.Services.CreateScope();

            
            IRailwayLinesDataProvider railwayProvider = host.Services.GetRequiredService<IRailwayLinesDataProvider>();

            IServicesDataProvider serviceProvider = host.Services.GetRequiredService<IServicesDataProvider>();

            List<RailwayLine> q11 = railwayProvider.GetRailwayLines();

            var q1 = serviceProvider.GetServices();
            var id = q1[1].Id;
            var q2 = serviceProvider.GetServiceById(id);
            var q3 = serviceProvider.GetRailwayLineServices("120A", "BP-Keleti->Szolnok");

            ;
            railwayProvider.UpdateDatabase(PathInput());

            var q4 = serviceProvider.GetRailwayLineServices("120A", "BP-Keleti->Szolnok");


            ;
        }
    }
}
