using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BoardGame
{
    public class GamePieceMoveJumpState : GamePieceState
    {
        public override void Enter()
        {
            Debug.Log("Move Jump Enter");
            MoveJump(StateMachine.BoardManager.CurrentButton);
            StateMachine.Input.PressedConfirm += EndTurn;
        }

        public override void Exit()
        {
            Debug.Log("Move Jump Exit");
            StateMachine.Input.PressedConfirm -= EndTurn;
        }

        void MoveJump(Button button)
        {
            button.transform.position = new Vector3(button.transform.position.x, button.transform.position.y + 200, button.transform.position.z);
        }

        private void EndTurn()
        {

            foreach (GameObject piece in StateMachine.BoardManager.PlayerPieces)
            {
                Button button = piece.GetComponent<Button>();
                button.interactable = false;
            }
            StateMachine.ChangeState<GamePieceTransitionState>();
        }
    }
}
