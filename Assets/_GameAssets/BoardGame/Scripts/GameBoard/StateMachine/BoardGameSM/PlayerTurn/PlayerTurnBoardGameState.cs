using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace BoardGame
{
    public class PlayerTurnBoardGameState : BoardGameState
    {
        [SerializeField] TMP_Text _playerTurnTextUI = null;

        private int _playerTurnCount = 0;

        public override void Enter()
        {
            Debug.Log("Player Turn: ...Entering");
            _playerTurnTextUI.gameObject.SetActive(true);

            _playerTurnCount++;
            _playerTurnTextUI.text = "Player Turn: " + _playerTurnCount.ToString();

            StateMachine.Input.PressedConfirm += OnClickButton;

            StartTurn();
        }

        public override void Exit()
        {
            //_playerTurnTextUI.gameObject.SetActive(false);
            StateMachine.Input.PressedConfirm -= OnClickButton;

            Debug.Log("Player Turn: Exiting...");
        }

        private void StartTurn()
        {
            foreach (GameObject piece in StateMachine.BoardManager.PlayerPieces)
            {
                Button button = piece.GetComponent<Button>();
                button.interactable = true;
                button.onClick.AddListener(() => StateMachine.BoardManager.GetCurrentButton(button));
            }

        }

        public void OnClickButton()
        {
            foreach (GameObject piece in StateMachine.BoardManager.PlayerPieces)
            {
                if (piece.name != StateMachine.BoardManager.CurrentButton.name)
                {
                    Button newButton = piece.GetComponent<Button>();
                    newButton.interactable = false;
                }
            }
            StateMachine.ChangeState<BoardGameTransitionState>();
        }
    }
}
