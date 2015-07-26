using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGameGo
{
    public class RandomAI : AI
    {
        private Random rnd;
        public RandomAI()
        {
            rnd = new Random();
        }

        public Point play(Board board)
        {
            PlayResult r = new PlayResult(board);
            List<Point> l = r.valid.toList();
            if(l.Count ==0)
            {
                return Point.Pass;
            }
            return l[rnd.Next(l.Count)];
        }

    }
}
