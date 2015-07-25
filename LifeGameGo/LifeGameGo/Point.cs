using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGameGo
{
    public class Point
    {
        public int x;
        public int y;

        public Point()
        {
            x = 0;
            y = 0;
        }

        public Point(int a, int b)
        {
            x = a;
            y = b;
        }

        public static Point operator +(Point a, Point b)
        {
            Point r = new Point(a.x + b.x, a.y + b.y);
            return r;
        }

        public static Point operator -(Point a, Point b)
        {
            Point r = new Point(a.x - b.x, a.y - b.y);
            return r;
        }

        public static bool operator ==(Point a, Point b)
        {
            return a.x == b.x && a.y == b.y;
        }
        
        public static bool operator !=(Point a,Point b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return x + (y << 8);
        }

        public static Point Pass = new Point(-1, -1);
    }


}
