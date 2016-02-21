using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessEngine.ChessBoard
{
    internal static class MovementRules
    {
        internal static bool CheckValidMove(Piece movingPiece,
                                          PieceColor movingPieceColor,
                                          bool movingPieceFirstMove,
                                          ChessBoardCoord origin,
                                          Piece destinationPiece,
                                          PieceColor destinationPieceColor,
                                          ChessBoardCoord destination)
        {
            return false;
        }

        internal static bool ValidMoveForPiece(Piece movingPiece,
                                             PieceColor movingPieceColor,
                                             bool movingPieceFirstMove,
                                             Piece destinationPiece,
                                             ChessBoardCoord origin,
                                             ChessBoardCoord destination)
        {
            switch(movingPiece)
            {
                case Piece.Pawn:
                    {
                        //Average case
                        if (destination.Y == origin.Y + 1)
                        {
                            if ( movingPieceColor == PieceColor.White &&
                                 destinationPiece == Piece.NoPiece)
                            {
                                return true;
                            }
                            return false;
                        }
                        else if( destination.X == origin.X - 1 )
                        {
                            if ( movingPieceColor == PieceColor.Black &&
                                 destinationPiece == Piece.NoPiece)
                            {
                                return true;
                            }
                            return false;
                        }
                        //Initial Move case
                        else if ( (destination.Y == origin.Y + 2) )
                        {
                            if ( movingPieceColor == PieceColor.White &&
                                 movingPieceFirstMove && 
                                 destinationPiece == Piece.NoPiece )
                            {
                                return true;
                            }
                            return false;
                        }
                        else if ((destination.Y == origin.Y - 2))
                        {
                            if ( movingPieceColor == PieceColor.Black &&
                                 movingPieceFirstMove &&
                                 destinationPiece == Piece.NoPiece )
                            {
                                return true;
                            }
                            return false;
                        }
                        //Capture case
                        else if ( ( ( destination.X == origin.X + 1 ) || ( destination.X == origin.X - 1 ) ) &&
                                    ( destination.Y == origin.Y + 1 ) &&
                                    ( movingPieceColor == PieceColor.White ) &&
                                    ( destinationPiece != Piece.NoPiece ) )
                        {
                            return true;
                        }
                        else if ( ( ( destination.X == origin.X + 1 ) || ( destination.X == origin.X - 1 ) ) &&
                                  ( destination.Y == origin.Y - 1 ) &&
                                  ( movingPieceColor == PieceColor.Black ) &&
                                  ( destinationPiece != Piece.NoPiece ) )
                        {
                            return true;
                        }

                        //En passant

                        return false;
                    }
                default:
                    return false;
            }
        }
    }
}
