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

        public void LoadFrom(string folder)
        {
            Users = User.ReadUsers(Path.Combine(folder, "users.dat"));
            Movies = Movie.ReadMovies(Path.Combine(folder, "movies.dat"));
            User.ReadRatings(Users, Movies, Path.Combine(folder, "ratings.dat"));
        }

        public double DistanceNorm2(User u1, User u2)
        {
            if (u1 == null || u2 == null) { throw new NullReferenceException("Hell mother of goat !"); }
            if (u1 == u2 && u1.Ratings.Count == 0) { return 0; }

            bool haveCommonMovie = false;
            double distance = 0;

            foreach (var r in u1.Ratings)
            {
                Movie mU1 = r.Key;
                int ratingU1 = r.Value;

                int ratingU2;
                if (u2.Ratings.TryGetValue(mU1, out ratingU2))
                {
                    haveCommonMovie = true;
                    distance += Math.Pow(ratingU1 - ratingU2, 2);
                }
            }

            return haveCommonMovie ? Math.Sqrt(distance) : Double.NaN;
        }

        public double Similarity(User u1, User u2)
        {
            var distance = DistanceNorm2(u1, u2);
            if (Double.IsNaN(distance))
            {
                return 0;
            }
            return 1 / (1 + distance);
        }
    }
}
