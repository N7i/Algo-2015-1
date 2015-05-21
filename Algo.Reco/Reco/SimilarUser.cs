using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algo
{
    public struct SimilarUser
    {
        public readonly User User;
        public readonly double Similarity;

        public SimilarUser(User user, double similarity)
        {
            User = user;
            Similarity = similarity;
        }
    }
}
