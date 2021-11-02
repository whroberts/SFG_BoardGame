using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardGame
{
    public class GamePieceMoveUpState : GamePieceState
    {
        public override void Enter()
        {
            Debug.Log("Move Up Enter");
        }

        public override void Exit()
        {
            Debug.Log("Move Up Exit");
        }

        void MoveUp()
        {
            //button.transform.position = new Vector3(button.transform.position.x, button.transform.position.y + 100, button.transform.position.z);
        }
    }
}
