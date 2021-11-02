using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardGame
{
    [RequireComponent(typeof(BoardGameSM))]
    public class GamePieceState : State
    {
        protected BoardGameSM StateMachine { get; private set; }

        private void Awake()
        {
            StateMachine = GetComponent<BoardGameSM>();
        }
    }
}

