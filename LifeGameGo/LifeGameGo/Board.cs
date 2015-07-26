using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGameGo
{
    public class Board
    {
        public PointSet[] stones;
        public int turn;
        public List<Point> points;

        private int opponent()
        {
            return 1 - turn;
        }


        public Board()
        {
            points = new List<Point>();
            stones = new PointSet[2];
            stones[0] = new PointSet();
            stones[1] = new PointSet();
            turn = 0;
        }

        public Board(Board b)
        {
            points = new List<Point>(b.points);
            stones = new PointSet[2];
            stones[0] = new PointSet(b.stones[0]);
            stones[1] = new PointSet(b.stones[1]);
            turn = b.turn;
        }


        public Info.Stones get(Point p)
        {
            if (p.x < 0 || p.x >= Info.N || p.y < 0 || p.y >= Info.N)
            {
                return Info.Stones.OUT_OF_BOARD;
            }
            if (stones[0].get(p))
            {
                return Info.Stones.BLACK;
            }
            else if (stones[1].get(p))
            {
                return Info.Stones.WHITE;
            }
            else
            {
                return Info.Stones.EMPTY;
            }
        }

        public void set(Point p, int s)
        {
            stones[s].set(p);
        }

        public Info.GameState test_play(Point p)
        {
            if (p == Point.Pass)
            {
                if (points.Count > 0)
                {
                    if (points.Last() == Point.Pass)
                    {
                        return Info.GameState.END_GAME;
                    }
                }
                return Info.GameState.VALID_MOVE;
            }
            if (p.x < 0 || p.x >= Info.N || p.y < 0 || p.y >= Info.N)
            {
                return Info.GameState.INVALID_MOVE;
            }
            if (get(p) != Info.Stones.EMPTY)
            {
                return Info.GameState.INVALID_MOVE;
            }

            if ((PointSet.around[p.x, p.y] & stones[opponent()]).size() >= 3)
            {
                return Info.GameState.INVALID_MOVE;
            }
            return Info.GameState.VALID_MOVE;
        }


        public Info.GameState play(Point p)
        {
            if (p == Point.Pass)
            {
                if (points.Count > 0)
                {
                    if (points.Last() == Point.Pass)
                    {
                        points.Add(p);
                        return Info.GameState.END_GAME;
                    }
                }
                points.Add(p);
                turn = 1 - turn;
                return Info.GameState.VALID_MOVE;
            }
            if (p.x < 0 || p.x >= Info.N || p.y < 0 || p.y >= Info.N)
            {
                return Info.GameState.INVALID_MOVE;
            }
            if (get(p) != Info.Stones.EMPTY)
            {
                return Info.GameState.INVALID_MOVE;
            }

            if ((PointSet.around[p.x, p.y] & stones[opponent()]).size() >= 3)
            {
                return Info.GameState.INVALID_MOVE;
            }

            points.Add(p);
            stones[turn].set(p);

            List<Point> list = PointSet.around[p.x, p.y].toList();
            PointSet kill = new PointSet();
            foreach (Point i in list)
            {
                if ((PointSet.around[i.x, i.y] & stones[turn]).size() >= 3)
                {
                    kill.set(i);
                }
            }
            stones[opponent()] = stones[opponent()] - kill;
            turn = 1 - turn;
            return Info.GameState.VALID_MOVE;
        }
    }
}
