

using System.Collections.Generic;

namespace ChessMate.Games.ChessGame
{
    public class OrthogonalMovement : PieceBehaviour
    {
        public override List<Coord> GetAvailableFieldCoords(Piece piece)
        {
            Coord currentCoord = piece.CurrentField.Coord;

            List<Coord> availableFieldCoords = new List<Coord>();

            for (int i = currentCoord.x - 1; i >= 0 ; i--)
            {
                Coord coord = new Coord(i, piece.CurrentField.Coord.y);

                if (CheckCoord(coord))
                    break;
            }

            for (int i = currentCoord.x + 1; i < piece.Board.Size.x; i++)
            {
                Coord coord = new Coord(i, piece.CurrentField.Coord.y);

                if (CheckCoord(coord))
                    break;
            }

            for (int i = currentCoord.y - 1; i >= 0; i--)
            {
                Coord coord = new Coord(piece.CurrentField.Coord.x, i);

                if (CheckCoord(coord))
                    break;
            }

            for (int i = currentCoord.y + 1; i < piece.Board.Size.y; i++)
            {
                Coord coord = new Coord(piece.CurrentField.Coord.x, i);

                if (CheckCoord(coord))
                    break;
            }

            return availableFieldCoords;

            bool CheckCoord(Coord coord)
            {
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
