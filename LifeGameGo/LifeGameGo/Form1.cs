using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LifeGameGo
{
    public partial class Form1 : Form
    {
        int turn;
        public Square[,] square;
        Board board;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Init.init();
            board = new Board();
            square = new Square[Info.N, Info.N];
            for(int x=0;x<Info.N;++x)
            {
                for (int y = 0; y < Info.N; ++y)
                {
                    square[x, y] = new Square(x, y);
                    square[x, y].Size = new Size(30, 30);
                    square[x, y].Location = new System.Drawing.Point((x + 1) * 30, (y + 1) * 30);
                    square[x, y].Click += new EventHandler(square_click);
                    this.Controls.Add(square[x, y]);
                }
            }
        }

        private void square_click(Object sender, EventArgs e)
        {
            Square s = (Square)sender;
            Info.GameState r = board.play(new Point(s.x, s.y));
            Refresh();
        }


        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            for(int x=0;x<Info.N;++x)
            {
                for (int y = 0; y < Info.N; ++y)
                {
                    LifeGameGo.Point p = new LifeGameGo.Point(x, y);
                    Info.Stones st = board.get(p);
                    if (st == Info.Stones.BLACK)
                    {
                        if (board.points.Count > 0 && board.points.Last() == p)
                        {
                            square[x, y].Image = Properties.Resources.blacklast;
                        }
                        else
                        {
                            square[x, y].Image = Properties.Resources.black;
                        }
                    }
                    else if (st == Info.Stones.WHITE)
                    {
                        if (board.points.Count>0 && board.points.Last() == p)
                        {
                            square[x, y].Image = Properties.Resources.whitelast;
                        }
                        else
                        {
                            square[x, y].Image = Properties.Resources.white;
                        }
                    }
                    else
                    {
                        if (x == 0)
                        {
                            if (y == 0)
                            {
                                square[x, y].Image = Properties.Resources.lefttop;
                            }
                            else if (y == Info.N - 1)
                            {
                                square[x, y].Image = Properties.Resources.leftbottom;
                            }
                            else
                            {
                                square[x, y].Image = Properties.Resources.left;
                            }
                        }
                        else if (x == Info.N - 1)
                        {
                            if (y == 0)
                            {
                                square[x, y].Image = Properties.Resources.righttop;
                            }
                            else if (y == Info.N - 1)
                            {
                                square[x, y].Image = Properties.Resources.rightbottom;
                            }
                            else
                            {
                                square[x, y].Image = Properties.Resources.right;
                            }
                        }
                        else
                        {
                            if (y == 0)
                            {
                                square[x, y].Image = Properties.Resources.top;
                            }
                            else if (y == Info.N - 1)
                            {
                                square[x, y].Image = Properties.Resources.bottom;
                            }
                            else
                            {
                                square[x, y].Image = Properties.Resources.center;
                            }
                        }
                    }
                }
            }
        }
    }
}
