using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace BoardGame
{
    public class PlayerSelectingPieceState : BoardGameState
    {
        [SerializeField] TMP_Text _playerTurnTextUI = null;

        private int _playerTurnCount = 0;

        public override void Enter()
        {
            Debug.Log("Selecting Piece");

            _playerTurnCount++;
            _playerTurnTextUI.text = "Player Turn: " + _playerTurnCount.ToString();

            StateMachine.BoardManager.SetCurrentButton(null);
            ResetButtons();
            AddListeners();
        }

        public override void Exit()
        {
            //_playerTurnTextUI.gameObject.SetActive(false);
            Debug.Log("Selected Piece");
        }

        private void ResetButtons()
        {
            foreach (GameObject piece in StateMachine.BoardManager.PlayerPieces)
            {
                Button newButton = piece.GetComponent<Button>();

                Navigation newNav = new Navigation();
                newNav.mode = Navigation.Mode.Explicit;
                newButton.navigation = newNav;

                newButton.interactable = true;
                newButton.onClick.RemoveAllListeners();
            }
        }

        private void AddListeners()
        {
            foreach (GameObject piece in StateMachine.BoardManager.PlayerPieces)
            {
                Button button = piece.GetComponent<Button>();
                button.onClick.AddListener(() => SelectButton(button));
            }
        }

        private void SelectButton(Button button)
        {
            StateMachine.BoardManager.SetCurrentButton(button);

            foreach (GameObject piece in StateMachine.BoardManager.PlayerPieces)
            {
                if (piece.name != button.name)
                {
                    Button newButton = piece.GetComponent<Button>();
                    newButton.interactable = false;
                    newButton.onClick.RemoveAllListeners();
                }
                button.onClick.RemoveAllListeners();
            }
            StateMachine.ChangeState<PlayerMovingPieceState>();
        }
    }
}
