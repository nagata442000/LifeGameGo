using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGameGo
{
    public class UCBNode
    {
        public Board board;
        public int win;
        public int iteration;
        public UCBNode parent;
        public List<UCBNode> children;
        public UCBNode()
        {
            win = 0;
            iteration = 0;
            parent = null;
            children = new List<UCBNode>();
        }


        public double UCB(double all)
        {
            if (iteration == 0)
            {
                return 10000.0;
            }
            return (double)win / (double)iteration + 0.3 * Math.Log(2.0 * Math.Sqrt(all) / (double)iteration);
        }

        public UCBNode maxUCB(double all)
        {
            UCBNode res = null;
            if(children.Count==0)
            {
                return res;
            }
            double max_ucb = -10000.0;
            foreach (UCBNode c in children)
            {
                double ucb = c.UCB(all);
                if(max_ucb<ucb)
                {
                    max_ucb = ucb;
                    res = c;
                }
            }
            return res;
        }

    }

    public class MCTAI : AI
    {
        public int iteration;
        public Random rnd;
        public UCBNode root;

        public List<Point> nextMove(Board board)
        {
            PlayResult p = new PlayResult(board);
            return p.valid.toList();
        }

        public List<Point> nextMovePlayOut(Board board)
        {
            PlayResult p = new PlayResult(board);
            return p.valid.toList();
        }

        public bool onePlayOut(Board board)
        {
            Board tmp = new Board(board);
            Info.GameState s;
            while(true)
            {
                List<Point> l = nextMovePlayOut(tmp);
                if (l.Count == 0)
                {
                    s = tmp.play(Point.Pass);
                    if (s == Info.GameState.END_GAME)
                    {
                        return (double)tmp.stones[0].size() > (double)tmp.stones[1].size() + 6.5;
                    }
                }
                else
                {
                    Point p = l[rnd.Next(l.Count)];
                    tmp.play(p);
                }
            }
        }

        public bool expandNode(UCBNode n)
        {
            List<Point> kouho = nextMove(n.board);
            if (kouho.Count == 0)
            {
                UCBNode c = new UCBNode();
                c.board = new Board(n.board);
                Info.GameState s=c.board.play(Point.Pass);
                if (s == Info.GameState.END_GAME)
                {
                    return false;
                }
                c.parent = n;
                n.children.Add(c);
                return true;
            }
            foreach (Point p in kouho)
            {
                UCBNode c = new UCBNode();
                c.board = new Board(n.board);
                c.board.play(p);
                c.parent = n;
                n.children.Add(c);
            }
            return true;
        }

        public MCTAI()
        {
            iteration = 500;
            rnd = new Random();
        }

        public Point play(Board board)
        {
            List<Point> kouho = nextMove(board);
            if(kouho.Count==0)
            {
                return Point.Pass;
            }else if(kouho.Count==1)
            {
                return kouho[0];
            }

            root = new UCBNode();
            root.board = new Board(board);
            expandNode(root);
            for(int i=0;i< iteration;++i)
            {
                List<UCBNode> list = new List<UCBNode>();
                UCBNode n = root;
                while(n!= null)
                {
                    list.Add(n);
                    n = n.maxUCB(root.iteration);
                }
                Board tmp = list.Last().board;
                bool black_win = onePlayOut(tmp);
                foreach (UCBNode j in list)
                {
                    if(j.board.turn==1 && black_win)
                    {
                        j.win++;
                    }
                    if(j.board.turn==0 && (!black_win))
                    {
                        j.win++;
                    }
                    j.iteration++;
                }
                if(list.Last().iteration >= 20)
                {
                    expandNode(list.Last());
                }
            }

            UCBNode m=root.children[0];
            int win = -1;
            foreach(UCBNode i in root.children)
            {
                if(i.win > win)
                {
                    m = i;
                    win = i.win;
                }
            }
            return m.board.points.Last();
        }

    }
}
