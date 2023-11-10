using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChessMate.Games
{
    [CreateAssetMenu(fileName = "New Field", menuName = "ChessMate/New Field")]
    public class FieldSO : ScriptableObject
    {

        [Tooltip("The prefab of the board field to instantiate.")]
        public GameObject Prefab;

        [Tooltip("The field's color.")]
        public Color Color;

        [Tooltip("The field's select highlight color."), ColorUsage(true, true)]
        public Color SelectHighlightColor;

    }
}
