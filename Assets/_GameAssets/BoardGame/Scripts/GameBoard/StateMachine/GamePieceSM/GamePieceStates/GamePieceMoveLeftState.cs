using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BoardGame
{
    public class GamePieceMoveLeftState : GamePieceState
    {
        public override void Enter()
        {
            Debug.Log("Move Left Enter");
            MoveLeft(StateMachine.BoardManager.CurrentButton);
            StateMachine.Input.PressedConfirm += EndTurn;
        }

        public override void Exit()
        {
            Debug.Log("Move Left Exit");
            StateMachine.Input.PressedConfirm -= EndTurn;
        }

        void MoveLeft(Button button)
        {
            button.transform.position = new Vector3(button.transform.position.x - 100, button.transform.position.y, button.transform.position.z);
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
