namespace L1_U1_9
{
    /// <summary>
    /// Klasė saugoti duomenis apie filmą
    /// </summary>
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
    }
}
