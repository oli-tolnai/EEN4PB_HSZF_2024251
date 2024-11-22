using EEN4PB_HSZF_2024251.Application;
using EEN4PB_HSZF_2024251.Persistence.MsSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEN4PB_HSZF_2024251.Console
{
    public class Menu
    {

        public static string PathInput()
        {
            System.Console.Write("Please enter the path of the JSON file: ");
            return System.Console.ReadLine();
        }

        public static string FirstMenu()
        {
            System.Console.WriteLine("\nTo start the programme, you need to import a Json File into the database");
            System.Console.Write("Please enter the path of the JSON file: ");
            return System.Console.ReadLine();
        }

        public static void MainMenu(IRailwayLinesLogic railwayLogic)
        {
            System.Console.WriteLine("1. Import another JSON file to database");
            System.Console.WriteLine("2. Exit");

            int input;
            bool isValidInput = false;
            bool correctNumber = false;
            while (!isValidInput || !correctNumber)
            {
                System.Console.Write("Enter your choice: ");
                string userInput = System.Console.ReadLine();

                isValidInput = int.TryParse(userInput, out input);
                //System.Console.WriteLine("Your choice: "+input);

                if (!isValidInput)
                {
                    //System.Console.Clear();
                    System.Console.WriteLine("Invalid input. Please enter a valid number.");
                }
                else if (input != 1 && input != 2)
                {
                    //System.Console.Clear();
                    System.Console.WriteLine("Invalid input. Please choose a correct number.");
                    correctNumber = false;
                }
                else if (input == 1 || input == 2)
                {
                    if (input == 1)
                    {
                        //System.Console.Clear();
                        railwayLogic.FillDatabaseWithNewDataWithProvider(PathInput());
                        System.Console.WriteLine("New JSON file imported to the database");
                        MainMenu(railwayLogic);
                        correctNumber = true;
                    }
                    else if (input == 2)
                    {
                        //System.Console.Clear();
                        System.Console.WriteLine("Goodbye!");
                        Environment.Exit(0);
                        correctNumber = true;
                    }
                }
            }


        }
    }

}
