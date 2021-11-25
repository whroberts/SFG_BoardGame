using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BoardGame
{
    public class BoardGameTransitionState : BoardGameState
    {
        [SerializeField] GameObject _gamePieceController = null;
        public override void Enter()
        {
            Debug.Log("Entering Board Game Transition State");
            _gamePieceController.SetActive(true);
            gameObject.SetActive(false);

        }

        public override void Exit()
        {
            Debug.Log("Exiting Board Game Transition State");
            StateMachine.BoardManager.SetCurrentButton(null);
            
            foreach (GameObject piece in StateMachine.BoardManager.PlayerPieceList)
            {
                Button button = piece.GetComponent<Button>();
                button.onClick.RemoveAllListeners();
            }
        }
    }
}
