
using UnityEngine;

namespace ChessMate.Games
{
    public abstract class Game : MonoBehaviour
    {

        [Header("References")]
        [SerializeField] protected Board board;
        [SerializeField] protected TurnManager turnManager;
        [SerializeField] protected GameController gameController;

        [Header("Parameters")]
        [SerializeField] private bool initalizeOnStart = true;
        [SerializeField] private bool startAfterInitalization = true;
        [SerializeField] protected PlayerSO starterPlayer;
        [SerializeField] protected int horizontalFieldCount = 8;
        [SerializeField] protected int verticalFieldCount = 8;

        protected IGameComponent[] _gameComponents;

        public PlayerSO CurrentPlayer => turnManager.CurrentPlayer;

        protected abstract void GameStarted();

        protected virtual void Start()
        {
            if(initalizeOnStart)
                Initalize();
        }

        protected virtual void OnDestroy()
        {
            if(turnManager != null)
            {
                turnManager.OnStartPlayerTurn -= PlayerTurnStarted;
                turnManager.OnEndPlayerTurn -= PlayerTurnEnded;
            }

            if(gameController != null)
            {
                gameController.OnPieceSelected -= PieceSelected;
                gameController.OnFieldSelected -= FieldSelected;
            }
        }

        public virtual void Initalize()
        {
            turnManager.Initalize(this, starterPlayer);
            turnManager.OnStartPlayerTurn += PlayerTurnStarted;
            turnManager.OnEndPlayerTurn += PlayerTurnEnded;

            gameController.Initalize(this);
            gameController.OnPieceSelected += PieceSelected;
            gameController.OnFieldSelected += FieldSelected;

            board.Initalize(this, horizontalFieldCount, verticalFieldCount);

            _gameComponents = GetComponentsInChildren<IGameComponent>();

            for (int i = 0; i < _gameComponents.Length; i++)
                _gameComponents[i].Initalize(this);

            if (startAfterInitalization)
                StartGame();
        }

        public void StartGame()
        {
            turnManager.NextPlayer();

            gameController.EnablePlayerControl(starterPlayer);

            GameStarted();

            for (int i = 0; i < _gameComponents.Length; i++)
                _gameComponents[i].GameStarted();
        }

        public virtual void NextPlayerTurn()
        {
            gameController.DisablePlayerControl(turnManager.CurrentPlayer);

            turnManager.NextPlayer();

            gameController.EnablePlayerControl(turnManager.CurrentPlayer);
        }

        protected virtual void PlayerTurnStarted(PlayerSO player)
        {
            gameController.EnablePlayerControl(player);

            for (int i = 0; i < _gameComponents.Length; i++)
                _gameComponents[i].PlayerTurnStarted(player);
        }

        protected virtual void PlayerTurnEnded(PlayerSO player)
        {
            gameController.DisablePlayerControl(player);

            for (int i = 0; i < _gameComponents.Length; i++)
                _gameComponents[i].PlayerTurnEnded(player);
        }

        protected virtual void PieceSelected(Piece piece)
        {
            for (int i = 0; i < _gameComponents.Length; i++)
                _gameComponents[i].PieceSelected(piece);
        }

        protected virtual void FieldSelected(Field field)
        {
            for (int i = 0; i < _gameComponents.Length; i++)
                _gameComponents[i].FieldSelected(field);
        }

    }
}
