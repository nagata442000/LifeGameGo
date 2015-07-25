using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGameGo
{
    public static class Info
    {
        public const int N = 9;
        public enum Stones
        {
            BLACK,
            WHITE,
            EMPTY,
            OUT_OF_BOARD
        };
        public enum GameState
        {
            VALID_MOVE,
            INVALID_MOVE,
            END_GAME
        };
    }
}
