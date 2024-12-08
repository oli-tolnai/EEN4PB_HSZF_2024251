using System;
using System.Linq;
using ConsoleTools;
using EEN4PB_HSZF_2024251.Application;
using EEN4PB_HSZF_2024251.Model;

namespace EEN4PB_HSZF_2024251
{
    class SearchEngine
    {
        public static void SearchRailwayLines(IRailwayLinesLogic railwayLogic)
        {
            WriteOutRailwayLinesAndServices(railwayLogic);


            Console.WriteLine("Do you want to use filters? (y/n)");
            string useFilters = Console.ReadLine().Trim().ToLower();

            if (useFilters == "y")
            {
                //Prompt the user for each filterable property
                Console.WriteLine("Enter 'From' (leave blank to skip):");
                string from = Console.ReadLine().Trim();

                Console.WriteLine("Enter 'To' (leave blank to skip):");
                string to = Console.ReadLine().Trim();

                Console.WriteLine("Enter 'Train Number' (leave blank to skip):");
                string trainNumberInput = Console.ReadLine().Trim();
                int? trainNumber = string.IsNullOrEmpty(trainNumberInput) ? (int?)null : int.Parse(trainNumberInput);

                Console.WriteLine("Enter 'Delay Amount' (leave blank to skip):");
                string delayAmountInput = Console.ReadLine().Trim();
                int? delayAmount = string.IsNullOrEmpty(delayAmountInput) ? (int?)null : int.Parse(delayAmountInput);

                Console.WriteLine("Enter 'Train Type' (leave blank to skip):");
                string trainType = Console.ReadLine().Trim();

                
                Console.Clear();
                Console.WriteLine(Menu.header);

                // Write out the filters provided by the user
                Console.WriteLine("Filters applied:");
                if (!string.IsNullOrEmpty(from)) Console.WriteLine($"From: {from}");
                if (!string.IsNullOrEmpty(to)) Console.WriteLine($"To: {to}");
                if (trainNumber.HasValue) Console.WriteLine($"Train Number: {trainNumber}");
                if (delayAmount.HasValue) Console.WriteLine($"Delay Amount: {delayAmount} minutes");
                if (!string.IsNullOrEmpty(trainType)) Console.WriteLine($"Train Type: {trainType}");
                Console.WriteLine();

                //Filter the services based on the provided input
                var filteredRailwayLines = railwayLogic.GetAllRailwayLines()
                    .Where(rl => rl.Services.Any(s =>
                        (string.IsNullOrEmpty(from) || s.From == from) &&
                        (string.IsNullOrEmpty(to) || s.To == to) &&
                        (!trainNumber.HasValue || s.TrainNumber == trainNumber) &&
                        (!delayAmount.HasValue || s.DelayAmount == delayAmount) &&
                        (string.IsNullOrEmpty(trainType) || s.TrainType == trainType)))
                    .ToList();

                //Display the filtered results
                foreach (var railwayLine in filteredRailwayLines)
                {
                    Console.WriteLine($"{railwayLine.LineNumber} - {railwayLine.LineName}");
                    var filteredServices = railwayLine.Services
                        .Where(s =>
                            (string.IsNullOrEmpty(from) || s.From == from) &&
                            (string.IsNullOrEmpty(to) || s.To == to) &&
                            (!trainNumber.HasValue || s.TrainNumber == trainNumber) &&
                            (!delayAmount.HasValue || s.DelayAmount == delayAmount) &&
                            (string.IsNullOrEmpty(trainType) || s.TrainType == trainType))
                        .ToList();

                    foreach (var service in filteredServices)
                    {
                        Console.WriteLine($"\tFrom: {service.From}, To: {service.To}, Train Number: {service.TrainNumber}, Delay: {service.DelayAmount} minutes, Train Type: {service.TrainType}");
                    }
                }
            }
        }

        public static void WriteOutRailwayLinesAndServices(IRailwayLinesLogic railwayLogic)
        {
            var railwayLines = railwayLogic.GetAllRailwayLines().ToList();
            foreach (var railwayLine in railwayLines)
            {
                Console.WriteLine($"{railwayLine.LineNumber} - {railwayLine.LineName}");
                foreach (var service in railwayLine.Services)
                {
                    Console.WriteLine($"\tFrom: {service.From}, To: {service.To}, Train Number: {service.TrainNumber}, Delay: {service.DelayAmount} minutes, Train Type: {service.TrainType}");
                }
            }
        }
    }
}