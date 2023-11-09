
using System.Collections.Generic;

namespace ChessMate.Games.ChessGame
{
    public class DiagonalMovement : PieceBehaviour
    {
        public override List<Coord> GetAvailableFieldCoords(Piece piece)
        {
            Coord currentCoord = piece.CurrentField.Coord;

            List<Coord> availableFieldCoords = new List<Coord>();

            for (int i = 1; i <= currentCoord.x; i--)
            {
                Coord coord = new Coord(currentCoord.x - i, currentCoord.y - i);

                if (CheckCoord(coord))
                    break;
            }

            for (int i = 1; i <= currentCoord.x; i--)
            {
                Coord coord = new Coord(currentCoord.x + i, currentCoord.y + i);

                if (CheckCoord(coord))
                    break;
            }

            for (int i = 1; i <= currentCoord.x; i--)
            {
                Coord coord = new Coord(currentCoord.x - i, currentCoord.y + i);

                if (CheckCoord(coord))
                    break;
            }

            for (int i = 1; i <= currentCoord.x; i--)
            {
                Coord coord = new Coord(currentCoord.x + i, currentCoord.y - i);

                if (CheckCoord(coord))
                    break;
            }

            return availableFieldCoords;

            bool CheckCoord(Coord coord)
            {
                if (coord.x < 0 || coord.x >= piece.Board.Size.x || coord.y < 0 || coord.y >= piece.Board.Size.y)
                    return true;

                Piece pieceOnCoord = piece.Board.GetPiece(coord);

                if (pieceOnCoord != null)
                {
                    if (pieceOnCoord.PlayerSO != piece.PlayerSO)
                        availableFieldCoords.Add(coord);

                    return true;
                }

                availableFieldCoords.Add(coord);

                return false;
            }
        }
    }
}
