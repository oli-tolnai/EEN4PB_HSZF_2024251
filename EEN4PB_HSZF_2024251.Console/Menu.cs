using EEN4PB_HSZF_2024251.Application;
using EEN4PB_HSZF_2024251.Model;
using EEN4PB_HSZF_2024251.Persistence.MsSql;
using Humanizer;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEN4PB_HSZF_2024251
{
    public class Menu
    {

        public static string PathInput()
        {
            Console.WriteLine("Please enter the path of the JSON file you want to import to the database:");
            var path = Console.ReadLine();

            var jsonfileexists = File.Exists(path);
            while (!jsonfileexists || !path.EndsWith(".json"))
            {
                Console.WriteLine("File not found or it's not a json file. Please try again:");
                path = Console.ReadLine();
                jsonfileexists = File.Exists(path);
            }
            Console.WriteLine("File found. Importing to database...");
            return path;
        }

        public static void FirstMenu(IRailwayLinesLogic railwayLogic)
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Railway Line Manager");
            Console.WriteLine("===================================\n");
            railwayLogic.FillDatabase(PathInput());
            MainMenu(railwayLogic);
        }

        public static void MainMenu(IRailwayLinesLogic railwayLogic)
        {
            Console.WriteLine("Press Enter to continue");
            Console.ReadLine();
            Console.Clear();

            Console.WriteLine("Choose an option from the menu below:\n");
            Console.WriteLine("1. Import another JSON file to database");
            Console.WriteLine("2. Create a new Railway Line");
            Console.WriteLine("3. Delete an existing Railway Line");
            Console.WriteLine("4. Change name or number of a railway line.");
            Console.WriteLine("5. Add new Service to an existing Railway Line");
            Console.WriteLine("6. Generate statistics");
            Console.WriteLine("7. List of railway lines with filtering");
            Console.WriteLine("8. Exit");

            int input;
            bool isValidInput = false;
            while (!isValidInput)
            {
                Console.Write("Enter your choice: ");
                string userInput = Console.ReadLine();

                isValidInput = int.TryParse(userInput, out input);

                if (!isValidInput)
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
                else if (input != 1 && input != 2 && input != 3 && input != 4 && input != 8)
                {
                    Console.WriteLine("Invalid input. Please choose a valid number.");
                    isValidInput = false;
                }
                else if (input == 1 || input == 2 || input == 3 || input == 4 || input == 8 )
                {
                    //1. Import another JSON file to database
                    if (input == 1)
                    {
                        railwayLogic.FillDatabase(PathInput());
                        Console.WriteLine("New JSON file imported to the database");
                        MainMenu(railwayLogic);
                    }
                    //2. Create a new Railway Line
                    else if (input == 2)
                    {
                        string inputLineName = "";
                        string inputLineNumber = "";
                        bool inputIsNotNull = false;
                        while (!inputIsNotNull)
                        {
                            Console.Write("Railway line name: ");
                            inputLineName = Console.ReadLine();
                            Console.Write("Railway line number: ");
                            inputLineNumber = Console.ReadLine();
                            if (inputLineName != "" && inputLineNumber != "")
                            {
                                inputIsNotNull = true;
                            }
                            else
                            {
                                Console.WriteLine("Please enter a valid name and number for the Railway Line");
                            }
                        }
                        railwayLogic.CreateRailwayLinesConsole(inputLineName, inputLineNumber);
                        MainMenu(railwayLogic);
                    }
                    //3.Delete an existing Railway Line
                    else if (input == 3)
                    {
                        //if railway line is empty, return to main menu
                        if (railwayLogic.GetAllRailwayLines().Count() == 0)
                        {
                            Console.WriteLine("There are no Railway Lines to delete.");
                            MainMenu(railwayLogic);
                        }
                        //else
                        var allrailwayLines = railwayLogic.GetAllRailwayLines();
                        WriteOutRailwayLines(railwayLogic);
                        int deleteInput;
                        var isValidDeleteInput = false;
                        while (!isValidDeleteInput)
                        {
                            Console.Write("Enter the number of the Railway Line you want to delete: ");
                            string userDeleteInput = Console.ReadLine();
                            isValidDeleteInput = int.TryParse(userDeleteInput, out deleteInput);
                            if (!isValidDeleteInput)
                            {
                                Console.WriteLine("Invalid input. Please enter a number.");
                            }
                            else if (deleteInput < 1 || deleteInput > allrailwayLines.Count())
                            {
                                Console.WriteLine("Invalid input. Please choose a valid number.");
                                isValidDeleteInput = false;
                            }
                            else
                            {
                                railwayLogic.DeleteRailwayLine(allrailwayLines.ToList()[deleteInput - 1].Id);
                                Console.WriteLine("Railway Line deleted");
                                isValidDeleteInput = true;
                            }
                        }
                        MainMenu(railwayLogic);
                    }
                    //4. Change name or number of a railway line.
                    else if (input == 4)
                    {
                        //if railway line is empty, return to main menu
                        if (railwayLogic.GetAllRailwayLines().Count() == 0)
                        {
                            Console.WriteLine("There are no Railway Lines to update.");
                            MainMenu(railwayLogic);
                        }
                        //else
                        var allrailwayLines = railwayLogic.GetAllRailwayLines();
                        WriteOutRailwayLines(railwayLogic);
                        int updateInput;
                        var isValidUpdateInput = false;
                        while (!isValidUpdateInput)
                        {
                            Console.Write("Enter the number of the Railway Line you want to update: ");
                            string userUpdateInput = Console.ReadLine();
                            isValidUpdateInput = int.TryParse(userUpdateInput, out updateInput);
                            if (!isValidUpdateInput)
                            {
                                Console.WriteLine("Invalid input. Please enter a number.");
                            }
                            else if (updateInput < 1 || updateInput > allrailwayLines.Count())
                            {
                                Console.WriteLine("Invalid input. Please choose a valid number.");
                                isValidUpdateInput = false;
                            }
                            else
                            {
                                Console.Write("The Railway line you want to update: \n\t\t");
                                Console.Write("Number: " + allrailwayLines.ToList()[updateInput - 1].LineNumber);
                                Console.WriteLine("\tName: " + allrailwayLines.ToList()[updateInput - 1].LineName);

                                Console.WriteLine("\nWhich you do not wish to change, leave blank.");
                                Console.Write("New number: ");
                                string inputLineNumber = Console.ReadLine();
                                Console.Write("New name: ");
                                string inputLineName = Console.ReadLine();

                                railwayLogic.UpdateRailwayLineConsole(allrailwayLines.ToList()[updateInput - 1].Id, inputLineName, inputLineNumber);
                                Console.WriteLine("Railway Line updated");
                                isValidUpdateInput = true;
                            }
                        }
                        MainMenu(railwayLogic);
                    }
                    //5.Add new Service to an existing Railway Line
                    else if (input == 5)
                    {   /*
                        if (railwayLogic.GetAllRailwayLines().Count() == 0)
                        {
                            Console.WriteLine("There are no Railway Lines to add a service to.");
                            MainMenu(railwayLogic);
                        }
                        //else
                        var allrailwayLines = railwayLogic.GetAllRailwayLines();
                        WriteOutRailwayLines(railwayLogic);
                        int addServiceInput;
                        var isValidAddServiceInput = false;
                        while (!isValidAddServiceInput)
                        {
                            Console.Write("Enter the number of the Railway Line you want to add a service to: ");
                            string userAddServiceInput = Console.ReadLine();
                            isValidAddServiceInput = int.TryParse(userAddServiceInput, out addServiceInput);
                            if (!isValidAddServiceInput)
                            {
                                Console.WriteLine("Invalid input. Please enter a number.");
                            }
                            else if (addServiceInput < 1 || addServiceInput > allrailwayLines.Count())
                            {
                                Console.WriteLine("Invalid input. Please choose a valid number.");
                                isValidAddServiceInput = false;
                            }
                            else
                            {
                                Console.Write("The Railway line you want to add a service to: \n\t\t");
                                Console.Write("Number: " + allrailwayLines.ToList()[addServiceInput - 1].LineNumber);
                                Console.WriteLine("\tName: " + allrailwayLines.ToList()[addServiceInput - 1].LineName);

                                Console.Write("Service name: ");
                                string inputServiceName = Console.ReadLine();
                                Console.Write("Service number: ");
                                string inputServiceNumber = Console.ReadLine();
                                Console.Write("Service type: ");
                                string inputServiceType = Console.ReadLine();
                                Console.Write("Service length: ");
                                string inputServiceLength = Console.ReadLine();
                                Console.Write("Service start station: ");
                                string inputServiceStartStation = Console.ReadLine();
                                Console.Write("Service end station: ");
                                string inputServiceEndStation = Console.ReadLine();

                                railwayLogic.AddServiceToRailwayLineConsole(allrailwayLines.ToList()[addServiceInput - 1].Id, inputServiceName, inputServiceNumber, inputServiceType, inputServiceLength, inputServiceStartStation, inputServiceEndStation);
                                Console.WriteLine("Service added to Railway Line");
                                isValidAddServiceInput = true;
                            }
                        }
                        MainMenu(railwayLogic);
                        */
                    }
                    //8. Exit
                    else if (input == 8)
                    {
                        Console.Clear();
                        Console.WriteLine("Goodbye!");
                        Environment.Exit(0);
                    }
                }
            }
        }

        public static void WriteOutRailwayLines(IRailwayLinesLogic railwayLogic)
        {
            var items = railwayLogic.GetAllRailwayLines();

            //write out all railway lines to console
            int db = 0;
            foreach (var item in items)
            {
                db++;
                Console.WriteLine($"{db}.\t{item.LineNumber}\t{item.LineName}\n");

            }
        }
    }
}
