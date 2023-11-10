using System.Collections;
using UnityEngine;

namespace ChessMate.Games.ChessGame
{
    public class ChessController : GameController
    {

        private enum PlayerState
        {
            SelectPiece,
            SelectField
        }

        [SerializeField] private LayerMask pieceHitMask;
        [SerializeField] private LayerMask fieldHitMask;
        [SerializeField] private float maxRayCastDistance = 20f;

        private Camera _currentCamera;

        public override void Initalize(Game game)
        {
            _currentCamera = Camera.main;

            base.Initalize(game);
        }

        protected override IEnumerator FieldSelection_Routine()
        {
            yield return null;

            while (true)
            {
                if (Input.GetMouseButtonDown(0) && Physics.Raycast(_currentCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, maxRayCastDistance, fieldHitMask, QueryTriggerInteraction.Collide))
                {
                    if (hit.transform.TryGetComponent(out Field field))
                    {
                        if (_selectedPiece.IsFieldAvailable(field))
                        {
                            _selectedField = field;

                            _selectedPiece.Move(field);

                            _selectedPiece.Deselect();

                            _selectedPiece.Board.Game.NextPlayerTurn();

                            _selectedPiece = null;

                            yield break;
                        }
                        else
                        {
                            _selectedField = null;

                            _selectedPiece.Deselect();

                            _selectedPiece = null;

                            yield break;
                        }
                    }
                }

                yield return null;
            }
        }

        protected override IEnumerator PieceSelection_Routine()
        {
            yield return null;

            while (true)
            {
                if (Input.GetMouseButtonDown(0) && Physics.Raycast(_currentCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, maxRayCastDistance, pieceHitMask, QueryTriggerInteraction.Collide))
                {
                    if (hit.transform.TryGetComponent(out Piece piece))
                    {
                        if (_selectedPiece != null)
                            _selectedPiece.Deselect();

                        _selectedPiece = null;

                        if (piece.PlayerSO == piece.Board.Game.CurrentPlayer && piece.Select())
                        {
                            _selectedPiece = piece;

                            yield break;
                        }
                    }
                }

                yield return null;
            }
        }

        protected override void SetPlayerControl(PlayerSO player, bool enabled)
        {
            // nothing happens in local
        }
    }
}
