using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardGame
{
    public class GamePieceSM : StateMachine
    {
        [SerializeField] InputController _input = null;
        [SerializeField] BoardManager _bm = null;
        public InputController Input => _input;
        public BoardManager BoardManager => _bm;

        bool _init = false;

        private void Start()
        {
            if (!_init)
            {
                ChangeState<GamePieceState>();
                ChangeState<ChooseGamePieceState>();
                _init = true;
            }
        }

        
        private void OnEnable()
        {
            if (_init)
            {
                ChangeState<ChooseGamePieceState>();
            }
        }

    }
}
