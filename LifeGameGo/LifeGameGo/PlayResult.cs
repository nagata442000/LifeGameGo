using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGameGo
{
    class PlayResult
    {
        public PointSet valid;
        public PointSet invalid;

        public PlayResult(Board board)
        {
            valid = new PointSet();
            invalid = new PointSet();
            Point p = new Point();

            for(int x=0;x<Info.N;++x)
            {
                for (int y = 0; y < Info.N; ++y)
                {
                    p.x = x;
                    p.y = y;
                    Info.GameState s = board.test_play(p);
                    switch(s)
                    {
                        case Info.GameState.VALID_MOVE:
                            valid.set(p);
                            break;
                        case Info.GameState.INVALID_MOVE:
                            invalid.set(p);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
