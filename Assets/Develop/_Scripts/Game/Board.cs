using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChessMate.Games
{
    public abstract class Board : MonoBehaviour
    {

        [Header("Prefabs")]
        [SerializeField] protected Piece piecePrefab;
        [SerializeField] protected Field fieldPrefab;

        [Header("Play Area")]
        [SerializeField] private Transform bottomLeftCornerTransform;
        [SerializeField] private Transform topRightCornerTransform;

        [Header("Parenting")]
        [SerializeField] private Transform fieldsParent;
        [SerializeField] private Transform piecesParent;

        protected Game _game;
        protected Field[,] _fieldMatrix;
        protected List<Piece> _pieces = new List<Piece>();

        private Vector2 _fieldSize;
        private Vector3 _bottomLeftCellPosition;

        public Rect Size { get; private set; }

        protected abstract FieldSO GetFieldSO(Coord coord);

        protected abstract void CreatePieces();

        public virtual void Initalize(Game game, int horizontalFieldCount, int verticalFieldCount)
        {
            _game = game;

            float boardWidth = Mathf.Abs(bottomLeftCornerTransform.position.x - topRightCornerTransform.position.x);
            float boardHeight = Mathf.Abs(bottomLeftCornerTransform.position.z - topRightCornerTransform.position.z);

            Size = new Rect(horizontalFieldCount, verticalFieldCount, boardWidth, boardHeight);

            Vector3 boardDirection = (topRightCornerTransform.position - bottomLeftCornerTransform.position).normalized;

            _fieldSize = new Vector2 (boardWidth / horizontalFieldCount, boardHeight / verticalFieldCount);

            Vector3 fieldSize = new Vector3(_fieldSize.x, 0f, _fieldSize.y);

            _bottomLeftCellPosition = bottomLeftCornerTransform.position + (fieldSize.magnitude / 2 * boardDirection);

            CreateBoard();

            CreatePieces();
        }

        public Field GetField(Coord coord)
        {
            return GetField(coord.x, coord.y);
        }

        public Field GetField(int x, int y)
        {
            if (x < 0 || y < 0 || x >= _fieldMatrix.GetLength(0) || y >= _fieldMatrix.GetLength(1))
                return null;

            return _fieldMatrix[x, y];
        }

        public Piece GetPiece(Coord coord)
        {
            return GetPiece(coord.x, coord.y);
        }

        public Piece GetPiece(int x, int y)
        {
            Field field = GetField(x, y);

            if (field == null)
                return null;

            return field.CurrentPiece;
        }

        protected void AddNewPiece(PieceSO pieceSO, PlayerSO playerSO, Coord startCoord, params PieceBehaviour[] pieceBehaviours)
        {
            Piece piece = Instantiate(piecePrefab);

            piece.transform.SetParent(piecesParent, true);

            Field startField = _fieldMatrix[startCoord.x, startCoord.y];

            piece.Initalize(this, pieceSO, playerSO, startField, pieceBehaviours);

            _pieces.Add(piece);
        }

        private void CreateBoard()
        {
            _fieldMatrix = new Field[(int)Size.x, (int)Size.y];

            for (int i = 0; i < Size.x; i++)
            {
                for (int j = 0; j < Size.y; j++)
                {
                    Field field = CreateField(new Coord(i, j));

                    field.transform.SetParent(fieldsParent, true);
                    field.transform.position = _bottomLeftCellPosition + new Vector3(i * _fieldSize.x, 0f, j * _fieldSize.y);
                    field.transform.localScale = new Vector3(_fieldSize.x, 1f, _fieldSize.y);

                    _fieldMatrix[i, j] = field;
                }
            }
        }

        private Field CreateField(Coord coord)
        {
            Field field = Instantiate(fieldPrefab);

            FieldSO fieldSO = GetFieldSO(coord);

            field.Initalize(this, fieldSO, coord);

            return field;
        }

#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            if (topRightCornerTransform == null || bottomLeftCornerTransform == null)
                return;

            Vector3 boardVector = topRightCornerTransform.position - bottomLeftCornerTransform.position;

            Color gizmoColorBuffer = Gizmos.color;

            Gizmos.color = Color.blue;

            Gizmos.DrawWireCube(bottomLeftCornerTransform.position + (boardVector / 2), boardVector);

            Gizmos.color = gizmoColorBuffer;
        }

#endif

    }
}
