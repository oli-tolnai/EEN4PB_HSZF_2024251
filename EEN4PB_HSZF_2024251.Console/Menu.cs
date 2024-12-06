using ConsoleTools;
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
        public static string PathInput(ConsoleMenu menu)
        {
            Console.WriteLine("Please enter the path of the JSON file you want to import to the database:");
            var path = Console.ReadLine();
            if (path == "")
            {
                Console.WriteLine("You did not specify a file.");
                Console.Write("Press Enter to return to the menu.");
                Console.ReadLine();
                menu.Show();
            }

            var jsonfileexists = File.Exists(path);
            while (!jsonfileexists || !path.EndsWith(".json"))
            {
                Console.WriteLine("File not found or it's not a json file. Please try again:");
                path = Console.ReadLine();
                if (path == "")
                {
                    Console.WriteLine("You did not specify a file.");
                    Console.Write("Press Enter to return to the menu.");
                    Console.ReadLine();
                    menu.Show();
                }
                jsonfileexists = File.Exists(path);
            }
            Console.WriteLine("File found. Importing to database...");
            return path;
        }
        
        /*
        public static void FirstMenu(IRailwayLinesLogic railwayLogic, IServicesLogic servicesLogic, ConsoleMenu menu)
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Railway Line Manager");
            Console.WriteLine("===================================\n");
            railwayLogic.FillDatabase(PathInput(menu));
            //MainMenu(railwayLogic, servicesLogic);
            Console.WriteLine("\nPress Enter to continue");
            Console.ReadLine();
            menu.Show();
        }*/

        //MenuOptionOne: Import another JSON file to database
        public static void MenuOptionOne(IRailwayLinesLogic railwayLogic, ConsoleMenu menu)
        {
            Console.Clear();
            Console.WriteLine(header);
            railwayLogic.FillDatabase(PathInput(menu));
            Console.WriteLine("New JSON file imported to the database");

            
            Console.Write("\nPress Enter to return to the menu.");
            Console.ReadLine();
            menu.Show();
        }

        //MenuOptionTwo: Create a new Railway Line
        public static void MenuOptionTwo(IRailwayLinesLogic railwayLogic, ConsoleMenu menu)
        {
            Console.Clear();
            Console.WriteLine(header);

            string inputLineName = "";
            string inputLineNumber = "";
            bool inputIsNotNull = false;

            Console.Write("Railway line name: ");
            inputLineName = Console.ReadLine();
            Console.Write("Railway line number: ");
            inputLineNumber = Console.ReadLine();
            if (inputLineName != "" && inputLineNumber != "")
            {
                railwayLogic.CreateRailwayLinesConsole(inputLineName, inputLineNumber);

                
                Console.Write("Press Enter to return to the menu.");
                Console.ReadLine();
                menu.Show();
            }
            else
            {
                menu.Show();
            }
        }

        //MenuOptionThree: Delete an existing Railway Line
        public static void MenuOptionThree(IRailwayLinesLogic railwayLogic, ConsoleMenu menu)
        {
            Console.Clear();
            Console.WriteLine(header);

            //if railway line is empty, return to main menu
            if (railwayLogic.GetAllRailwayLines().Count() == 0)
            {
                Console.WriteLine("There are no Railway Lines to delete.");

                Console.Write("\nPress Enter to continue");
                Console.ReadLine();
                menu.Show();
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
                if (userDeleteInput == "")
                {
                    break;
                }
                else if (!isValidDeleteInput)
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
            Console.Write("\nPress Enter to return to the menu.");
            Console.ReadLine();
            menu.Show();
        }

        //MenuOptionFour: Change name or number of a railway line.
        public static void MenuOptionFour(IRailwayLinesLogic railwayLogic, ConsoleMenu menu)
        {
            Console.Clear();
            Console.WriteLine(header);

            //if railway line is empty, return to main menu
            if (railwayLogic.GetAllRailwayLines().Count() == 0)
            {
                Console.WriteLine("There are no Railway Lines to update.");
                Console.Write("\nPress Enter to return to the menu.");
                Console.ReadLine();
                menu.Show();
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
                if (userUpdateInput == "")
                {
                    break;
                }
                else if (!isValidUpdateInput)
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
            Console.Write("\nPress Enter to return to the menu.");
            Console.ReadLine();
            menu.Show();
        }

        //MenuOptionFive: Add new Service to an existing Railway Line
        public static void MenuOptionFive(IRailwayLinesLogic railwayLogic, IServicesLogic servicesLogic, ConsoleMenu menu)
        {
            Console.Clear();
            Console.WriteLine(header);

            if (railwayLogic.GetAllRailwayLines().Count() == 0)
            {
                Console.WriteLine("There are no Railway Lines to add a service to.");
                Console.Write("\nPress Enter to return to the menu.");
                Console.ReadLine();
                menu.Show();
            }
            //else
            var allrailwayLines = railwayLogic.GetAllRailwayLines();
            WriteOutRailwayLines(railwayLogic);
            int addServiceInput;
            var isValidAddServiceInput = false;
            while (!isValidAddServiceInput)
            {
                //TODO: return menu if the number is null and many things TODO here
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

                    //TODO: if give nothing return to the menu + intparse -> tryparse
                    Console.Write("From: ");
                    string inputFrom = Console.ReadLine();
                    Console.Write("To: ");
                    string inputTo = Console.ReadLine();
                    Console.Write("Train number: ");
                    string inputTrainNumber = Console.ReadLine();
                    Console.Write("Delay Amount: ");
                    string inputDelayAmount = Console.ReadLine();
                    Console.Write("Train type: ");
                    string inputTrainType = Console.ReadLine();

                    //if at least one of them empty
                    if (inputFrom == "" || inputTo == "" || inputTrainNumber == "" || inputDelayAmount == "" || inputTrainType == "")
                    {
                        Console.WriteLine("Please fill out all the fields.");
                        isValidAddServiceInput = false;
                        continue;
                    }
                    //if train number or delay amount not a number
                    if (!int.TryParse(inputTrainNumber, out _) || !int.TryParse(inputDelayAmount, out _))
                    {
                        Console.WriteLine("Train number and delay amount must be a number.");
                        isValidAddServiceInput = false;
                        continue;
                    }
                    int inputDelayAmountInt = int.Parse(inputDelayAmount);
                    int inputTrainNumberInt = int.Parse(inputTrainNumber);
                    servicesLogic.ConsoleCreateAndAddService(allrailwayLines.ToList()[addServiceInput - 1], inputFrom, inputTo, inputTrainNumberInt, inputDelayAmountInt, inputTrainType);
                    Console.WriteLine("Service added to Railway Line");
                    isValidAddServiceInput = true;
                }
            }
            Console.Write("\nPress Enter to return to the menu.");
            Console.ReadLine();
            menu.Show();
        }

        //TODO: statistics
        //MenuOptionSix: Generate Statistics
        public static void MenuOptionSix(IRailwayLinesLogic railwayLogic, IServicesLogic servicesLogic, ConsoleMenu menu)
        {
            Console.Clear();
            Console.WriteLine(header);

            Console.WriteLine("Statistics");
            Console.WriteLine("==========\n");

            var allRailwayLines = railwayLogic.GetAllRailwayLines();
            var allServices = servicesLogic.GetAllServices();

            Console.WriteLine("Number of Railway Lines: " + allRailwayLines.Count());
            Console.WriteLine("Number of Services: " + allServices.Count());
            Console.WriteLine("Average number of services per Railway Line: " + allServices.Count() / allRailwayLines.Count());
            Console.WriteLine("Average delay amount: " + allServices.Average(x => x.DelayAmount) + " minutes");
            

            Console.Write("\nPress Enter to return to the menu.");
            Console.ReadLine();
            menu.Show();
        }

        //TODO: Filtering
        //MenuOptionSeven: List of railway lines with filtering
        public static void MenuOptionSeven(IRailwayLinesLogic railwayLogic, IServicesLogic servicesLogic, ConsoleMenu menu)
        {
            Console.Clear();
            Console.WriteLine(header);


            Console.Write("\nPress Enter to return to the menu.");
            Console.ReadLine();
            menu.Show();
        }

        //MenuOptionEight: Exit
        public static void MenuOptionEight()
        {
            Console.Clear();
            Console.WriteLine("Goodbye!");
            Environment.Exit(0);
        }
        
        
        /*MainMenu
        public static void MainMenu(IRailwayLinesLogic railwayLogic, IServicesLogic servicesLogic)
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
                else if (input != 1 && input != 2 && input != 3 && input != 4 && input != 5 && input != 8)
                {
                    Console.WriteLine("Invalid input. Please choose a valid number.");
                    isValidInput = false;
                }
                else if (input == 1 || input == 2 || input == 3 || input == 4 || input == 5 || input == 8)
                {
                    //1. Import another JSON file to database
                    if (input == 1)
                    {
                        //railwayLogic.FillDatabase(PathInput());  //PathInput miatt van kikommettezve
                        Console.WriteLine("New JSON file imported to the database");
                        MainMenu(railwayLogic, servicesLogic);
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
                        MainMenu(railwayLogic, servicesLogic);
                    }
                    //3.Delete an existing Railway Line
                    else if (input == 3)
                    {
                        //if railway line is empty, return to main menu
                        if (railwayLogic.GetAllRailwayLines().Count() == 0)
                        {
                            Console.WriteLine("There are no Railway Lines to delete.");
                            MainMenu(railwayLogic, servicesLogic);
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
                        MainMenu(railwayLogic, servicesLogic);
                    }
                    //4. Change name or number of a railway line.
                    else if (input == 4)
                    {
                        //if railway line is empty, return to main menu
                        if (railwayLogic.GetAllRailwayLines().Count() == 0)
                        {
                            Console.WriteLine("There are no Railway Lines to update.");
                            MainMenu(railwayLogic, servicesLogic);
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
                        MainMenu(railwayLogic, servicesLogic);
                    }
                    //5.Add new Service to an existing Railway Line
                    else if (input == 5)
                    {
                        if (railwayLogic.GetAllRailwayLines().Count() == 0)
                        {
                            Console.WriteLine("There are no Railway Lines to add a service to.");
                            MainMenu(railwayLogic, servicesLogic);
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

                                Console.Write("From: ");
                                string inputFrom = Console.ReadLine();
                                Console.Write("To: ");
                                string inputTo = Console.ReadLine();
                                Console.Write("Train number: ");
                                string inputTrainNumber = Console.ReadLine();
                                int inputTrainNumberInt = int.Parse(inputTrainNumber);
                                Console.Write("Delay Amount: ");
                                string inputDelayAmount = Console.ReadLine();
                                int inputDelayAmountInt = int.Parse(inputDelayAmount);
                                Console.Write("Train type: ");
                                string inputTrainType = Console.ReadLine();

                                servicesLogic.ConsoleCreateAndAddService(allrailwayLines.ToList()[addServiceInput - 1], inputFrom, inputTo, inputTrainNumberInt, inputDelayAmountInt, inputTrainType);
                                Console.WriteLine("Service added to Railway Line");
                                isValidAddServiceInput = true;
                            }
                        }
                        MainMenu(railwayLogic, servicesLogic);

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
        }*/

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

        public static string header = "  _   _ ____ _______     __          _____ _____ _   _ _  _   ____  ____  \r\n | \\ | / ___|__  /\\ \\   / /         | ____| ____| \\ | | || | |  _ \\| __ ) \r\n |  \\| \\___ \\ / /  \\ \\ / /   _____  |  _| |  _| |  \\| | || |_| |_) |  _ \\ \r\n | |\\  |___) / /_   \\ V /   |_____| | |___| |___| |\\  |__   _|  __/| |_) |\r\n |_| \\_|____/____|   \\_/            |_____|_____|_| \\_|  |_| |_|   |____/ \n";
    }
}
