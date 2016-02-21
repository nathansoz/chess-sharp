using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ChessEngine.ChessBoard;

namespace ChessPrinter
{
    class Program
    {
        static void Main(string[] args)
        {
            ChessEngine.ChessBoard.ChessBoard board = new ChessEngine.ChessBoard.ChessBoard();
            PrintBoard(board);
            Console.WriteLine("");
            board.Move(new ChessBoardCoord(0, 1), new ChessBoardCoord(0, 2));
            PrintBoard(board);
            Console.Read();
        }

        static void PrintBoard(ChessEngine.ChessBoard.ChessBoard board)
        {
            for(int i = 9; i >= -2; i--)
            {
                for(int j = -2; j < 10; j++ )
                {
                    Console.Write(" " + board.GetPieceVisualizationString(new ChessBoardCoord(j, i)) + " ");
                }
                Console.Write("\n");
            }

        }
    }
}
