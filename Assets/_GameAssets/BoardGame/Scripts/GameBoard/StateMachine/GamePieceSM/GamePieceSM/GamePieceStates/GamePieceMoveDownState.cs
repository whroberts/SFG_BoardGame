using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardGame
{
    public class GamePieceMoveDownState : GamePieceState
    {
        public override void Enter()
        {
            Debug.Log("Move Down Enter");
        }

        public override void Exit()
        {
            Debug.Log("Move Down Exit");
        }

        void MoveDown()
        {

        }
    }
}
