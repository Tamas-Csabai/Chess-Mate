
using System.Collections.Generic;

namespace ChessMate.Games.ChessGame
{
    public class StepMovement : PieceBehaviour
    {
        public override List<Coord> GetAvailableFieldCoords(Piece piece)
        {
            Coord currentCoord = piece.CurrentField.Coord;

            List<Coord> availableFieldCoords = new List<Coord>();

            AddCoord(currentCoord.x + 1, currentCoord.y);
            AddCoord(currentCoord.x - 1, currentCoord.y);
            AddCoord(currentCoord.x, currentCoord.y + 1);
            AddCoord(currentCoord.x, currentCoord.y - 1);

            return availableFieldCoords;

            void AddCoord(int x, int y)
            {
                if (x < 0 || x >= piece.Board.Size.x || y < 0 || y >= piece.Board.Size.y)
                    return;

                Piece pieceOnCoord = piece.Board.GetPiece(x, y);

                if (pieceOnCoord != null && pieceOnCoord.PlayerSO == piece.PlayerSO)
                    return;

                availableFieldCoords.Add(new Coord(x, y));
            }
        }
    }
}
