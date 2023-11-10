
using UnityEngine;

namespace ChessMate.Games
{
    public class TurnManager : MonoBehaviour
    {

        [SerializeField] protected PlayerSO[] playersInOrder;

        protected Game _game;
        protected int _currentPlayerIndex = 0;

        public event System.Action<PlayerSO> OnStartPlayerTurn;
        public event System.Action<PlayerSO> OnEndPlayerTurn;

        public PlayerSO CurrentPlayer { get; private set; }

        public virtual void Initalize(Game game, PlayerSO starterPlayer)
        {
            _game = game;

            CurrentPlayer = starterPlayer;

            for (int i = 0; i < playersInOrder.Length; i++)
            {
                if (playersInOrder[i] == starterPlayer)
                    _currentPlayerIndex = i - 1;
            }
        }

        public virtual void NextPlayer()
        {
            EndPlayerTurn(CurrentPlayer);

            _currentPlayerIndex++;

            if (_currentPlayerIndex >= playersInOrder.Length)
                _currentPlayerIndex = 0;

            CurrentPlayer = playersInOrder[_currentPlayerIndex];

            StartPlayerTurn(CurrentPlayer);
        }

        protected virtual void StartPlayerTurn(PlayerSO player)
        {
            OnStartPlayerTurn?.Invoke(player);
        }

        protected virtual void EndPlayerTurn(PlayerSO player)
        {
            OnEndPlayerTurn?.Invoke(player);
        }

    }
}
