
namespace ChessMate.Games.ChessGame
{
    public class ChessField : Field
    {
        public override void Initalize(Board board, FieldSO fieldSO, Coord coord)
        {
            base.Initalize(board, fieldSO, coord);

            gameObject.name = Chess.CoordToSquare(coord);
        }
    }
}
