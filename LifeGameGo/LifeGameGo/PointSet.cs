using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGameGo
{
    public class PointSet
    {
        private long[] table;
        public PointSet(long a, long b)
        {
            table = new long[] { a, b };
        }

        public PointSet()
        {
            table = new long[] { 0, 0 };
        }

        public PointSet(PointSet p)
        {
            table = new long[] { p.table[0], p.table[1] };
        }

        public bool get(Point p)
        {
            return ((table[p.y & 1] >> (p.x + (p.y >> 1) * 10)) & 1)!=0 ;
        }

        public void set(Point p)
        {
            if (p.x < 0 || p.x >= Info.N || p.y < 0 || p.y >= Info.N)
            {
                return;
            }

            table[p.y & 1] |= 1L << (p.x + (p.y >> 1) * 10);
        }

        public int size()
        {
            long l1, l2;
            l1 = table[0];
            l2 = table[1];
            l1 = (l1 & 0x5555555555555555L) + ((l1 >> 1) & 0x5555555555555555L);
            l2 = (l2 & 0x5555555555555555L) + ((l2 >> 1) & 0x5555555555555555L);
            l1 = (l1 & 0x3333333333333333L) + ((l1 >> 2) & 0x3333333333333333L);
            l2 = (l2 & 0x3333333333333333L) + ((l2 >> 2) & 0x3333333333333333L);
            l1 = (l1 & 0x0f0f0f0f0f0f0f0fL) + ((l1 >> 4) & 0x0f0f0f0f0f0f0f0fL);
            l2 = (l2 & 0x0f0f0f0f0f0f0f0fL) + ((l2 >> 4) & 0x0f0f0f0f0f0f0f0fL);
            l1 = (l1 & 0x00ff00ff00ff00ffL) + ((l1 >> 8) & 0x00ff00ff00ff00ffL);
            l2 = (l2 & 0x00ff00ff00ff00ffL) + ((l2 >> 8) & 0x00ff00ff00ff00ffL);
            l1 = (l1 & 0x0000ffff0000ffffL) + ((l1 >>16) & 0x0000ffff0000ffffL);
            l2 = (l2 & 0x0000ffff0000ffffL) + ((l2 >>16) & 0x0000ffff0000ffffL);
            l1 = (l1 & 0x00000000ffffffffL) + ((l1 >>32) & 0x00000000ffffffffL);
            l2 = (l2 & 0x00000000ffffffffL) + ((l2 >>32) & 0x00000000ffffffffL);
            return (int)(l1 + l2);
        }

        public static PointSet operator&(PointSet a,PointSet b)
        {
            PointSet res = new PointSet();
            res.table[0] = a.table[0] & b.table[0];
            res.table[1] = a.table[1] & b.table[1];
            return res;
        }
        public static PointSet operator |(PointSet a, PointSet b)
        {
            PointSet res = new PointSet();
            res.table[0] = a.table[0] | b.table[0];
            res.table[1] = a.table[1] | b.table[1];
            return res;
        }
        public static PointSet operator -(PointSet a, PointSet b)
        {
            PointSet res = new PointSet();
            res.table[0] = a.table[0] & (~b.table[0]);
            res.table[1] = a.table[1] & (~b.table[1]);
            return res;
        }

        public List<Point> toList()
        {
            List<Point> res = new List<Point>();
            for(int x=0;x<Info.N;++x)
            {
                for (int y = 0; y < Info.N; ++y)
                {
                    Point p = new Point(x, y);
                    if(get(p))
                    {
                        res.Add(p);
                    }
                }
            }
            return res;
        }

        public static PointSet[,] around;

    }
}
