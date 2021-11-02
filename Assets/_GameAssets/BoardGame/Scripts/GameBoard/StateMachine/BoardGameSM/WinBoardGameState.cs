using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardGame
{
    public class WinBoardGameState : BoardGameState
    {
        public override void Enter()
        {
            Debug.Log("Entering Win State");
        }

        public override void Exit()
        {
            Debug.Log("Leaving Win State");
        }
    }
}
