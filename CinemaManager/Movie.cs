using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace CinemaManager
{
    class Movie
    {
        public static int movieListCounter = 0;
        // up to 10 movies at once
        public static Movie[] movieList = new Movie[10];
        
        private string name;
        private int lenghtInMinutes;
        private string type;
        // no other constructors
        public Movie(string name, int lenghtInMinutes, string type)
        {
            this.name = name;
            this.lenghtInMinutes = lenghtInMinutes;
            this.type = type;
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int LenghtInMinutes
        {
            get { return lenghtInMinutes; }
            set { lenghtInMinutes = value; }
        }
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        
        // shows moie list
        public static void ShowMovieList()
        {
            Console.WriteLine("|{0,3}|{1, 15}|{2, 10}|{3, 15}|", "No.", "Name", "Lenght in minutes", "Type");
            string nameFormatted;
            string typeFormatted;
            for (int x = 0; x < movieList.Length; x++)
            {
                if (movieList[x] == null) // saving some time
                {
                    break;
                }
                nameFormatted = movieList[x].Name;
                if (nameFormatted.Length > 15)
                {
                    nameFormatted = nameFormatted.Substring(0, 15);
                }
                typeFormatted = movieList[x].Type;
                if (typeFormatted.Length > 15)
                {
                    typeFormatted = nameFormatted.Substring(0, 15);
                }
                
                Console.WriteLine("|{0, 3}|{1, 15}|{2, 17}|{3, 15}|",
                    x + 1, nameFormatted, movieList[x].LenghtInMinutes,
                    typeFormatted); // x+1 cause dont want to have first movie number to be 0.
            }
        }
        public static void AddMovie(string name, int lenght, string type)
        {
            movieList[movieListCounter] = new Movie(name, lenght, type);
            movieListCounter++;
        }

        public static void AddMovie()
        {
            try
            {
                // when full, dont let to add.
                if (movieListCounter == 10)
                {
                    throw new StackOverflowException();
                }
                Console.Write("Type the movie name: ");
                string name = Console.ReadLine();
                name = CompareMovieName(name);
                Console.Write("Insert movie lenght: ");
                int lenght = int.Parse(Console.ReadLine());
                if (lenght<1)
                {
                    throw new FormatException();
                }
                Console.Write("Write movie type: ");
                string type = Console.ReadLine();
                movieList[movieListCounter] = new Movie(name, lenght, type);
                movieListCounter++;
            }
            catch (StackOverflowException)
            {
                Console.WriteLine("There are too much movies in the roster, please" +
                    " delete at least one if you need to add more.");
            }
            catch (FormatException)
            {
                Console.WriteLine("That's not a valid number!"); ;
            }
        }

        public static string CompareMovieName(string name)
        {
            bool didChange = false;
            int valueCheck;
            bool isANum = int.TryParse(name, out valueCheck);
            if (isANum && valueCheck <= 10 && valueCheck >= 0)
            {
                name = "Movie " + name;
                didChange = true;
            }
            for (int x = 0; x < movieListCounter; x++)
            {
                if (name == movieList[x].name)
                {
                    name = name + "1";
                    didChange = true;
                    CompareMovieName(name);
                }
            }
            if(didChange)
            {
                Console.WriteLine("Due to invalid movie name, it was changed to: {0}", name);
            }
            return name;
        }

        public static void DeleteMovie()
        {
            try
            {
                // can't delete anything when it's empty, right?
                if (movieListCounter == 0)
                {
                    throw new InvalidOperationException();
                }
                Console.WriteLine("Type the number of movie you want to delete");
                int deleteMovieNumber = int.Parse(Console.ReadLine());
                // can't delete movie that doesn't exist
                if (deleteMovieNumber > movieListCounter || deleteMovieNumber <= 0 )
                {
                    throw new ArgumentOutOfRangeException();
                }
                movieList[deleteMovieNumber - 1] = null;
                for (int x = deleteMovieNumber; x < movieList.Length; x++)
                {
                    movieList[x - 1] = movieList[x];
                }
                movieListCounter--;
                Console.WriteLine("Movie was deleted. Seances that were created for this movie were not.");
            }
            catch(InvalidOperationException)
            {
                Console.WriteLine("No movies to delete");
            }
            catch(ArgumentOutOfRangeException)
            {
                Console.WriteLine("Incorrect number!");
            }
        }

        public static void CorrectMovie()
        {
            try
            {
                Console.Write("Type movie number you want to change: ");
                int movieToChange = int.Parse(Console.ReadLine());
                if (movieToChange > movieListCounter || movieToChange <= 0)
                {
                    throw new ArgumentOutOfRangeException();
                }
                // correcting only one movie property at time, maybe some day.
                Console.WriteLine("If there is a property that you dont need to change, press Enter without typing anything");
                Console.Write("Type new movie name: ");
                string name = Console.ReadLine();
                Console.Write("Insert new movie lenght: ");
                int lenght;
                int.TryParse(Console.ReadLine(), out lenght);
                Console.Write("Write new movie type: ");
                string type = Console.ReadLine();
                if (name != null)
                {
                    movieList[movieToChange - 1].Name = name;
                }
                if (lenght != 0)
                {
                    movieList[movieToChange - 1].LenghtInMinutes = lenght;
                }
                if (type != null)
                {
                    movieList[movieToChange - 1].Type = type;
                }
            }
            catch (ArgumentOutOfRangeException AOORE)
            {
                Console.WriteLine("Incorrect number!");
            }
            catch (System.FormatException)
            {
                Console.WriteLine("That's not a number!");
            }
        }
        
        public class Seance
        {
            public static int seanceCounter = 0;
            // 10 seances for each movie, more or less.
            public static Seance[] seanceList = new Seance[100];

            // lets say we want a default date for our seance to be current day at noon
            private DateTime seanceStart;
            private DateTime seanceEnd;
            private Movie parent;
            internal char[,] cinemaHall;

            public DateTime SeanceStart
            {
                get { return seanceStart; }
                set { seanceStart = value; }
            }
            public DateTime SeanceEnd
            {
                get { return seanceStart.AddMinutes(parent.lenghtInMinutes); }
            }
            public Movie Parent
            {
                get { return parent; }
            }

            public Seance(Movie Parent, DateTime SeanceStart)
            {
                this.parent = Parent;
                this.seanceStart = SeanceStart;
                this.seanceEnd = this.seanceStart.AddMinutes(parent.lenghtInMinutes);
                this.cinemaHall = new char[18, 33];

                for (int x = 0; x < this.cinemaHall.GetLength(0); x++)
                {
                    for (int y = 0; y < this.cinemaHall.GetLength(1); y++)
                    {
                        this.cinemaHall[x, y] = 'X';
                    }
                }
                // making "empty" seats
                for (int x = 9, z = 0; x < this.cinemaHall.GetLength(0); x++, z++)
                {
                    for (int y = 0; y < this.cinemaHall.GetLength(1); y++)
                    {
                        for (int k = 0; k <= z; k++)
                        {
                            this.cinemaHall[x, k] = ' ';
                            this.cinemaHall[x, this.cinemaHall.GetLength(1) - k - 1] = ' ';
                        }
                    }
                }
            }

            public static void ClearCinemaHall(int seanceNumber)
            {
                char[,] memo = Movie.Seance.seanceList[seanceNumber].cinemaHall;
                for (int x = 0; x < Movie.Seance.seanceList[seanceNumber].cinemaHall.GetLength(0); x++)
                {
                    for (int y = 0; y < Movie.Seance.seanceList[seanceNumber].cinemaHall.GetLength(1); y++)
                    {
                        Movie.Seance.seanceList[seanceNumber].cinemaHall[x, y] = 'X';
                    }
                }
                // making "empty" seats
                for (int x = 9, z = 0; x < Movie.Seance.seanceList[seanceNumber].cinemaHall.GetLength(0); x++, z++)
                {
                    for (int y = 0; y < Movie.Seance.seanceList[seanceNumber].cinemaHall.GetLength(1); y++)
                    {
                        for (int k = 0; k <= z; k++)
                        {
                            Movie.Seance.seanceList[seanceNumber].cinemaHall[x, k] = ' ';
                            Movie.Seance.seanceList[seanceNumber].cinemaHall[x, Movie.Seance.seanceList[seanceNumber].cinemaHall.GetLength(1) - k - 1] = ' ';
                        }
                    }
                }
                string areYouSure;
                do
                {
                    Console.WriteLine("Are you sure you want to clear this seance? (y/n): ");
                    areYouSure = Console.ReadLine();
                }
                while (areYouSure != "y" && areYouSure != "n");
                
                if (areYouSure == "y")
                {
                    Console.WriteLine("You have successfully cleared reservation list");
                }
                else
                {
                    Movie.Seance.seanceList[seanceNumber].cinemaHall = memo;
                    Console.WriteLine("Reservation list was not cleared");
                }
            }
            
            public static void AddSeance(int movieNum, DateTime startDate)
            {
                seanceList[seanceCounter] = new Seance(movieList[movieNum], startDate);
                seanceCounter++;
            }

            public static void AddSeance() // needs datecheck - can't play 2 seances at once
            {
                try
                {
                    
                    if (movieListCounter == 0)
                    {
                        throw new InvalidOperationException();
                    }
                    Movie.ShowMovieList();
                    Console.Write("Type the number of movie that you want to make seance of: ");
                    int movieNum = int.Parse(Console.ReadLine());
                    if (movieNum > movieListCounter || movieNum <= 0)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                    Console.WriteLine("Write the starting time for seance start in \"yyyy-MM-dd HH:mm\" format:");
                    DateTime startDate = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd HH:mm", new CultureInfo("en-US"));
                    seanceList[seanceCounter] = new Seance(movieList[movieNum - 1], startDate);
                    if (SeanceCollisionCheck(seanceCounter))
                    {
                        Console.WriteLine("You have successfully created \"{0}\" seance at {1}", seanceList[seanceCounter].parent.name, seanceList[seanceCounter].seanceStart);
                        seanceCounter++;
                        SortSeanceList();
                    }
                    else
                    {
                        seanceList[seanceCounter] = null;
                        Console.WriteLine("Seance was not created.");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Wrong date format");
                    Console.WriteLine("Remember to always put correct number of digits\nex. 2017-03-06 08:05 instead of 2017-3-6 8:05");
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine("There are no created movies!");
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Wrong movie number!");
                    Movie.ShowMovieList();
                }
            }
            public static void SortSeanceList() // by date
            {
                Seance placeholder;
                for (int x = 0; x < seanceCounter; x++)
                {
                    for (int y = x+1; y < seanceCounter; y++)
                    {
                        if (seanceList[x].seanceStart > seanceList[y].SeanceStart)
                        {
                            placeholder = seanceList[y];
                            seanceList[y] = seanceList[x];
                            seanceList[x] = placeholder;
                        }
                    }
                }
            }
            public static void ShowSeanceList()
            {
                Console.WriteLine("|{0,3}|{1, 15}|{2, 19}|{3, 19}|", "No.", "Name", "Start", "End");
                for (int x = 0; x < seanceCounter; x++)
                {
                    string movieName = seanceList[x].parent.Name;
                    // dont want to break the table
                    if (movieName.Length > 15)
                    {
                        movieName = movieName.Substring(0, 15);
                    }
                    Console.WriteLine("|{0,3}|{1,15}|{2,19}|{3,19}|", x+1, movieName, seanceList[x].seanceStart, seanceList[x].seanceEnd);
                }
            }
            public static void ChangeSeanceDate()
            {
                try
                {
                    ShowSeanceList();
                    Console.Write("Pick a seance number, which date will be changed: ");
                    int seanceToChange;
                    bool isANumber = int.TryParse(Console.ReadLine(), out seanceToChange);
                    seanceToChange--;
                    if (seanceToChange < 0 || seanceToChange >= seanceCounter)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                    Console.WriteLine("Write new seance date in \"yyyy-MM-dd HH:mm\" format:");
                    DateTime startMemo = seanceList[seanceToChange].seanceStart;
                    DateTime endMemo = seanceList[seanceToChange].seanceEnd;
                    seanceList[seanceToChange].seanceStart = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd HH:mm", new CultureInfo("en-US"));
                    seanceList[seanceToChange].seanceEnd = seanceList[seanceToChange].seanceStart.AddMinutes(seanceList[seanceToChange].parent.lenghtInMinutes);
                    if (SeanceCollisionCheck(seanceToChange))
                    {
                        Console.WriteLine("Seance date successfully changed");
                        SortSeanceList();
                    }
                    else
                    {
                        seanceList[seanceToChange].seanceStart = startMemo;
                        seanceList[seanceToChange].seanceEnd = endMemo;
                        Console.WriteLine("Seance wasn't successfully changed, try different date");
                    }
                    
                }
                catch (FormatException)
                {
                    Console.WriteLine("Wrong date format");
                    Console.WriteLine("Remember to always put correct number of digits\nex. 2017-03-06 08:05 instead of 2017-3-6 8:05");
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Wrong number!");
                }
            }
            public static void DeleteSeance()
            {
                try
                {
                    ShowSeanceList();
                    Console.Write("Pick a seance to cancel: ");
                    int seanceToCancel;
                    int.TryParse(Console.ReadLine(), out seanceToCancel);
                    seanceToCancel--; // to fit array
                    if (seanceToCancel < 0 || seanceToCancel >= movieListCounter)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                    seanceList[seanceToCancel] = null;
                    for (int x = seanceToCancel; x <= seanceCounter; x++)
                    {
                        seanceList[x] = seanceList[x + 1];
                    }
                    seanceCounter--;
                    Console.WriteLine("Seance was successfully cancelled");
                    SortSeanceList();
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Wrong number!");
                }
            }
            private static bool SeanceCollisionCheck(int seanceNumber)
            {
                bool noCollide = true;
                for (int x = 0; x < seanceCounter; x++)
                {
                    if (x == seanceNumber)
                    {
                        continue;
                    }
                    if ((seanceList[x].seanceStart < seanceList[seanceNumber].seanceStart && seanceList[x].seanceEnd > seanceList[seanceNumber].seanceStart)
                        || (seanceList[x].seanceStart < seanceList[seanceNumber].seanceEnd && seanceList[x].seanceEnd > seanceList[seanceNumber].seanceEnd)
                        || (seanceList[seanceNumber].seanceStart < seanceList[x].seanceStart && seanceList[seanceNumber].seanceEnd > seanceList[x].seanceEnd))
                    {
                        noCollide = false;
                        Console.WriteLine("Seance collides with seance number {0}", x+1);
                    }
                }
                return noCollide;
            }

            public static void ShowReservations()
            {
                try
                {
                    Console.Write("Pick a seance to show: ");
                    int seanceNumber = int.Parse(Console.ReadLine());
                    seanceNumber--;
                    ShowReservations(seanceNumber);
                }
                catch (FormatException)
                {
                    Console.WriteLine("That's not a number!");
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Wrong number!");
                }
                catch (NullReferenceException)
                {
                    Console.WriteLine("Seance with this number doesn't exist yet!");
                }
                
            }

            public static void ShowReservations(int seanceNumber)
            {
                
                for (int x = 0; x < seanceList[seanceNumber].cinemaHall.GetLength(0); x++)
                {
                    Console.Write("{0, -2}", x + 1);
                    for (int y = 0; y < seanceList[seanceNumber].cinemaHall.GetLength(1); y++)
                    {
                        Console.Write("{0,2}", seanceList[seanceNumber].cinemaHall[x, y]);
                    }
                    Console.WriteLine();
                }
            }

            public static void ManipulateReservations(int seanceNumber, char changer)
            {
                // Spaghetti time!
                ShowReservations(seanceNumber);
                Console.Write("\nPick how many seats you want to reserve/cancel: ");
                int seatsToReserve;
                bool isANumber = int.TryParse(Console.ReadLine(), out seatsToReserve);
                isANumber = isANumber && seatsToReserve > 0 && seatsToReserve <= Movie.Seance.seanceList[seanceNumber].cinemaHall.GetLength(1);
                if (isANumber)
                {
                    int row;
                    Console.Write("Select a row: ");
                    bool isARow = int.TryParse(Console.ReadLine(), out row);
                    isARow = isARow && row > 0 && row <= Movie.Seance.seanceList[seanceNumber].cinemaHall.GetLength(0);
                    if (isARow)
                    {
                        row--; // doing it now so i don't have to swap this later.
                        int column;
                        Console.WriteLine("Select first seat (counting from left): ");
                        bool isAColumn = int.TryParse(Console.ReadLine(), out column);
                        isAColumn = isAColumn && column > 0 && column <= Movie.Seance.seanceList[seanceNumber].cinemaHall.GetLength(1);
                        if (isAColumn)
                        {
                            column--;
                            int narrowing = row - 8;
                            if (narrowing < 0)
                            {
                                narrowing = 0;
                            }
                            if (seatsToReserve + column + narrowing < Movie.Seance.seanceList[seanceNumber].cinemaHall.GetLength(1) - narrowing + 1)
                            {
                                for (int x = 0; x < seatsToReserve; x++)
                                {
                                    seanceList[seanceNumber].cinemaHall[row, column + x + narrowing] = changer;
                                }
                                Console.WriteLine("Reservations successfully created");
                            }
                            else
                            {
                                Console.WriteLine("Cannot make this reservation/reservation cancel - number of seats to reserve is too big for this column");
                            }
                        }
                        else
                        {
                            Console.WriteLine("That's not a valid number!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("That's not a valid number!");
                    }
                }
                else
                {
                    Console.WriteLine("That's not a valid number!");
                }
            }
        }
    }
}
