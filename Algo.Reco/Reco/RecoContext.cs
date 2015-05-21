using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Algo.Reco;

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

            return Math.Sqrt(distance);
            //return haveCommonMovie ? Math.Sqrt(distance) : Double.NaN;
        }

        public double DistancePearson(User u1, User u2)
        {
            double uX = 0;
            double uY = 0;
            double uXPow2 = 0;
            double uYPow2 = 0;
            int watchDog = 0;

            foreach(var r in u1.Ratings) {
                Movie mU1 = r.Key;
                int ratingU1 = r.Value;

                int ratingU2;
                if (u2.Ratings.TryGetValue(mU1, out ratingU2))
                {
                    uX += ratingU1;
                    uXPow2 += Math.Pow(ratingU1, 2);

                    uY += ratingU2;
                    uYPow2 += Math.Pow(ratingU2, 2);

                    watchDog++;
                }
            }


            if (watchDog < 2)
            {
                return 0;
            }

            // EAT THAT
            return ((uX + uY) - ((uX * uY) / watchDog)) / (Math.Sqrt(uXPow2 - (Math.Pow(uX, 2) / watchDog)) * Math.Sqrt(uYPow2 - (Math.Pow(uY, 2) / watchDog)));
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

        public List<Movie> GetRecoMovies(User u, int count)
        {
            SimilarUser[] similarUsers = GetSimilarUsers(u, 200);
        }

        private SimilarUser[] GetSimilarUsers(User u, int count)
        {
            
        }
    }

    public class BestKeeper<T> : IReadOnlyList<T> {

        private T[] _dataSet;

        public BestKeeper(int count, Func<T, T, int> comparator) {
            if (count < 0) { throw new ArgumentException("Count should be equal or superior at 0"); }

            _dataSet = new T[count];
        }

        public bool Add(T item) {
            int position = Array.BinarySearch(_dataSet, item);
        }

        int Count { get { return 0; } }

        public T this[int index]
        {
            get { throw new NotImplementedException(); }
        }

        int IReadOnlyCollection<T>.Count
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
