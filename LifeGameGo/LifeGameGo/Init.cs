using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGameGo
{
    public class Init
    {
        public static void init()
        {
            PointSet.around = new PointSet[Info.N, Info.N];
            for(int x=0;x<Info.N;++x)
            {
                for(int y=0;y<Info.N;++y)
                {
                    PointSet.around[x, y] = new PointSet();
                    Point p = new Point();
                    p.x = x-1;
                    p.y = y-1;
                    PointSet.around[x, y].set(p);
                    p.x = x-1;
                    p.y = y;
                    PointSet.around[x, y].set(p);
                    p.x = x-1;
                    p.y = y+1;
                    PointSet.around[x, y].set(p);
                    p.x = x;
                    p.y = y-1;
                    PointSet.around[x, y].set(p);
                    p.x = x;
                    p.y = y+1;
                    PointSet.around[x, y].set(p);
                    p.x = x+1;
                    p.y = y-1;
                    PointSet.around[x, y].set(p);
                    p.x = x+1;
                    p.y = y;
                    PointSet.around[x, y].set(p);
                    p.x = x+1;
                    p.y = y+1;
                    PointSet.around[x, y].set(p);
                }
            }
        }
    }
}
