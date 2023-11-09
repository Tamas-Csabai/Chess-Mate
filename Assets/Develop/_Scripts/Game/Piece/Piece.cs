using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChessMate.Games
{
    public abstract class Piece : MonoBehaviour
    {

        private GameObject _pieceObject;
        private PieceBehaviour[] _pieceBehaviours;
        private IColorSetter[] _colorSetters;
        private List<Field> _availableFields = new List<Field>();

        public PieceSO PieceSO { get; private set; }

        public Board Board { get; private set; }

        public PlayerSO PlayerSO { get; private set; }

        public Field CurrentField { get; private set; }

        public virtual void Initalize(Board board, PieceSO pieceSO, PlayerSO playerSO, Field startField, PieceBehaviour[] pieceBehaviours)
        {
            Board = board;
            PieceSO = pieceSO;
            PlayerSO = playerSO;
            CurrentField = startField;
            _pieceBehaviours = pieceBehaviours;

            transform.position = CurrentField.PiecePoint.position;

            startField.SetCurrentPiece(this);

            gameObject.name = pieceSO.name + " (" + playerSO.name + ")";

            _pieceObject = Instantiate(pieceSO.Prefab);

            _pieceObject.transform.SetParent(transform, true);
            _pieceObject.transform.localPosition = Vector3.zero;
            _pieceObject.transform.localRotation = Quaternion.identity;

            _colorSetters = GetComponentsInChildren<IColorSetter>();

            SetColor(PlayerSO.Color);
        }

        public void Move(Field targetField)
        {
            CurrentField.SetCurrentPiece(null);

            targetField.SetCurrentPiece(this);

            CurrentField = targetField;

            transform.position = CurrentField.PiecePoint.position;
        }

        public void DestroyPiece()
        {
            Destroy(gameObject);
        }

        public bool Select()
        {
            _availableFields.Clear();

            List<Coord> availableFieldCoords = new List<Coord>();

            for (int i = 0; i < _pieceBehaviours.Length; i++)
                availableFieldCoords.AddRange(_pieceBehaviours[i].GetAvailableFieldCoords(this));

            foreach(Coord coord in availableFieldCoords)
                _availableFields.Add(Board.GetField(coord));

            foreach (Field field in _availableFields)
                field.EnableHighlight();

            if (_availableFields.Count > 0)
                return true;

            return false;
        }

        public void Deselect()
        {
            foreach (Field field in _availableFields)
            {
                field.ClearHighlight();
            }
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
                _colorSetters[i].SetColor("_EmissionColor", PieceSO.HighlightColor);
            }
        }

        public void ClearHighlight()
        {
            for (int i = 0; i < _colorSetters.Length; i++)
            {
                _colorSetters[i].SetColor("_EmissionColor", Color.black);
            }
        }

        public bool IsFieldAvailable(Field field)
        {
            return _availableFields.Contains(field);
        }

    }
}
