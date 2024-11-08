using ConsoleTools;
using Microsoft.CodeAnalysis.CodeActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEN4PB_HSZF_2024251.Console
{
    public class MainMenu
    {
        public ConsoleColor SelectedItemBackgroundColor = System.Console.ForegroundColor;
        public ConsoleColor SelectedItemForegroundColor = System.Console.BackgroundColor;
        public ConsoleColor ItemBackgroundColor = System.Console.BackgroundColor;
        public ConsoleColor ItemForegroundColor = System.Console.ForegroundColor;
        public Encoding InputEncoding = System.Console.InputEncoding;
        public Encoding OutputEncoding = System.Console.OutputEncoding;
        public Action WriteHeaderAction = () => System.Console.WriteLine("Pick an option:");
        public Action<MenuItem> WriteItemAction = item => System.Console.Write("[{0}] {1}", item.Index, item.Name);
        public string Selector = ">> ";
        public string FilterPrompt = "Filter: ";
        public bool ClearConsole = true;
        public bool EnableFilter = false;
        public string ArgsPreselectedItemsKey = "--menu-select=";
        public char ArgsPreselectedItemsValueSeparator = '.';
        public bool EnableWriteTitle = false;
        public string Title = "My menu";
        public Action<string> WriteTitleAction = title => System.Console.WriteLine(title);
        public bool EnableBreadcrumb = false;
        public Action<IReadOnlyList<string>> WriteBreadcrumbAction = titles => System.Console.WriteLine(string.Join(" > ", titles));
        public bool EnableAlphabet = false;


    }
}
