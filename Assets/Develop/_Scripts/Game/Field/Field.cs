
using UnityEngine;

namespace ChessMate.Games
{
    public abstract class Field : MonoBehaviour
    {

        [SerializeField] private Transform piecePoint;

        private GameObject _fieldObject;
        private IColorSetter[] _colorSetters; 

        public FieldSO FieldSO { get; private set; }

        public Coord Coord { get; private set; }

        public Board Board { get; private set; }

        public Transform PiecePoint => piecePoint;

        public Piece CurrentPiece { get; private set; }

        public virtual void Initalize(Board board, FieldSO fieldSO, Coord coord)
        {
            Board = board;
            FieldSO = fieldSO;
            Coord = coord;

            _fieldObject = Instantiate(fieldSO.Prefab);

            _fieldObject.transform.SetParent(transform, true);
            _fieldObject.transform.localPosition = Vector3.zero;
            _fieldObject.transform.localRotation = Quaternion.identity;

            _colorSetters = GetComponentsInChildren<IColorSetter>();

            SetColor(FieldSO.Color);
        }

        public void SetCurrentPiece(Piece piece)
        {
            if (piece != null && CurrentPiece != null)
                DestroyCurrentPiece();

            CurrentPiece = piece;
        }

        public void DestroyCurrentPiece()
        {
            CurrentPiece.DestroyPiece();

            CurrentPiece = null;
        }

        public void SetColor(Color color)
        {
            for (int i = 0; i < _colorSetters.Length; i++)
            {
                _colorSetters[i].SetColor("_BaseColor", color);
            }
        }

        public void EnableHighlight()
        {
            for (int i = 0; i < _colorSetters.Length; i++)
            {
                _colorSetters[i].SetColor("_EmissionColor", FieldSO.HighlightColor);
            }
        }

        public void ClearHighlight()
        {
            for (int i = 0; i < _colorSetters.Length; i++)
            {
                _colorSetters[i].SetColor("_EmissionColor", Color.black);
            }
        }

    }
}
