using System.Collections;
using UnityEngine;

namespace ChessMate.Games
{
    public abstract class GameController : MonoBehaviour
    {

        private Coroutine _pieceSelectionRoutine;
        private Coroutine _fieldSelectionRoutine;

        protected Game _game;
        protected Piece _selectedPiece;
        protected Field _selectedField;

        public event System.Action<Piece> OnPieceSelected;
        public event System.Action<Field> OnFieldSelected;

        protected abstract void SetPlayerControl(PlayerSO player, bool enabled);

        protected abstract IEnumerator PieceSelection_Routine();

        protected abstract IEnumerator FieldSelection_Routine();

        public virtual void Initalize(Game game)
        {
            _game = game;
        }

        public virtual void EnablePlayerControl(PlayerSO player)
        {
            SetPlayerControl(player, true);
        }

        public virtual void DisablePlayerControl(PlayerSO player)
        {
            SetPlayerControl(player, false);
        }

        public void StartPieceSelection()
        {
            if (_pieceSelectionRoutine != null)
                StopCoroutine(_pieceSelectionRoutine);

            _pieceSelectionRoutine = StartCoroutine(PieceSelection_Routine_Internal());
        }

        public void StartFieldSelection()
        {
            if (_fieldSelectionRoutine != null)
                StopCoroutine(_fieldSelectionRoutine);

            _fieldSelectionRoutine = StartCoroutine(FieldSelection_Routine_Internal());
        }

        private IEnumerator PieceSelection_Routine_Internal()
        {
            yield return StartCoroutine(PieceSelection_Routine());

            OnPieceSelected?.Invoke(_selectedPiece);
        }

        private IEnumerator FieldSelection_Routine_Internal()
        {
            yield return StartCoroutine(FieldSelection_Routine());

            OnFieldSelected?.Invoke(_selectedField);
        }

    }
}
