using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardGame
{
    [RequireComponent(typeof(BoardGameSM))]
    public class BoardGameState : State
    {
        protected BoardGameSM StateMachine { get; private set; }
        public BoardGameSM PubStateMachine => StateMachine;

        private void Awake()
        {
            StateMachine = GetComponent<BoardGameSM>();
        } 
    }
}
