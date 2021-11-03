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

            StartCoroutine(Transition(null));
        }

        public override void Exit()
        {
            //_playerTurnTextUI.gameObject.SetActive(false);

            Debug.Log("Player Turn: Exiting to chose piece");
        }

        IEnumerator Transition(Button button)
        {
            yield return new WaitForSeconds(0.1f);

            StateMachine.BoardManager.SetCurrentButton(button);
            ResetButtons();
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

            //StateMachine.BoardManager.SetCurrentButton(button);
            StateMachine.ChangeState<BoardGameTransitionState>();
        }
    }
}
