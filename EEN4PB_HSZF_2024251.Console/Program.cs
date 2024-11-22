using EEN4PB_HSZF_2024251.Persistence.MsSql;
using EEN4PB_HSZF_2024251.Application;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ConsoleTools;
using System.IO;
using System;
using EEN4PB_HSZF_2024251.Model;
using EEN4PB_HSZF_2024251.Console;

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

                })
                .Build();
            host.Start();

            using IServiceScope serviceScope = host.Services.CreateScope();

            IRailwayLinesLogic railwayLogic = host.Services.GetRequiredService<IRailwayLinesLogic>();

            railwayLogic.FillDatabaseFirstTimeWithProvider(Menu.FirstMenu());

            Menu.MainMenu(railwayLogic);




            /* TESTS
            var q1 = serviceProvider.GetServices();
            var id = q1[1].Id;
            var q2 = serviceProvider.GetServiceById(id);
            var q3 = serviceProvider.GetRailwayLineServices("120A", "BP-Keleti->Szolnok");

            ;
            railwayProvider.UpdateDatabase(Menu.PathInput());

            var q4 = serviceProvider.GetRailwayLineServices("120A", "BP-Keleti->Szolnok");
            */

            ;
        }
    }
}
