
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChessMate.Games
{
    public abstract class Game : MonoBehaviour
    {

        [SerializeField] protected Board board;
        [SerializeField] protected int horizontalFieldCount = 8;
        [SerializeField] protected int verticalFieldCount = 8;

        protected virtual void Start()
        {
            board.Initalize(this, horizontalFieldCount, verticalFieldCount);
        }

    }
}
