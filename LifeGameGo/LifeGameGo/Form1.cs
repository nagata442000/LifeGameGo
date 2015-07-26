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
        String black;
        String white;
        AI ai;
        bool on_game;
        bool ai_vs_human;
        public Square[,] square;
        Board board;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ai = new RandomAI();
            on_game = false;
            ai_vs_human = false;
            Init.init();
            board = new Board();
            listBox1.SelectedIndex = 0;
            listBox2.SelectedIndex = 0;
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
            if(!on_game)
            {
                return;
            }
            Square s = (Square)sender;
            Info.GameState r = board.play(new Point(s.x, s.y));
            if (r == Info.GameState.VALID_MOVE && ai_vs_human)
            {
                board.play(ai.play(board));
            }
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

        private void button1_Click(object sender, EventArgs e)
        {
            black = listBox1.SelectedItem.ToString();
            white = listBox2.SelectedItem.ToString();
            if(black == "AI" && white == "AI" )
            {
                MessageBox.Show("AI vs AI can't selected.");
                return;
            }
            board = new Board();
            if (black == "AI")
            {
                ai_vs_human = true;
                board.play(ai.play(board));
                Refresh();
            }
            if (white == "AI")
            {
                ai_vs_human = true;
            }
            on_game = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(!on_game)
            {
                return;
            }
            Info.GameState r=board.play(Point.Pass);
            Refresh();
            if (r == Info.GameState.END_GAME)
            {
                double black_point = board.stones[0].size();
                double white_point = board.stones[1].size() + 6.5;
                String msg = "Black " + black_point + "White " + white_point;
                MessageBox.Show(msg);
                on_game = false;
                return;
            }

            if (ai_vs_human)
            {
                r=board.play(ai.play(board));
                Refresh();
                if (r == Info.GameState.END_GAME)
                {
                    double black_point = board.stones[0].size();
                    double white_point = board.stones[1].size() + 6.5;
                    String msg = "Black " + black_point + "White " + white_point;
                    MessageBox.Show(msg);
                    on_game = false;
                }
            }
        }
    }
}
