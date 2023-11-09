
using System.Collections.Generic;

namespace ChessMate.Games.ChessGame
{
    public class ForwardMovement : PieceBehaviour
    {
        public override List<Coord> GetAvailableFieldCoords(Piece piece)
        {
            Coord currentCoord = piece.CurrentField.Coord;

            int verticalModifier = piece.PlayerSO.VerticalModifier;

            List<Coord> availableFieldCoords = new List<Coord>();

            // forward
            Coord forwardCoord = new Coord(currentCoord.x, currentCoord.y + verticalModifier);

            if(forwardCoord.y >= 0 && forwardCoord.y < piece.Board.Size.x && piece.Board.GetPiece(forwardCoord) == null)
                availableFieldCoords.Add(forwardCoord);

            // forward left
            Coord leftCoord = new Coord(currentCoord.x + 1, currentCoord.y + verticalModifier);

            Piece leftPiece = piece.Board.GetPiece(leftCoord);

            if (leftPiece != null && leftPiece.PlayerSO != piece.PlayerSO)
                availableFieldCoords.Add(leftCoord);

            // forward right
            Coord rightCoord = new Coord(currentCoord.x - 1, currentCoord.y + verticalModifier);

            Piece rightPiece = piece.Board.GetPiece(rightCoord);

            if (rightPiece != null && rightPiece.PlayerSO != piece.PlayerSO)
                availableFieldCoords.Add(rightCoord);

            return availableFieldCoords;
        }
    }
}
