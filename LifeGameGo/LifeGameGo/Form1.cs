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
        AI ai2;
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
            on_game = false;
            ai_vs_human = false;
            Init.init();
            board = new Board();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
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
            Refresh();
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
            black = comboBox1.Text;
            white = comboBox2.Text;
            board = new Board();

            if (black != "Human" && white != "Human")
            {
                if (black == "RandomAI")
                {
                    ai = new RandomAI();
                }
                else if (black == "MonteCarloAI")
                {
                    ai = new MCTAI();
                }

                if (white == "RandomAI")
                {
                    ai2 = new RandomAI();
                }
                else if (white == "MonteCarloAI")
                {
                    ai2 = new MCTAI();
                }

                Info.GameState st;
                while (true)
                {
                    st = board.play(ai.play(board));
                    Refresh();
                    if (st == Info.GameState.END_GAME)
                    {
                        break;
                    }
                    st = board.play(ai2.play(board));
                    Refresh();
                    if (st == Info.GameState.END_GAME)
                    {
                        break;
                    }
                }
                double black_point = board.stones[0].size();
                double white_point = board.stones[1].size() + 6.5;
                String msg = "Black " + black_point + " White " + white_point;
                MessageBox.Show(msg);

                return;
            }

            if (black == "RandomAI")
            {
                ai = new RandomAI();
            }
            else if (black == "MonteCarloAI")
            {
                ai = new MCTAI();
            }

            if (white == "RandomAI")
            {
                ai = new RandomAI();
            }
            else if (white == "MonteCarloAI")
            {
                ai = new MCTAI();
            }


            if (black != "Human")
            {
                ai_vs_human = true;
                board.play(ai.play(board));
            }
            if (white != "Human")
            {
                ai_vs_human = true;
            }
            Refresh();
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
                String msg = "Black " + black_point + " White " + white_point;
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
                    String msg = "Black " + black_point + " White " + white_point;
                    MessageBox.Show(msg);
                    on_game = false;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(!on_game)
            {
                return;
            }
            if(board.points.Count==0)
            {
                return;
            }
            Board n = new Board();
            if (ai_vs_human)
            {
                if(board.points.Count==1)
                {
                    return;
                }
                for(int i=0;i<board.points.Count-2;++i)
                {
                    n.play(board.points[i]);
                }
            }
            else
            {
                for (int i = 0; i < board.points.Count - 1; ++i)
                {
                    n.play(board.points[i]);
                }
            }
            board = n;
            Refresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(!on_game)
            {
                return;
            }
            Info.GameState st = Info.GameState.VALID_MOVE;
            AI randomai = new RandomAI();
            while(st != Info.GameState.END_GAME)
            {
                st = board.play(randomai.play(board));
                Refresh();
            }
            double black_point = board.stones[0].size();
            double white_point = board.stones[1].size() + 6.5;
            String msg = "Black " + black_point + " White " + white_point;
            MessageBox.Show(msg);
            on_game = false;
        }
    }
}
