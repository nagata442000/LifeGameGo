using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGameGo
{
    class Attack
    {
        public PointSet[,] attack;

        public Attack(Board board)
        {
            attack = new PointSet[2, 3];
            for (int i = 0; i < 2; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    attack[i, j] = new PointSet();
                }
            }

            for (int x = 0; x < Info.N; ++x)
            {
                for (int y = 0; y < Info.N; ++y)
                {
                    Point p = new Point(x, y);
                    PointSet ps = PointSet.around[x, y] & board.stones[0];
                    int s = ps.size();
                    if (s<=2)
                    {
                        attack[0,s].set(p);
                    }

                    ps = PointSet.around[x, y] & board.stones[1];
                    s = ps.size();
                    if (s <= 2)
                    {
                        attack[1, s].set(p);
                    }
                }
            }
        }
    }
}
