using System;

namespace L2_U2_9
{
    /// Klasė saugoti duomenis apie filmą
    class Movie
    {
        // Filmo pavadinimas
        public string Name { get; set; }
        // Filmo išleidimo metai 
        public int Release { get; set; }
        // Filmo žanras
        public string Genre { get; set; }
        // Filmą sukūrios studijos pavadinimas
        public string Studio { get; set; }
        // Filmą sukūrusio direktoriaus vardas
        public string Director { get; set; }
        // Filme vaidines aktorius
        public string Actor1 { get; set; }
        // Filme vaidines aktorius
        public string Actor2 { get; set; }
        // Filmo pelnas
        public double Profit { get; set; }

        /// <summary>
        /// Konstruktrorius su klasės savybėmis
        /// </summary>
        public Movie(string name, int release, string genre, string studio, string director, string actor1, string actor2, double profit)
        {
            Name = name;
            Release = release;
            Genre = genre;
            Studio = studio;
            Director = director;
            Actor1 = actor1;
            Actor2 = actor2;
            Profit = profit;
        }
        public override string ToString()
        {
            return $"{Name,2};{Release,2};{Genre,2};{Studio,2};{Director,2};{Actor1,2};{Actor2,2};{Profit,2}\u20AC";
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Movie other))
            {
                return false;
            }
            return Name == other.Name &&
                   Release == other.Release &&
                   Genre == other.Genre &&
                   Studio == other.Studio &&
                   Director == other.Director &&
                   Actor1 == other.Actor1 &&
                   Actor2 == other.Actor2 &&
                   Math.Abs(Profit - other.Profit) < 0.1;
        }

        public override int GetHashCode()
        {
            return Genre.GetHashCode() ^ Studio.GetHashCode() ^ Director.GetHashCode() ^ Actor1.GetHashCode() ^ Actor2.GetHashCode() ^ Profit.GetHashCode();
        }

        public static bool operator ==(Movie lhs, Movie rhs)
        {
            if (Object.ReferenceEquals(lhs, null))
            {
                if (Object.ReferenceEquals(rhs, null))
                {
                return true;
                }
                return false;
            }

            return lhs.Equals(rhs);
        }

        public static bool operator !=(Movie lhs, Movie rhs)
        {
            return !(lhs == rhs);
        }
    }
}
