using System;
using System.Collections.Generic;
using System.IO;

namespace L1_U1_9
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            List<Movie> movies = p.ReadMoviesData();
            // int testRelease = 2014;
            // string actorName = "Nicolas Cage";
            p.PrintSaveReportToFile(movies);
            p.PrintSaveReportToTxt(movies);

            Console.WriteLine("Si programa atlieka 4 skirtingus veiksmus su IMDB filmu sarasu, kuris yra laikomas programos aplankale");
            Console.WriteLine();
            Console.ReadKey();

            // Daugiausio penlo gavusio filmo(ų) pajieška
            Console.WriteLine("Programos 1 Dalis");
            Console.WriteLine("Programa suras daugiausia pelno gavusius filmus jusu pasirinktais metais");
            Console.WriteLine();
            Console.ReadKey();
            Console.WriteLine("Irasykite Metus, kuriu norite surasti populiariausius filmus");
            Console.WriteLine("Pvz.: 2014");
            Console.WriteLine("Jeigu paliskite tuscia, naudosime pavyzdi");
            var testRelease = p.ConsoleWhatYear();
            p.PrintProfitMovies(p.FindProfitMovies(movies, p.FindHighProfit(movies, testRelease)));
            Console.ReadKey();

            // Daugiausiai filmų sukūrusių direktorių pajieška
            List<Director> directors = p.GetDirector(movies);
            var maxPopular = p.FindMostPopularDirectorMovieAmount(directors);
            Console.WriteLine("Programos 2 dalis");
            Console.WriteLine("Programa suras kiek daugiausia filmu populiariausi rezisieriai sukure ir visus atspausdins");
            Console.WriteLine();
            Console.ReadKey();
            p.PrintDirector(p.FindByPopularDirectors(directors, maxPopular), maxPopular);
            Console.ReadKey();

            // Filmų sarašo sudarymas kuriame vaidino tam tikras aktorius
            Console.WriteLine("Programos 3 dalis");
            Console.WriteLine("Programa suras kiek filmus kuriuose vaidino jusu pasirinktas aktorius");
            Console.WriteLine();
            Console.ReadKey();
            Console.WriteLine("Irasykite aktoriu kurio filmus norite surasti");
            Console.WriteLine("Rasymo Formatas : Vardas (pirma r. didzioji), tarpas, pavarde (pirma r. didzioji)");
            Console.WriteLine("Pvz.:Nicolas Cage");
            Console.WriteLine("Jeigu paliskite tuscia, naudosime pavyzdi");
            var actorName = p.ConsoleWhatActor();
            p.PrintActorMovies(p.FindActorMovies(movies, actorName));
            Console.WriteLine("Programa sukure Cage.csv faila programos aplankale");
            Console.ReadKey();
            Console.WriteLine();

            //  Visų unikalių žanrų pajieška
            Console.WriteLine("Programos 4 dalis");
            Console.WriteLine("Programa suras visus zanrus jusu IMDB sarase ir issaugos faile");
            Console.WriteLine();
            Console.ReadKey();
            p.PrintStringList(p.GetGenre(movies));
            Console.WriteLine("Programa sukure Žanrai.csv programos aplankale");
            Console.WriteLine();
        }

        /// <summary>
        /// Atspausdina pradinius duomenius i rezultatų failą
        /// </summary>
        /// <param name="movies">Filmų sarašas</param>
        void PrintSaveReportToFile(List<Movie> movies)
        {
            string[] lines = new string[movies.Count];
            for (int i = 0; i < movies.Count; i++)
            {
                lines[i] = String.Format("{0},{1},{2},{3},{4},{5},{6},{7}",
                                         movies[i].Name, movies[i].Release, movies[i].Genre, movies[i].Studio,
                                         movies[i].Director, movies[i].Actor1, movies[i].Actor2, movies[i].Profit);
            }
            File.WriteAllLines(@"SavedData.csv", lines);
        }

        /// <summary>
        /// Atspausdina pradinius duomenis į rezultatų failą
        /// </summary>
        /// <param name="movies">Filmų sarašas</param>
        void PrintSaveReportToTxt(List<Movie> movies)
        {
            string[] lines = new string[movies.Count + 4];
            int tableWidth = 218;
            string tableLine = new string('-', tableWidth);
            lines[0] = tableLine;
            lines[1] = String.Format("| {0, -40} | {1,-14} | {2,-30} | {3,-35} | {4,-20} | {5,-20} | {6,-20} | {7,-14} |",
                         "Pavadinimas", "Leidimo metai", "Žanras", "Studija", "Direktorius", "Aktorius 1", "Aktorius 2", "Pelnas");
            lines[2] = tableLine;
            for (int i = 0; i < movies.Count; i++)
            {
                lines[i + 3] = String.Format("| {0, -40} | {1,14} | {2,-30} | {3,-35} | {4,-20} | {5,-20} | {6,-20} | {7,14} |",
                         movies[i].Name, movies[i].Release, movies[i].Genre, movies[i].Studio,
                         movies[i].Director, movies[i].Actor1, movies[i].Actor2, movies[i].Profit);
            }
            lines[movies.Count + 3] = tableLine;
            File.WriteAllLines(@"Data.txt", lines);
        }

        /// <summary>
        ///skaito duomenis iš csv failo
        /// </summary>
        /// <returns>filmų sarašą</returns>
        List<Movie> ReadMoviesData()
        {
            List<Movie> movies = new List<Movie>();

            string[] lines = File.ReadAllLines(@"L1-9.csv");
            foreach (string line in lines)
            {
                string[] values = line.Split(',');
                var name = values[0];
                var release = int.Parse(values[1]);
                var genre = values[2];
                var studio = values[3];
                var director = values[4];
                var actor1 = values[5];
                var actor2 = values[6];
                var profit = double.Parse(values[7]);
                Movie movie = new Movie(name, release, genre, studio, director, actor1, actor2, profit);
                movies.Add(movie);
            }
            return movies;
        }

        /// <summary>
        /// Nuskaitomi testavimo metai is konsolės
        /// </summary>
        /// <returns>testavimo metai</returns>
        int ConsoleWhatYear()
        {
            string input = Console.ReadLine();
            int testRelease = 0;
            if (!int.TryParse(input, out testRelease))
            {
                testRelease = 2014;
            }
            return testRelease;
        }

        /// <summary>
        /// Iš sarašo movies yra surandamas didžiausias profit elementų, kurių Release = testRelease
        /// </summary>
        /// <param name="movies">Filmų sarašas</param>
        /// <param name="testRelease">Testavimo metai</param>
        /// <returns>didziausias pelnas</returns>
        double FindHighProfit(List<Movie> movies, int testRelease)
        {
            var maxProfit = 0.0;
            foreach (Movie movie in movies)
            {
                if (movie.Profit > maxProfit && movie.Release == testRelease)
                {
                    maxProfit = movie.Profit;
                }
            }
            return maxProfit;
        }

        /// <summary>
        /// Suranda sarašo movies elementus kurie Profit = MaxProfit ir juos prideda prie naujo sarašo profitMovies
        /// </summary>
        /// <param name="movies">Filmų sarašas</param>
        /// <param name="profit">Testavimo pelnas</param>
        /// <returns>sarašas pelningų filmų</returns>
        List<Movie> FindProfitMovies(List<Movie> movies, double profit)
        {
            List<Movie> profitMovies = new List<Movie>();
            foreach (Movie movie in movies)
            {
                if (movie.Profit == profit)
                {
                    profitMovies.Add(movie);
                }
            }
            return profitMovies;
        }

        /// <summary>
        /// Atspausdinamas į ekraną pateikto sarašo tam tikrus duomenis
        /// </summary>
        /// <param name="movies">Filmų sarašas</param>
        void PrintProfitMovies(List<Movie> movies)
        {
            Console.WriteLine("Daugiausia pelno surinke filmai jusu pasirenktu metu");
            foreach (Movie movie in movies)
            {
                Console.WriteLine("Pavadinimas : {0} ", movie.Name);
                Console.WriteLine("Režisierius : {0}", movie.Genre);
                Console.WriteLine("Filmo pelnas : {0}", movie.Profit);
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Sukuriama naujas Sarašas directors (Director klasės tipo) į kurį išsaugomi direktorių vardai iš sarašo movies, 
        /// o vardo pasikartojimo metu naujas sarašo elementas nekuriamas o pridedama prie esančio saraše pasikartojimo rodiklio amount
        /// </summary>
        /// <param name="movies">Filmų sarašas</param>
        /// <returns>sarašas direktorių</returns>
        List<Director> GetDirector(List<Movie> movies)
        {
            List<Director> directors = new List<Director>();
            foreach (Movie movie in movies)
            {
                var test = 0;
                foreach (Director director in directors)
                {
                    if (director.Name == movie.Director)
                    {
                        director.Amount++;
                        test++;
                    }
                }
                if (test == 0)
                {
                    var temporaryName = movie.Director;
                    Director director = new Director(temporaryName, 1);
                    directors.Add(director);
                }
            }
            return directors;
        }

        /// <summary>
        /// Suranda didžiausią pasikartojimo rodikli
        /// </summary>
        /// <param name="directors">Direktorių sarašas</param>
        /// <returns>didžiausias kiekis</returns>
        int FindMostPopularDirectorMovieAmount(List<Director> directors)
        {
            int maxPopular = 0;
            foreach (Director director in directors)
            {
                if (director.Amount > maxPopular) maxPopular = director.Amount;
            }
            return maxPopular;
        }

        /// <summary>
        /// Sudaromas sarašas direktorių kurių ammount = maxPopular
        /// </summary>
        /// <param name="directors">Direktorių sarašas</param>
        /// <param name="popular">Testavimo rodiklis</param>
        /// <returns>sarašas populiarių direktorių</returns>
        List<Director> FindByPopularDirectors(List<Director> directors, int popular)
        {
            List<Director> popularDirectors = new List<Director>();
            foreach (Director director in directors)
            {
                if (director.Amount == popular)
                {
                    popularDirectors.Add(director);
                }
            }
            return popularDirectors;
        }

        /// <summary>
        /// Atspausdina vardus pateikto direktoriaus sarašo
        /// </summary>
        /// <param name="popularDirectors">Direktorių sarašas</param>
        /// <param name="popular"></param>
        void PrintDirector(List<Director> popularDirectors, int popular)
        {
            Console.WriteLine("Populiaurisi direktoriai sukure tiek filmu : {0}", popular);
            Console.WriteLine();
            Console.WriteLine("Šie direktoriai sukurė daugiausia filmų : ");
            foreach (Director director in popularDirectors)
            {
                Console.WriteLine("{0}", director.Name);
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Nuskaitomas pajieskos vardas
        /// </summary>
        /// <returns>pajieskos vardas</returns>
        string ConsoleWhatActor()
        {
            var actorName = Console.ReadLine();
            if (string.IsNullOrEmpty(actorName))
            {
                actorName = "Nicolas Cage";
            }
            return actorName;
        }

        /// <summary>
        /// Sudaromas sarašas kurių actor1 arba actor2 yra lygus actorname
        /// </summary>
        /// <param name="movies">Filmų sarašas</param>
        /// <param name="actorName">Testavimo rodiklis vardas</param>
        /// <returns>Atrinktas sarašas filmų</returns>
        List<Movie> FindActorMovies(List<Movie> movies, string actorName)
        {
            List<Movie> actorMovies = new List<Movie>();
            foreach (Movie movie in movies)
            {
                if (movie.Actor1 == actorName || movie.Actor2 == actorName)
                {
                    actorMovies.Add(movie);
                }
            }
            return actorMovies;
        }
        /// <summary>
        /// Spausdinami pateikto sarašo tam tikri elementai
        /// </summary>
        /// <param name="movies">Filmų sarašas</param>
        void PrintActorMovies(List<Movie> movies)
        {
            string[] lines = new string[movies.Count];
            for (int i = 0; i < movies.Count; i++)
            {
                lines[i] = string.Format("{0},{1},{2}", movies[i].Name, movies[i].Release, movies[i].Studio);
            }
            File.WriteAllLines(@"Cage.csv", lines);
        }
        /// <summary>
        /// Surandami visi unikalus žanrai pateikto sarašo 
        /// </summary>
        /// <param name="movies">Filmų sarašas</param>
        /// <returns>sarašas žanrų</returns>
        List<string> GetGenre(List<Movie> movies)
        {
            List<string> genres = new List<string>();
            foreach (Movie movie in movies)
            {
                if (!genres.Contains(movie.Genre))
                {
                    genres.Add(movie.Genre);
                }
            }
            return genres;
        }
        /// <summary>
        /// Atspausdinami visi pateikto sarašo elementai
        /// </summary>
        /// <param name="genres">Žanrų sarašas</param>
        void PrintStringList(List<string> genres)
        {
            File.WriteAllLines(@"Žanrai.csv", genres);
        }
    }
}
