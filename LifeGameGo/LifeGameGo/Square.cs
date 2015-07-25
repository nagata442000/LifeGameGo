using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGameGo
{
    public class Square : System.Windows.Forms.PictureBox
    {
        public int x;
        public int y;

        public Square(int a,int b)
        {
            x = a;
            y = b;
        }
    }
}
