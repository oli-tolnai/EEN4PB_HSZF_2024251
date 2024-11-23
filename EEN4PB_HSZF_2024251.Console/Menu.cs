using EEN4PB_HSZF_2024251.Application;
using EEN4PB_HSZF_2024251.Model;
using EEN4PB_HSZF_2024251.Persistence.MsSql;
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
            Console.Write("Please enter the path of the JSON file: ");
            return Console.ReadLine();
        }

        public static string FirstMenu()
        {
            Console.WriteLine("\nTo start the application, the database must be loaded. To do this, enter the path to the json file.");
            return PathInput();
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

        public static void MainMenu(IRailwayLinesLogic railwayLogic)
        {
            Console.ReadLine();
            Console.Clear();
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
            while (!isValidInput /*|| !correctNumber*/)
            {
                Console.Write("Enter your choice: ");
                string userInput = Console.ReadLine();

                isValidInput = int.TryParse(userInput, out input);
                //Console.WriteLine("Your choice: "+input);

                if (!isValidInput)
                {
                    //Console.Clear();
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
                else if (input != 1 && input != 2 && input != 3 && input != 8)
                {
                    //Console.Clear();
                    Console.WriteLine("Invalid input. Please choose a valid number.");
                    isValidInput = false;
                }
                else if (input == 1 || input == 2 || input == 3 || input == 8)
                {
                    //1. Import another JSON file to database
                    if (input == 1)
                    {
                        //Console.Clear();
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
                    else if (input == 8)
                    {
                        //Console.Clear();
                        Console.WriteLine("Goodbye!");
                        Environment.Exit(0);
                    }
                }
            }
        }
    }
}
