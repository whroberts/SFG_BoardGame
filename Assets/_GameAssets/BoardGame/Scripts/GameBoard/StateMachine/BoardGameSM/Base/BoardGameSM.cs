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

        bool _init = false;

        private void Start()
        {
            // set starting State here
            if (!_init)
            {
                ChangeState<SetupBoardGameBaseState>();
                _init = true;
            }
        }

        private void OnEnable()
        {
            if (_init)
            {
                //ChangeState<BoardGameState>();
                //ChangeState<PlayerTurnBoardGameState>();
                ChangeState<EnemyTurnBoardGameState>();
            }
        }
    }
}
