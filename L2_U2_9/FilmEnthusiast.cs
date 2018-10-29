using System.Collections.Generic;

namespace L2_U2_9
{
    class FilmEnthusiast
    {
        //Kino megėjo vardas
        public string FilmEnthusiastName { get; set; }
        //Kino megėjo pavardė
        public string FilmEnthusiastSurname { get; set; }
        //Kino megėjo gimimo metai
        public string YearOfBirth { get; set; }
        //Kino megėjo miestas
        public string City { get; set; }
        //Filmų konteineris
        public MoviesContainer MoviesContainer { get; set; }
    }
}
