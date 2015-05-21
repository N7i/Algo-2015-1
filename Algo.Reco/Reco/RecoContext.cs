using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Algo
{
    public class RecoContext
    {
        public User[] Users { get; private set; }
        public Movie[] Movies { get; private set; }

        public void LoadFrom( string folder )
        {
            Users = User.ReadUsers( Path.Combine( folder, "users.dat" ) );
            Movies = Movie.ReadMovies( Path.Combine( folder, "movies.dat" ) );
            User.ReadRatings( Users, Movies, Path.Combine( folder, "ratings.dat" ) );
        }

        public double Distance(User u1, User u2)
        {
            if (u1 == null || u2 == null) { throw new NullReferenceException("Hell mother of goat !"); }

            var joinRatedFilm = u1.Ratings.Keys.Where(m =>
            {
                int rating;
                return u2.Ratings.TryGetValue(m, out rating);
            }).ToList();

            if (joinRatedFilm.Count != 0)
            {
                double distance = 0;

                foreach (Movie m in joinRatedFilm)
                {
                    int rateU1;
                    int rateU2;

                    u1.Ratings.TryGetValue(m, out rateU1);
                    u2.Ratings.TryGetValue(m, out rateU2);

                    distance += Math.Max(rateU1, rateU2);
                }

                return Math.Sqrt(distance);
            }

            return Double.NaN;
        }
    }
}
