using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChessMate.Games
{

    [CreateAssetMenu(fileName = "New Piece", menuName = "ChessMate/New Piece")]
    public class PieceSO : ScriptableObject
    {

        [Tooltip("The prefab of the chess piece to instantiate.")]
        public GameObject Prefab;

        [Tooltip("The field's highlight color."), ColorUsage(true, true)]
        public Color HighlightColor;

    }
}
