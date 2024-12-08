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
        public static void LowestDelayEvent() { Console.WriteLine("\nNew lowest delay amount detected!"); }
        public static void RailwayLineCreatedEvent() { Console.WriteLine("\nNew railway line created!"); }
        public static void AlreadyInUseEvent() { Console.WriteLine("\nOne or two of the values specified are already in use!"); }

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


            servicesLogic.LowestDelayEventHandler += LowestDelayEvent;
            railwayLogic.RailwayLineCreatedEventHandler += RailwayLineCreatedEvent;
            railwayLogic.AlreadyInUseEventHandler += AlreadyInUseEvent;


            var sConsole = new ConsoleMenu();
            sConsole.Add("Import a JSON file to database", () => Menu.MenuOptionOne(railwayLogic, sConsole))
                      .Add("Create a new Railway Line", () => Menu.MenuOptionTwo(railwayLogic, sConsole))
                      .Add("Delete an existing Railway Line", () => Menu.MenuOptionThree(railwayLogic, sConsole))
                      .Add("Change name or number of a railway line.", () => Menu.MenuOptionFour(railwayLogic, sConsole))
                      .Add("Add new Service to an existing Railway Line", () => Menu.MenuOptionFive(railwayLogic, servicesLogic, sConsole))
                      .Add("Generate statistics", () => Menu.MenuOptionSix(railwayLogic, servicesLogic, sConsole))
                      .Add("List of railway lines with filtering", () => Menu.MenuOptionSeven(railwayLogic, servicesLogic, sConsole))
                      .Add("Exit", Menu.MenuOptionEight)
                      .Configure(config =>
                      {
                          config.Selector = "  ";
                          config.WriteHeaderAction = () => Console.WriteLine(Menu.header + "\n\nPick an option:");
                          config.WriteItemAction = item => Console.Write($"[{item.Index + 1}] {item.Name}");
                      })
                      .Show();



            ;
        }
    }
}
