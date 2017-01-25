using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManager
{
    // nothing interesting there, just some guides
    static class HelpLists
    {
        public static void WelcomeMenuCommandList()
        {
            Console.WriteLine("help - get list of commands");
            Console.WriteLine("exit - leave Cinema Manager");
            Console.WriteLine("movie - add, remove and check movie list");
            Console.WriteLine("about - information about Cinema Manager and its creator");
            Console.WriteLine("seance - add, remove and check seance list");
        }
        public static void MovieListMenu()
        {
            Console.WriteLine("help - get list of commands");
            Console.WriteLine("back - go back");
            Console.WriteLine("add - adds new movie and its information to movie list");
            Console.WriteLine("delete - deletes selected movie from movie list");
            Console.WriteLine("correct - changes chosen movie information");
        }
        public static void SeanceListMenu()
        {
            Console.WriteLine("help - get list of commands");
            Console.WriteLine("back - go back");
            Console.WriteLine("add - adds new seance");
            Console.WriteLine("delete - deletes chosen seance");
            Console.WriteLine("change - changes chosen seance start date and time");
            Console.WriteLine("list - shows seance list");
            Console.WriteLine("status - shows chosen seance reservation status");
        }
        public static void ReservationListMenu()
        {
            Console.WriteLine("help - get list of commands");
            Console.WriteLine("back - go back");
            Console.WriteLine("add - adds new reservation to chosen seance");
            Console.WriteLine("delete - deletes chosen reservations");
            Console.WriteLine("change - changes chosen seance");
            Console.WriteLine("clear - clears all reservations for this seance");
            Console.WriteLine("status - shows chosen seance reservation status");
        }
        public static void About()
        {
            Console.WriteLine("Cinema Manager, version 0.1. \n\nFeatuers for now:\n"
                + "- add movies to your cinema roster and manipulate them\n"
                + "- Create seances\n- Reservation system\n- Work progress save and auto - load with program start"
                + "Future features (in order):"
                + "\n- GUI"
                + "\n Project made by Michal Grzyb. \n e-mail: michal92grzyb@gmail.com"
                + "\nFeel free to contact me, any criticism is welcome!\n");
        }
    }
}
