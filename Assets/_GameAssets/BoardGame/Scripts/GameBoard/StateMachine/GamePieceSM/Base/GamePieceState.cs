using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardGame
{
    [RequireComponent(typeof(GamePieceSM))]
    public class GamePieceState : State
    {
        protected GamePieceSM StateMachine { get; private set; }
        public GamePieceSM PubStateMachine => StateMachine;

        private void Awake()
        {
            StateMachine = GetComponent<GamePieceSM>();
        }
    }
}

