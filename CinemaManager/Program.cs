using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManager
{
    class Program
    {
        static void Main(string[] args)
        {
            SaveLoad.Load();
            Console.WriteLine("Welcome to Cinema Manager, type \"help\" to get commmand list");
            Menus.MainMenu();
            SaveLoad.Save();
        }
    }
}
