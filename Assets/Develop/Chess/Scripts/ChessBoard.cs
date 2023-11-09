
using UnityEngine;

namespace ChessMate.Games.ChessGame
{
    public class ChessBoard : Board
    {

        [Header("Field Assets")]
        [SerializeField] private FieldSO lightFieldSO;
        [SerializeField] private FieldSO darkFieldSO;

        [Header("Piece Assets")]
        [SerializeField] private PieceSO pawnSO;
        [SerializeField] private PieceSO knightSO;
        [SerializeField] private PieceSO bishopSO;
        [SerializeField] private PieceSO rookSO;
        [SerializeField] private PieceSO queenSO;
        [SerializeField] private PieceSO kingSO;

        protected Chess _chess;

        public override void Initalize(Game game, int horizontalFieldCount, int verticalFieldCount)
        {
            _chess = game as Chess;

            base.Initalize(game, horizontalFieldCount, verticalFieldCount);
        }

        protected override FieldSO GetFieldSO(Coord coord)
        {
            return (coord.x + coord.y) % 2 == 0 ? darkFieldSO : lightFieldSO;
        }

        protected override void CreatePieces()
        {
            AddNewPiece(knightSO, _chess.WhitePlayerSO, "B1", new JumpMovement());
            AddNewPiece(knightSO, _chess.WhitePlayerSO, "G1", new JumpMovement());
            AddNewPiece(knightSO, _chess.BlackPlayerSO, "B8", new JumpMovement());
            AddNewPiece(knightSO, _chess.BlackPlayerSO, "G8", new JumpMovement());

            AddNewPiece(bishopSO, _chess.WhitePlayerSO, "C1", new DiagonalMovement());
            AddNewPiece(bishopSO, _chess.WhitePlayerSO, "F1", new DiagonalMovement());
            AddNewPiece(bishopSO, _chess.BlackPlayerSO, "C8", new DiagonalMovement());
            AddNewPiece(bishopSO, _chess.BlackPlayerSO, "F8", new DiagonalMovement());

            AddNewPiece(rookSO, _chess.WhitePlayerSO, "A1", new OrthogonalMovement());
            AddNewPiece(rookSO, _chess.WhitePlayerSO, "H1", new OrthogonalMovement());
            AddNewPiece(rookSO, _chess.BlackPlayerSO, "A8", new OrthogonalMovement());
            AddNewPiece(rookSO, _chess.BlackPlayerSO, "H8", new OrthogonalMovement());

            AddNewPiece(kingSO, _chess.WhitePlayerSO, "E1", new StepMovement());
            AddNewPiece(queenSO, _chess.WhitePlayerSO, "D1", new OrthogonalMovement(), new DiagonalMovement());
            AddNewPiece(kingSO, _chess.BlackPlayerSO, "E8", new StepMovement());
            AddNewPiece(queenSO, _chess.BlackPlayerSO, "D8", new OrthogonalMovement(), new DiagonalMovement());

            for (int i = 0; i < 8; i++)
            {
                AddNewPiece(pawnSO, _chess.WhitePlayerSO, new Coord(i, 1), new ForwardMovement());
            }

            for (int i = 0; i < 8; i++)
            {
                AddNewPiece(pawnSO, _chess.BlackPlayerSO, new Coord(i, 6), new ForwardMovement());
            }
        }

        private void AddNewPiece(PieceSO pieceSO, PlayerSO playerSO, string startSquareID, params PieceBehaviour[] pieceBehaviours)
        {
            AddNewPiece(pieceSO, playerSO, Chess.SquareToCoord(startSquareID), pieceBehaviours);
        }
    }
}
