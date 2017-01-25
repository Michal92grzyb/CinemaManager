using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManager
{
    static class Menus
    {
        static bool doWeWantToExit = false;

        public static void MainMenu()
        {
            do
            {
                string command = Console.ReadLine();
                switch (command.ToLower())
                {
                    case "help":
                        HelpLists.WelcomeMenuCommandList();
                        break;
                    case "exit":
                        doWeWantToExit = true;
                        break;
                    case "movie":
                        MovieMenu();
                        break;
                    case "about":
                        HelpLists.About();
                        break;
                    case "seance":
                        SeanceMenu();
                        break;
                    default:
                        Console.WriteLine("Invalid command!");
                        break;
                }
                if (doWeWantToExit == false)
                {
                    Console.WriteLine("You are now at main menu, type \"help\" to get command list");
                }
            } while (doWeWantToExit == false);
        }
        static void MovieMenu()
        {
            string command;
            do
            {
                Console.WriteLine("You are in the movie list menu, type \"help\" to get commmand list");
                command = Console.ReadLine();
                switch (command.ToLower())
                {
                    case "help":
                        HelpLists.MovieListMenu();
                        break;
                    case "back":
                        break;
                    case "add":
                        Movie.AddMovie();
                        break;
                    case "delete":
                        Movie.DeleteMovie();
                        break;
                    case "correct":
                        Movie.CorrectMovie();
                        break;
                    case "list":
                        Movie.ShowMovieList();
                        break;
                    default:
                        Console.WriteLine("Invalid command!");
                        break;
                }
            } while (command.ToLower() != "back");
        }
        static void SeanceMenu()
        {
            string command;
            do
            {
                Console.WriteLine("You are in the seance list menu, type \"help\" to get commmand list");
                command = Console.ReadLine();
                switch (command.ToLower())
                {
                    case "help":
                        HelpLists.SeanceListMenu();
                        break;
                    case "back":
                        break;
                    case "add":
                        Movie.Seance.AddSeance();
                        break;
                    case "delete":
                        Movie.Seance.DeleteSeance();
                        break;
                    case "change":
                        if (Movie.Seance.seanceCounter == 0)
                        {
                            Console.WriteLine("There are no seances to change!");
                            break;
                        }
                        Movie.Seance.ChangeSeanceDate();
                        break;
                    case "list":
                        Movie.Seance.ShowSeanceList();
                        break;
                    case "status":
                        Movie.Seance.ShowReservations();
                        break;
                    case "reservation":
                        InitiateReservationMenu();
                        break;
                    default:
                        Console.WriteLine("Invalid command!");
                        break;
                }
            } while (command.ToLower() != "back");
        }
        static void InitiateReservationMenu()
        {
            if (Movie.Seance.seanceCounter != 0)
            {
                Movie.Seance.ShowSeanceList();
                Console.Write("Pick seance to edit reservations: ");
                int seanceNumber;
                bool isANumber = int.TryParse(Console.ReadLine(), out seanceNumber);
                if (isANumber)
                {
                    seanceNumber--;
                    if (seanceNumber >= 0 && seanceNumber < Movie.Seance.seanceCounter)
                    {
                        ReservationMenu(seanceNumber);
                    }
                    else
                    {
                        Console.WriteLine("Wrong number!");
                    }
                }
                else
                {
                    Console.WriteLine("That's not a number!");
                }
            }
            else
            {
                Console.WriteLine("There are no seances created!");
            }
        }
        static void ReservationMenu(int seanceNumber)
        {
            string command;
            do
            {
                Console.WriteLine("You are in the reservation list menu for {0}, at {1} (seance number {2}) type \"help\" to get commmand list",
                    Movie.Seance.seanceList[seanceNumber].Parent.Name, Movie.Seance.seanceList[seanceNumber].SeanceStart, seanceNumber + 1);
                command = Console.ReadLine();
                switch (command.ToLower())
                {
                    case "help":
                        HelpLists.ReservationListMenu();
                        break;
                    case "back":
                        break;
                    case "status":
                        Movie.Seance.ShowReservations(seanceNumber);
                        break;
                    case "add":
                        Movie.Seance.ManipulateReservations(seanceNumber, 'R');
                        break;
                    case "delete":
                        Movie.Seance.ManipulateReservations(seanceNumber, 'X');
                        break;
                    case "clear":
                        Movie.Seance.ClearCinemaHall(seanceNumber);
                        break;
                    case "change":
                        InitiateReservationMenu();
                        command = "back"; // when i type 'back' after changing reservation it will go to the previous instance and have to type 'back' again. This is a prevention.
                        break;
                    default:
                        Console.WriteLine("Invalid command!");
                        break;
                }
            } while (command.ToLower() != "back");
        }
    }
}
