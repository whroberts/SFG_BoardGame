using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardGame
{
    public class BoardGameSM : StateMachine
    {
        [SerializeField] InputController _input = null;
        [SerializeField] BoardManager _bm = null;
        public InputController Input => _input;
        public BoardManager BoardManager => _bm;

        private void Start()
        {
            ChangeState<SetupBoardGameBaseState>();
        }
    }
}
