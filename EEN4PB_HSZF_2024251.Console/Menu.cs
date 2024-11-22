using EEN4PB_HSZF_2024251.Application;
using EEN4PB_HSZF_2024251.Model;
using EEN4PB_HSZF_2024251.Persistence.MsSql;
using System;
using System.Collections.Generic;
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

        public static void MainMenu(IRailwayLinesLogic railwayLogic)
        {
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
            bool correctNumber = false;
            while (!isValidInput || !correctNumber)
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
                else if (input != 1 && input != 2 && input != 8)
                {
                    //Console.Clear();
                    Console.WriteLine("Invalid input. Please choose a valid number.");
                    correctNumber = false;
                }
                else if (input == 1 || input == 2 || input == 8)
                {
                    if (input == 1)
                    {
                        //Console.Clear();
                        railwayLogic.FillDatabaseWithNewData(PathInput());
                        Console.WriteLine("New JSON file imported to the database");
                        MainMenu(railwayLogic);
                        correctNumber = true;
                    }
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
                        correctNumber = true;
                    }
                    else if (input == 8)
                    {
                        //Console.Clear();
                        Console.WriteLine("Goodbye!");
                        Environment.Exit(0);
                        correctNumber = true;
                    }
                }
            }
        }
    }
}
