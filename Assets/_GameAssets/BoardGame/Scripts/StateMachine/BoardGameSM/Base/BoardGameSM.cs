using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardGame
{
    public class BoardGameSM : StateMachine
    {
        [SerializeField] InputController _input;
        public InputController Input => _input;

        private void Start()
        {
            // set starting State here
            ChangeState<SetupBoardGameState>();

        }

    }
}
