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
        [SerializeField] TMP_Text _playerPiecesTaken = null;

        public override void Enter()
        {
            _playerPiecesTaken.text = "Pieces Taken: " + StateMachine.BoardManager.PiecesTakenByPlayer;

            StateMachine.BoardManager.SetCurrentButton(null);
            ResetButtons();
            AddListeners();
        }

        public override void Tick()
        {
            if (StateMachine.BoardManager.EnemyPieceList.Count <= 0)
            {
                StateMachine.ChangeState<WinBoardGameState>();
            }
        }

        public override void Exit()
        {
            //Debug.Log("Selected Piece");
        }

        private void ResetButtons()
        {
            foreach (GameObject piece in StateMachine.BoardManager.PlayerPieceList)
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
            foreach (GameObject piece in StateMachine.BoardManager.PlayerPieceList)
            {
                Button button = piece.GetComponent<Button>();
                button.onClick.AddListener(() => SelectButton(button));
            }
        }

        private void SelectButton(Button button)
        {
            StateMachine.BoardManager.SetCurrentButton(button);

            foreach (GameObject piece in StateMachine.BoardManager.PlayerPieceList)
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
