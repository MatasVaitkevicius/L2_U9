using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace L2_U2_9
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("IFF-8/8 Matas Vaitkevičius L2-U2-09");
            Console.ReadKey();

            //Skaitomi failai
            var filePaths = Directory.GetFiles(Directory.GetCurrentDirectory(), "L2*.csv");
            var filmEnthusiastsContainer = new FilmEnthusiastsContainer();
            foreach (var path in filePaths)
            {
                filmEnthusiastsContainer.AddFilmEnthusiast(ReadFilmEnthusiastData(path));
            }

            //Duomenys sudedami į lentelę
            CreateAllReportTables(filmEnthusiastsContainer);
            Console.WriteLine();

            //Kiekvieno kino megėjo mėgstamiausias režisierius
            PrintFavoriteDirector(filmEnthusiastsContainer);
            Console.ReadKey();

            //Sudarytas filmų sąrašas, kuriuos peržiūrėjo visi kino megėjai ir įrašyta į csv failą
            Console.WriteLine("2.Sudarytas filmų sąrašas, kuriuos peržiūrėjo visi kino megėjai ir įrašyta į csv failą");
            Console.WriteLine();
            var uniqueMovies = FilterSeenMovies(filmEnthusiastsContainer);
            WriteMovieData(uniqueMovies, "MatėVisi.csv");
            Console.ReadKey();

            //Kiekvienam kino megėjui sudarytas rekomenduojamų peržiūrėti filmų sąrašas 
            Console.WriteLine("3.Kiekvienam kino megėjui sudarytas rekomenduojamų peržiūrėti filmų sąrašas ir įrašyta į csv failą");
            PrintRecommendedMovies(filmEnthusiastsContainer, uniqueMovies);
            Console.ReadKey();
        }

        /// <summary>
        /// Skaito kiekvieno kino megėjo informaciją ir sudeda informaciją į konteinerį
        /// </summary>
        /// <param name="file"> Failo pavadinimas </param>
        /// <returns>Kino megėjų konteinerį</returns>
        private static FilmEnthusiast ReadFilmEnthusiastData(string file)
        {
            FilmEnthusiast enthusiast;

            using (var reader = new StreamReader(file))
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                var filmEnthusiastName = values[0];
                var filmEnthusiastSurname = values[1];
                var yearOfBirth = reader.ReadLine();
                var city = reader.ReadLine();
                line = reader.ReadLine();
                enthusiast = new FilmEnthusiast
                {
                    FilmEnthusiastName = filmEnthusiastName,
                    FilmEnthusiastSurname = filmEnthusiastSurname,
                    YearOfBirth = yearOfBirth,
                    City = city,
                    MoviesContainer = new MoviesContainer()
                };
                while (line != null)
                {
                    values = line.Split(',');
                    var name = values[0];
                    var release = int.Parse(values[1]);
                    var genre = values[2];
                    var studio = values[3];
                    var director = values[4];
                    var actor1 = values[5];
                    var actor2 = values[6];
                    var profit = double.Parse(values[7]);
                    enthusiast.MoviesContainer.AddMovie(new Movie(name, release, genre, studio, director, actor1, actor2, profit));
                    line = reader.ReadLine();
                }
            }
            return enthusiast;
        }

        /// <summary>
        /// Išspausdina kiekvieno kino megėjo mėgstamiausią režisierių
        /// </summary>
        /// <param name="filmEnthusiastsContainer"></param>
        private static void PrintFavoriteDirector(FilmEnthusiastsContainer filmEnthusiastsContainer)
        {
            Console.WriteLine("1.Kiekvieno kino megėjo mėgstamiausi režisieriai");
            Console.WriteLine();
            for (int i = 0; i < filmEnthusiastsContainer.Count; i++)
            {
                var favoriteDirector = FilmEnthusiastFavoriteDirector(filmEnthusiastsContainer.FilmEnthusiasts[i]);
                Console.WriteLine("| {0,7} | {1,9} | {2,11} |", "Vardas", "Pavardė", "Mėgstamiausias režisierius");
                Console.WriteLine($"{filmEnthusiastsContainer.FilmEnthusiasts[i].FilmEnthusiastName,9}  " +
                                  $"{filmEnthusiastsContainer.FilmEnthusiasts[i].FilmEnthusiastSurname,11}: {favoriteDirector,11}");
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Sukuria kino megėjų duomenų lentelę
        /// </summary>
        /// <param name="filmEnthusiast"></param>
        /// <param name="file"></param>
        private static void CreateReportTable(FilmEnthusiast filmEnthusiast, string file)
        {
            using (var writer = new StreamWriter(file))
            {
                writer.WriteLine("Kino megėjo sąrašas");
                writer.WriteLine(new string('-', 218));
                writer.WriteLine("| {0,7} | {1,9} | {2,12} | {3,9} |",
                    "Vardas", "Pavardė", "Gimimo metai", "Miestas");
                writer.WriteLine($"| {filmEnthusiast.FilmEnthusiastName,7} | {filmEnthusiast.FilmEnthusiastSurname,9} | " +
                                 $"{filmEnthusiast.YearOfBirth + "m.",-12 } | {filmEnthusiast.City,9} |");

                writer.WriteLine(new string('-', 218));
                writer.WriteLine(
                    "| {0, -40} | {1,-14} | {2,-30} | {3,-35} | {4,-20} | {5,-20} | {6,-20} | {7,-14} |",
                    "Pavadinimas", "Leidimo metai", "Žanras", "Studija", "Režisierius", "Aktorius 1", "Aktorius 2",
                    "Pelnas");
                writer.WriteLine(new string('-', 218));
                for (int i = 0; i < filmEnthusiast.MoviesContainer.Count; i++)
                {
                    writer.WriteLine($"| {filmEnthusiast.MoviesContainer.GetMovie(i).Name,-40} | {filmEnthusiast.MoviesContainer.GetMovie(i).Release,14} | " +
                                     $"{filmEnthusiast.MoviesContainer.GetMovie(i).Genre,-30} | {filmEnthusiast.MoviesContainer.GetMovie(i).Studio,-35} | " +
                                     $"{filmEnthusiast.MoviesContainer.GetMovie(i).Director,-20} | {filmEnthusiast.MoviesContainer.GetMovie(i).Actor1,-20} | " +
                                     $"{filmEnthusiast.MoviesContainer.GetMovie(i).Actor2,-20} | {filmEnthusiast.MoviesContainer.GetMovie(i).Profit + "\u20AC",14} |");
                }
                writer.WriteLine(new string('-', 218));
            }
        }

        /// <summary>
        /// Sukuria visiem duomenims duomenų lentelę
        /// </summary>
        /// <param name="filmEnthusiastsContainer"></param>
        private static void CreateAllReportTables(FilmEnthusiastsContainer filmEnthusiastsContainer)
        {
            for (var i = 0; i < filmEnthusiastsContainer.Count; i++)
            {
                CreateReportTable(filmEnthusiastsContainer.GetFilmEnthusiast(i), $"L2_{filmEnthusiastsContainer.GetFilmEnthusiast(i).FilmEnthusiastName}_DataTable.txt");
            }
        }

        /// <summary>
        /// Spausdina filmų duomenys
        /// </summary>
        /// <param name="moviesContainer"></param>
        /// <param name="file"></param>
        private static void WriteMovieData(MoviesContainer moviesContainer, string file)
        {
            using (var writer = new StreamWriter(file, false, Encoding.UTF8))
            {
                writer.WriteLine("Filmo pavadinimas;Leidimo metai;Žanras;Kino studija;Režisierius;Pirmasis aktorius;Antrasis Aktorius;Pelnas \u20AC");
                for (int i = 0; i < moviesContainer.Count; i++)
                {
                    writer.WriteLine(moviesContainer.GetMovie(i).ToString());
                }
            }
        }

        /// <summary>
        /// Įrašo rekomenduojamus filmus į csv failą
        /// </summary>
        /// <param name="filmEnthusiastsContainer"></param>
        /// <param name="uniqueMovies"></param>
        private static void PrintRecommendedMovies(FilmEnthusiastsContainer filmEnthusiastsContainer, MoviesContainer uniqueMovies)
        {
            for (var i = 0; i < filmEnthusiastsContainer.Count; i++)
            {
                var recommendedMovies = FilmEnthusiastsRecommendations(filmEnthusiastsContainer.FilmEnthusiasts[i].MoviesContainer, uniqueMovies);
                if (recommendedMovies.Count != 0)
                {
                    WriteMovieData(recommendedMovies,
                        $"Rekomendacija_{filmEnthusiastsContainer.FilmEnthusiasts[i].FilmEnthusiastName}_" +
                        $"{filmEnthusiastsContainer.FilmEnthusiasts[i].FilmEnthusiastSurname}.csv");
                }
            }
        }

        /// <summary>
        /// Suranda kiekvieno kino megėjo mėgstamiausią režisierių
        /// </summary>
        /// <param name="filmEnthusiast"> Kino mėgejai</param>
        /// <returns> Kino megėjo mėgstamiausią režisierių </returns>
        private static string FilmEnthusiastFavoriteDirector(FilmEnthusiast filmEnthusiast)
        {
            var favoriteDirector = new Dictionary<string, int>();
            for (var i = 0; i < filmEnthusiast.MoviesContainer.Count; i++)
            {
                if (favoriteDirector.ContainsKey(filmEnthusiast.MoviesContainer.GetMovie(i).Director))
                {
                    favoriteDirector[filmEnthusiast.MoviesContainer.GetMovie(i).Director]++;
                }
                else
                {
                    favoriteDirector.Add(filmEnthusiast.MoviesContainer.GetMovie(i).Director, 1);
                }
            }
            var maxValue = favoriteDirector.Values.Max();
            return favoriteDirector.FirstOrDefault(f => f.Value == maxValue).Key;
        }

        /// <summary>
        /// Suranda filmus, kuriuos peržiūrėjo visi kino megėjai
        /// </summary>
        /// <param name="filmEnthusiastsContainer"></param>
        /// <returns> Gražina filmus, kuriuos peržiūrėjo visi kino megėjai </returns>
        private static MoviesContainer FilterSeenMovies(FilmEnthusiastsContainer filmEnthusiastsContainer)
        {
            var filteredMovies = new MoviesContainer();
            var uniqueMovies = new HashSet<Movie>();
            for (var i = 0; i < filmEnthusiastsContainer.Count; i++)
            {
                for (int j = 0; j < filmEnthusiastsContainer.FilmEnthusiasts[i].MoviesContainer.Count; j++)
                {
                    uniqueMovies.Add(filmEnthusiastsContainer.FilmEnthusiasts[i].MoviesContainer.GetMovie(j));
                }
            }
            foreach (var uniqueMovie in uniqueMovies)
            {
                filteredMovies.AddMovie(uniqueMovie);
            }
            return filteredMovies;
        }

        /// <summary>
        /// Kino megėjui sudaro rekomenduojamų filmų sąrašą
        /// </summary>
        /// <param name="filmEnthusiastMovies"></param>
        /// <param name="uniqueMovies"></param>
        /// <returns> Rekomenduojamų filmų sąrašą </returns>
        private static MoviesContainer FilmEnthusiastsRecommendations(MoviesContainer filmEnthusiastMovies, MoviesContainer uniqueMovies)
        {
            var recommendationList = new MoviesContainer();

            for (int i = 0; i < uniqueMovies.Count; i++)
            {

                if (!filmEnthusiastMovies.Contains(uniqueMovies.GetMovie(i)))
                {
                    recommendationList.AddMovie(uniqueMovies.GetMovie(i));
                }
            }
            return recommendationList;
        }
    }
}
