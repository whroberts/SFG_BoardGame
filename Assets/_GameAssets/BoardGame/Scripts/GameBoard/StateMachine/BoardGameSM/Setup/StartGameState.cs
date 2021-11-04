using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace BoardGame
{
    public class StartGameState : BoardGameState
    {
        [SerializeField] Button _startButton = null;
        [SerializeField] Button _winButton = null;
        [SerializeField] Button _loseButton = null;

        public override void Enter()
        {
            Debug.Log("Starting Game");
            _startButton.gameObject.SetActive(true);
            StartTurn();
        }

        public override void Exit()
        {
            Debug.Log("Started Game");
            _startButton.gameObject.SetActive(false);
        }

        private void StartTurn()
        {
            _startButton.onClick.AddListener(() => StartCoroutine(Transition()));
            _winButton.onClick.AddListener(WinGame);
            _loseButton.onClick.AddListener(LoseGame);
        }

        IEnumerator Transition()
        {
            yield return new WaitForSeconds(0.1f);

            OnClickSetCurrentButton();
        }

        private void OnClickSetCurrentButton()
        {
            foreach (GameObject piece in StateMachine.BoardManager.PlayerPieces)
            {
                Button newButton = piece.GetComponent<Button>();

                Navigation newNav = new Navigation();
                newNav.mode = Navigation.Mode.Explicit;
                newButton.navigation = newNav;

                newButton.interactable = true;
            }

            StateMachine.ChangeState<PlayerTurnBoardGameState>();
        }

        void WinGame()
        {
            StateMachine.ChangeState<WinBoardGameState>();
        }

        void LoseGame()
        {
            StateMachine.ChangeState<LoseBoardGameState>();
        }
    }
}
