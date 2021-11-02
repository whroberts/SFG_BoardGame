using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardGame
{
    public class GamePieceMoveLeftState : GamePieceState
    {
        public override void Enter()
        {
            Debug.Log("Move Left Enter");
        }

        public override void Exit()
        {
            Debug.Log("Move Left Exit");
        }

        void MoveLeft()
        {

        }
    }
}
