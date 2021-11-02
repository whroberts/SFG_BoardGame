using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardGame
{
    public class GamePieceActiveState : GamePieceState
    {

        public override void Enter()
        {
            Debug.Log("Game Piece Enter");
        }

        public override void Exit()
        {
            Debug.Log("Game Piece Exit");
        }
    }
}
