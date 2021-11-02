using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardGame
{
    public class GamePieceMoveDiagonalState : GamePieceState
    {
        public override void Enter()
        {
            Debug.Log("Move Diagonal Enter");
        }

        public override void Exit()
        {
            Debug.Log("Move Diagonal Exit");
        }

        void MoveDiagonal()
        {

        }
    }
}
