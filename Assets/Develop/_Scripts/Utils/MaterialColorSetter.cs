using ChessMate.Games;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChessMate
{
    public class MaterialColorSetter : MonoBehaviour, IColorSetter
    {

        [SerializeField] private Renderer[] renderers;

        public void SetColor(string colorProperty, Color color)
        {
            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].material.SetColor(colorProperty, color);
            }
        }

        void IColorSetter.SetColor(string colorProperty, Color color)
        {
            SetColor(colorProperty, color);
        }
    }
}
