using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using L1_U1_9;

namespace L1_U1_9
{
    //Konteineris

    class FilmEnthusiastsContainer
    {
        private const int MaxFilmEnthusiats = 50;
        public FilmEnthusiast[] FilmEnthusiasts;
        public int Count { get; private set; }

        public FilmEnthusiastsContainer()
        {
            FilmEnthusiasts = new FilmEnthusiast[MaxFilmEnthusiats];
            Count = 0;
        }

        public void AddFilmEnthusiast(FilmEnthusiast filmEnthusiast)
        {
            FilmEnthusiasts[Count++] = filmEnthusiast;
        }

        public void AddFilmEnthusiast(FilmEnthusiast filmEnthusiast, int index)
        {
            FilmEnthusiasts[index] = filmEnthusiast;
        }

        public FilmEnthusiast GetFilmEnthusiast(int index)
        {
            return FilmEnthusiasts[index];
        }

        public bool Contains(FilmEnthusiast filmEnthusiast)
        {
            return FilmEnthusiasts.Contains(filmEnthusiast);
        }
    }
}
