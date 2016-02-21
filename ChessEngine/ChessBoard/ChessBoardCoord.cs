using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessEngine.ChessBoard
{
    public class ChessBoardCoord
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        /// <summary>
        /// Represents the true X coordinate, including margin
        /// </summary>
        internal int BoardX { get; private set; }

        /// <summary>
        /// Represents the true Y coordinate, including margin
        /// </summary>
        internal int BoardY { get; private set; }
        public ChessBoardCoord(int x, int y)
        {
            X = x;
            Y = y;
            BoardX = x + ChessBoard.OFFSET;
            BoardY = y + ChessBoard.OFFSET;
        }
    }
}
