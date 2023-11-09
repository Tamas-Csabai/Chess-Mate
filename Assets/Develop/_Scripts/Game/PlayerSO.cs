using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChessMate.Games
{
    [CreateAssetMenu(fileName = "New Player", menuName = "ChessMate/New Player")]
    public class PlayerSO : ScriptableObject
    {

        [Tooltip("The player's color.")]
        public Color Color;

        [Tooltip("The player's horizontal modifier, which could be applied in behaviour calculations.")]
        public int HorizontalModifier;

        [Tooltip("The player's vertical modifier, which could be applied in behaviour calculations.")]
        public int VerticalModifier;

    }
}
