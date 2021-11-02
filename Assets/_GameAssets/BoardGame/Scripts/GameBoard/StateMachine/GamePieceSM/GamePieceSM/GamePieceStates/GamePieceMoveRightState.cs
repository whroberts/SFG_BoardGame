using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardGame
{
    public class GamePieceMoveRightState : GamePieceState
    {
        public override void Enter()
        {
            Debug.Log("Move Right Enter");
        }

        public override void Exit()
        {
            Debug.Log("Move Right Exit");
        }

        void MoveRight()
        {

        }
    }
}
