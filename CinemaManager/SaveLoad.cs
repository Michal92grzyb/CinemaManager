using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CinemaManager
{
    static class SaveLoad
    {
        internal static void Save()
        {
            String fileName = "save.txt";
            StreamWriter save = new StreamWriter(fileName);

            using (save)
            {
                int counter = 0;
                while (Movie.movieList[counter] != null)
                {
                    save.WriteLine("{0}|{1}|{2}", 
                        Movie.movieList[counter].Name,
                        Movie.movieList[counter].LenghtInMinutes, 
                        Movie.movieList[counter].Type);
                    counter++;
                }
                save.WriteLine("Seance");
                counter = 0;
                while (Movie.Seance.seanceList[counter] != null)
                {
                    save.WriteLine("{0}|{1}", 
                        Movie.Seance.seanceList[counter].Parent.Name,
                        Movie.Seance.seanceList[counter].SeanceStart);
                    for (int x = 0; x < Movie.Seance.seanceList[counter].cinemaHall.GetLength(0); x++)
                    {
                        for (int y = 0; y < Movie.Seance.seanceList[counter].cinemaHall.GetLength(1); y++)
                        {
                            save.Write("{0}", Movie.Seance.seanceList[counter].cinemaHall[x, y]);
                        }
                        save.WriteLine();
                    }
                    counter++;
                }
            }
        }
        internal static void Load()
        {
            try
            {
                string fileName = "save.txt";
                StreamReader read = new StreamReader(fileName);

                using (read)
                {
                    string line = "empty";
                    line = read.ReadLine();
                    while (line != "Seance")
                    {
                        int index1 = line.IndexOf('|');
                        int index2 = line.IndexOf('|', index1 + 1);
                        string name = line.Substring(0, index1);
                        int lenghtInMinutes = int.Parse(line.Substring(index1 + 1, index2 - index1 - 1));
                        string type = line.Substring(index2 + 1);
                        Movie.AddMovie(name, lenghtInMinutes, type);
                        line = read.ReadLine();
                    }
                    line = read.ReadLine();
                    while (line != null)
                    {
                        int index1 = line.IndexOf('|');
                        string name = line.Substring(0, index1);
                        DateTime startingDate = DateTime.Parse(line.Substring(index1 + 1));
                        int movieNum = FindMovieNumber(name);
                        Movie.Seance.AddSeance(movieNum, startingDate);
                        line = read.ReadLine();
                        for (int x = 0; x < Movie.Seance.seanceList[Movie.Seance.seanceCounter - 1].cinemaHall.GetLength(0); x++)
                        {
                            for (int y = 0; y < Movie.Seance.seanceList[Movie.Seance.seanceCounter - 1].cinemaHall.GetLength(1); y++)
                            {
                                char toPut = line[y];
                                Movie.Seance.seanceList[Movie.Seance.seanceCounter - 1].cinemaHall[x, y] = toPut;
                            }
                            line = read.ReadLine();
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Could not find save file, program will start with no movies and seances.");
            }
        }
        private static int FindMovieNumber(string name)
        {
            int returnValue = 0;
            for(int x = 0; x<Movie.movieListCounter; x++)
            {
                if (Movie.movieList[x].Name == name)
                {
                    returnValue = x;
                    break;
                }
            }
            return returnValue;
        }
    }
}
