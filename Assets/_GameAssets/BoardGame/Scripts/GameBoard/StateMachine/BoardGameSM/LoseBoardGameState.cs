using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardGame
{
    public class LoseBoardGameState : BoardGameState
    {
        public override void Enter()
        {
            Debug.Log("Entering Lose State");
        }

        public override void Exit()
        {
            Debug.Log("Leaving Lose State");
        }
    }
}
