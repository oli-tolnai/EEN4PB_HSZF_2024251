using EEN4PB_HSZF_2024251.Persistence.MsSql;
using EEN4PB_HSZF_2024251.Application;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ConsoleTools;
using System.IO;
using System;
namespace EEN4PB_HSZF_2024251
{
    public class Program
    {
        static void Main(string[] args)
        {
            
            var ctx = new RailwayLinesDbContext();
            var RdataProvider = new RailwayLinesDataProvider("railwayLines3.json", ctx);

            var servicesDataProvider = new ServicesDataProvider(ctx);
            var q1 = servicesDataProvider.GetServices();
            var id = q1[1].Id;
            var q2 = servicesDataProvider.GetServiceById(id);
            var q3 = servicesDataProvider.GetRailwayLineServices("120A", "BP-Keleti->Szolnoks");


            //var railwayLines = ctx.RailwayLines.ToList();
            //var services = ctx.Services.ToList();

            //var q1 = from x in railwayLines
            //         select x.Services




            /*
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    //TODO: Add services here

                    services.AddScoped<RailwayLinesDbContext>();
                    services.AddSingleton<IRailwayLinesDataProvider, RailwayLinesDataProvider>();
                    services.AddSingleton<IRailwayLinesLogic, RailwayLinesLogic>();

                    services.AddSingleton<IServicesDataProvider, ServicesDataProvider>(); 

                })
                .Build();
            host.Start();

            //Call RailwayLinesLogic

            using IServiceScope scope = host.Services.CreateScope();

            var railwayLinesLogic = host.Services.GetRequiredService<IRailwayLinesLogic>();

            var railwayLine = railwayLinesLogic.GetRailwayLineById("");
            */



            /*ConsoleMenu Try
            new ConsoleMenu()
                .Add("Add new Railway Line", () => SomeAction("One"))
                .Add("Add new Service", () => SomeAction("Two"))
                .Add("Close", () => OtherAction())
                .Configure(config => { config.Selector = "--> "; })
                .Show();*/


            ;
        }

        /* ConsoleMenu try
        public static void SomeAction(string action)
        {
            new ConsoleMenu()
                .Add("Halicsihi", () => SomeAction("One"))
                .Add("Add new Service", () => SomeAction("Two"))
                .Add("Close", () => OtherAction())
                .Configure(config => { config.Selector = "--> "; })
                .Show();
        }

        public static void OtherAction()
        {
            System.Environment.Exit(-1);
        }*/
    }
}
