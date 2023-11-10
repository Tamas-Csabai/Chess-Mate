
namespace ChessMate.Games
{
    public interface IGameComponent
    {

        void Initalize(Game game);

        void GameStarted();

        void GameEnded();

        void PlayerTurnStarted(PlayerSO player);

        void PlayerTurnEnded(PlayerSO player);

        void PieceSelected(Piece piece);

        void FieldSelected(Field field);

    }
}
