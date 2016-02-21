using System;
using Xunit;

using ChessEngine.ChessBoard;

namespace ChessEngineTests
{
    public class ChessBoardTests
    {
        [Fact]
        public void BoardInitializationTest()
        {
            ChessBoard board = new ChessBoard();
        }
    }
}
