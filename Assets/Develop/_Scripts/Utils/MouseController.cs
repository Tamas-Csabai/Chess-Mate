using ChessMate.Games;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChessMate
{
    public class MouseController : MonoBehaviour
    {

        private enum PlayerState
        {
            SelectPiece,
            SelectField
        }

        [SerializeField] private LayerMask pieceHitMask;
        [SerializeField] private LayerMask fieldHitMask;
        [SerializeField] private float maxDistance = 20f;

        private Camera _currentCamera;
        private PlayerState _currentPlayerState = PlayerState.SelectPiece;
        private Piece _currentPiece;

        private void Awake()
        {
            _currentCamera = Camera.main;
        }

        private void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                if(_currentPlayerState == PlayerState.SelectPiece)
                {
                    if (Physics.Raycast(_currentCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, maxDistance, pieceHitMask, QueryTriggerInteraction.Collide))
                    {
                        if (hit.transform.TryGetComponent(out Piece piece))
                        {
                            if (_currentPiece != null)
                                _currentPiece.Deselect();

                            _currentPiece = piece;

                            if(piece.Select())
                                _currentPlayerState = PlayerState.SelectField;

                            return;
                        }
                    }
                }
                else if (_currentPlayerState == PlayerState.SelectField)
                {
                    if (Physics.Raycast(_currentCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, maxDistance, fieldHitMask, QueryTriggerInteraction.Collide))
                    {
                        if (hit.transform.TryGetComponent(out Field field))
                        {
                            if (_currentPiece.IsFieldAvailable(field))
                            {
                                _currentPiece.Move(field);

                                _currentPiece.Deselect();

                                _currentPiece = null;

                                _currentPlayerState = PlayerState.SelectPiece;

                                return;
                            }
                        }
                    }
                }
                
            }
        }

    }
}
