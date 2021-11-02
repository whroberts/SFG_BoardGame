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
        [SerializeField] BoardManager _bm = null;

        [SerializeField] TMP_Text _playerTurnTextUI = null;

        private int _playerTurnCount = 0;

        public override void Enter()
        {
            Debug.Log("Player Turn: ...Entering");
            _playerTurnTextUI.gameObject.SetActive(true);

            _playerTurnCount++;
            _playerTurnTextUI.text = "Player Turn: " + _playerTurnCount.ToString();

            // hook into events
            StateMachine.Input.PressedConfirm += OnPressedConfirm;

            StartTurn();
        }

        public override void Exit()
        {
            _playerTurnTextUI.gameObject.SetActive(false);
            
            // unhook into events
            StateMachine.Input.PressedConfirm -= OnPressedConfirm;

            Debug.Log("Player Turn: Exiting...");
        }

        private void OnPressedConfirm()
        {
            Debug.Log("Attempt to enter Enemy State!");

            // change the enemy turn state
            StateMachine.ChangeState<EnemyTurnBoardGameState>();
        }

        private void StartTurn()
        {
            foreach (GameObject piece in _bm.PlayerPieces)
            {
                Button button = piece.GetComponent<Button>();
                button.interactable = true;

                button.onClick.AddListener(() => OnClickButton(button));
                //button.onClick.AddListener(EndTurn);
            }
        }
        
        private void EndTurn()
        {
            foreach (GameObject piece in _bm.PlayerPieces)
            {
                Button button = piece.GetComponent<Button>();
                button.interactable = false;
                button.onClick.RemoveAllListeners();
            }
            StateMachine.ChangeState<EnemyTurnBoardGameState>();
        }
        public void OnClickButton(Button button)
        {
            //GamePieceStateMachine.ChangeState<PieceSetupState>();
        }
    }
}
