using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardGame
{
    public class GamePieceMoveJumpState : GamePieceState
    {
        public override void Enter()
        {
            Debug.Log("Move Up Enter");
        }

        public override void Exit()
        {
            Debug.Log("Move Up Exit");
        }

        void MoveJump()
        {

        }
    }
}
