using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2_U2_9
{
    //Filmų konteineris
    class MoviesContainer
    {
        private const int MaxMovies = 50;
        private Movie[] Movies { get; set; }
        public int Count { get; private set; }

        public MoviesContainer()
        {
            Movies = new Movie[MaxMovies];
        }

        public void AddMovie(Movie movie)
        {
            Movies[Count] = movie;
            Count++;
        }

        public Movie GetMovie(int index)
        {
            return Movies[index];
        }

        public bool Contains(Movie movie)
        {
            return Movies.Contains(movie);
        }
    }
}
