using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessEngine.ChessBoard
{
    public class ChessBoard
    {
        public static readonly int OFFSET = 2;

        const int LOWER_PIECETYPE_BIT = 0;
        const int MIDDLE_PIECETYPE_BIT = 1;
        const int UPPER_PIECETYPE_BIT = 2;
        const int PIECE_MOVED_BIT = 3;
        const int KING_CASTLED_BIT = 4;
        const int PIECE_COLOR_BIT = 7;

        const byte INVALID_SQUARE = 0xFF;
        const byte EMPTY_SQUARE = 0x00;

        byte[,] _board;

        public ChessBoard()
        {
            _board = new byte[12, 12];
            InitStandardBoard();
        }

        public bool Move(ChessBoardCoord origin, ChessBoardCoord destination)
        {
            if (!IsSaneMove(origin, destination) || !IsPathClear(origin, destination))
                return false;
            SetSquare(GetPieceAtSquare(origin), GetPieceColorAtSquare(origin), destination);
            ClearSquare(origin);
            SetMoved(destination);
            return true;
        }

        private bool IsSaneMove(ChessBoardCoord origin, ChessBoardCoord destination)
        {
            if (_board[origin.BoardX, origin.BoardY] == EMPTY_SQUARE ||
                _board[origin.BoardX, origin.BoardY] == INVALID_SQUARE ||
                _board[origin.BoardX, origin.BoardY] == INVALID_SQUARE)
            {
                return false;
            }

            if (GetPieceColorAtSquare(origin) == GetPieceColorAtSquare(destination))
            {
                return false;
            }

            return true;
        }
        private bool IsPathClear(ChessBoardCoord origin, ChessBoardCoord destination)
        {
            if(origin.BoardX == destination.BoardX)
            {
                for(int i = (origin.BoardY + 1); i < (destination.BoardY); i++)
                {
                    if (_board[origin.BoardX, i] != EMPTY_SQUARE)
                        return false;
                }
            }
            else if(origin.BoardY == destination.BoardY)
            {
                for(int i = (origin.BoardX); i < destination.BoardX; i++)
                {
                    if (_board[i, origin.BoardY] != EMPTY_SQUARE)
                        return false;
                }
            }
            else if(Math.Abs(destination.BoardX - origin.BoardX) == Math.Abs(destination.BoardY - origin.BoardY))
            {
                for (int i = (origin.BoardX + 1), j = (origin.BoardY + 1); i < destination.BoardX; i++, j++)
                {
                    if (_board[i, j] != EMPTY_SQUARE)
                        return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        private void InitStandardBoard()
        {
            SetInvalidSquares();
            for(int i = 0; i < 8; i++)
            {
                SetSquare(Piece.Pawn, PieceColor.White, new ChessBoardCoord(i, 1));
                SetSquare(Piece.Pawn, PieceColor.Black, new ChessBoardCoord(i, 6));
            }
            SetSquare(Piece.Rook, PieceColor.White, new ChessBoardCoord(0, 0));
            SetSquare(Piece.Rook, PieceColor.White, new ChessBoardCoord(7, 0));
            SetSquare(Piece.Knight, PieceColor.White, new ChessBoardCoord(1, 0));
            SetSquare(Piece.Knight, PieceColor.White, new ChessBoardCoord(6, 0));
            SetSquare(Piece.Bishop, PieceColor.White, new ChessBoardCoord(2, 0));
            SetSquare(Piece.Bishop, PieceColor.White, new ChessBoardCoord(5, 0));
            SetSquare(Piece.Queen, PieceColor.White, new ChessBoardCoord(3, 0));
            SetSquare(Piece.King, PieceColor.White, new ChessBoardCoord(4, 0));

            SetSquare(Piece.Rook, PieceColor.Black, new ChessBoardCoord(0, 7));
            SetSquare(Piece.Rook, PieceColor.Black, new ChessBoardCoord(7, 7));
            SetSquare(Piece.Knight, PieceColor.Black, new ChessBoardCoord(1, 7));
            SetSquare(Piece.Knight, PieceColor.Black, new ChessBoardCoord(6, 7));
            SetSquare(Piece.Bishop, PieceColor.Black, new ChessBoardCoord(2, 7));
            SetSquare(Piece.Bishop, PieceColor.Black, new ChessBoardCoord(5, 7));
            SetSquare(Piece.Queen, PieceColor.Black, new ChessBoardCoord(3, 7));
            SetSquare(Piece.King, PieceColor.Black, new ChessBoardCoord(4, 7));

        }

        private void SetSquare(Piece piece, ChessBoardCoord square)
        {
            switch((int)piece)
            {
                
                case 0:
                    _board[square.BoardX, square.BoardY] = (byte)(_board[square.BoardX, square.BoardY] & ~(1 << LOWER_PIECETYPE_BIT));
                    _board[square.BoardX, square.BoardY] = (byte)(_board[square.BoardX, square.BoardY] & ~(1 << MIDDLE_PIECETYPE_BIT));
                    _board[square.BoardX, square.BoardY] = (byte)(_board[square.BoardX, square.BoardY] & ~(1 << UPPER_PIECETYPE_BIT));
                    break;
                case 1:
                    _board[square.BoardX, square.BoardY] = (byte)(_board[square.BoardX, square.BoardY] | (1 << LOWER_PIECETYPE_BIT));
                    _board[square.BoardX, square.BoardY] = (byte)(_board[square.BoardX, square.BoardY] & ~(1 << MIDDLE_PIECETYPE_BIT));
                    _board[square.BoardX, square.BoardY] = (byte)(_board[square.BoardX, square.BoardY] & ~(1 << UPPER_PIECETYPE_BIT));
                    break;
                case 2:
                    _board[square.BoardX, square.BoardY] = (byte)(_board[square.BoardX, square.BoardY] & ~(1 << LOWER_PIECETYPE_BIT));
                    _board[square.BoardX, square.BoardY] = (byte)(_board[square.BoardX, square.BoardY] | (1 << MIDDLE_PIECETYPE_BIT));
                    _board[square.BoardX, square.BoardY] = (byte)(_board[square.BoardX, square.BoardY] & ~(1 << UPPER_PIECETYPE_BIT));
                    break;
                case 3:
                    _board[square.BoardX, square.BoardY] = (byte)(_board[square.BoardX, square.BoardY] | (1 << LOWER_PIECETYPE_BIT));
                    _board[square.BoardX, square.BoardY] = (byte)(_board[square.BoardX, square.BoardY] | (1 << MIDDLE_PIECETYPE_BIT));
                    _board[square.BoardX, square.BoardY] = (byte)(_board[square.BoardX, square.BoardY] & ~(1 << UPPER_PIECETYPE_BIT));
                    break;
                case 4:
                    _board[square.BoardX, square.BoardY] = (byte)(_board[square.BoardX, square.BoardY] & ~(1 << LOWER_PIECETYPE_BIT));
                    _board[square.BoardX, square.BoardY] = (byte)(_board[square.BoardX, square.BoardY] & ~(1 << MIDDLE_PIECETYPE_BIT));
                    _board[square.BoardX, square.BoardY] = (byte)(_board[square.BoardX, square.BoardY] | (1 << UPPER_PIECETYPE_BIT));
                    break;
                case 5:
                    _board[square.BoardX, square.BoardY] = (byte)(_board[square.BoardX, square.BoardY] | (1 << LOWER_PIECETYPE_BIT));
                    _board[square.BoardX, square.BoardY] = (byte)(_board[square.BoardX, square.BoardY] & ~(1 << MIDDLE_PIECETYPE_BIT));
                    _board[square.BoardX, square.BoardY] = (byte)(_board[square.BoardX, square.BoardY] | (1 << UPPER_PIECETYPE_BIT));
                    break;
                case 6:
                    _board[square.BoardX, square.BoardY] = (byte)(_board[square.BoardX, square.BoardY] & ~(1 << LOWER_PIECETYPE_BIT));
                    _board[square.BoardX, square.BoardY] = (byte)(_board[square.BoardX, square.BoardY] | (1 << MIDDLE_PIECETYPE_BIT));
                    _board[square.BoardX, square.BoardY] = (byte)(_board[square.BoardX, square.BoardY] | (1 << UPPER_PIECETYPE_BIT));
                    break;
                case 7:
                    _board[square.BoardX, square.BoardY] = (byte)(_board[square.BoardX, square.BoardY] | (1 << LOWER_PIECETYPE_BIT));
                    _board[square.BoardX, square.BoardY] = (byte)(_board[square.BoardX, square.BoardY] | (1 << MIDDLE_PIECETYPE_BIT));
                    _board[square.BoardX, square.BoardY] = (byte)(_board[square.BoardX, square.BoardY] | (1 << UPPER_PIECETYPE_BIT));
                    break;
            }
        }
        
        private void SetSquare(PieceColor color, ChessBoardCoord square)
        {
            switch((int) color)
            {
                case 0:
                    _board[square.BoardX, square.BoardY] = (byte)(_board[square.BoardX, square.BoardY] & ~(1 << PIECE_COLOR_BIT));
                    break;
                case 1:
                    _board[square.BoardX, square.BoardY] = (byte)(_board[square.BoardX, square.BoardY] | (1 << PIECE_COLOR_BIT));
                    break;
            }
        }

        private void SetMoved(ChessBoardCoord square)
        {
            _board[square.BoardX, square.BoardY] = (byte)(_board[square.BoardX, square.BoardY] | (1 << PIECE_MOVED_BIT));
        }

        private void SetSquare(Piece piece, PieceColor color, ChessBoardCoord square)
        {
            SetSquare(piece, square);
            SetSquare(color, square);
        }

        private Piece GetPieceAtSquare(ChessBoardCoord square)
        {
            byte ret = (byte)(_board[square.BoardX, square.BoardY] & (1 << LOWER_PIECETYPE_BIT));
            ret |= (byte)(_board[square.BoardX, square.BoardY] & (1 << MIDDLE_PIECETYPE_BIT));
            ret |= (byte)(_board[square.BoardX, square.BoardY] & (1 << UPPER_PIECETYPE_BIT));
            return (Piece)ret;
        }

        private PieceColor GetPieceColorAtSquare(ChessBoardCoord square)
        {
            if (_board[square.BoardX, square.BoardY] == EMPTY_SQUARE)
                return PieceColor.EmptySquare;

            if((byte)(_board[square.BoardX, square.BoardY] & (1 << PIECE_COLOR_BIT)) != 0)
            {
                return (PieceColor)1;
            }
            else
            {
                return 0;
            }
        }

        private void ClearSquare(ChessBoardCoord square)
        {
            _board[square.BoardX, square.BoardY] = 0;
        }

        private void SetInvalidSquares()
        {
            for(int x = 0; x < 12; x++)
            {
                _board[x, 0] = 0xFF;
                _board[x, 1] = 0xFF;
                _board[x, 10] = 0xFF;
                _board[x, 11] = 0xFF;
                _board[0, x] = 0xFF;
                _board[1, x] = 0xFF;
                _board[10, x] = 0xFF;
                _board[11, x] = 0xFF;
            }
        }

        /// <summary>
        /// Debugging function, remove later...
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public string GetPieceVisualizationString(ChessBoardCoord selectedSquare)
        {
            byte square = _board[selectedSquare.BoardX, selectedSquare.BoardY];
            if (square == 0xFF)
                return "II";
            else if (square == 0x00)
                return "00";

            PieceColor color = GetPieceColorAtSquare(selectedSquare);
            Piece piece = GetPieceAtSquare(selectedSquare);
            string ret = "";

            switch(color)
            {
                case PieceColor.Black:
                    ret += "B";
                    break;
                case PieceColor.White:
                    ret += "W";
                    break;
            }
            switch(piece)
            {
                case Piece.Pawn:
                    ret += "P";
                    break;
                case Piece.Knight:
                    ret += "N";
                    break;
                case Piece.Bishop:
                    ret += "B";
                    break;
                case Piece.Rook:
                    ret += "R";
                    break;
                case Piece.Queen:
                    ret += "Q";
                    break;
                case Piece.King:
                    ret += "K";
                    break;
            }
            return ret;
        }
    }
}
