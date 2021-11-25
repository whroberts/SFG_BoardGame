using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace BoardGame
{
    public class ChooseGamePieceState : GamePieceState
    {
        [SerializeField] Canvas _gamePieceUI = null;
        [SerializeField] TMP_Text _chosenPieceTitle = null;
        [SerializeField] TMP_Text _controlsText = null;
        [SerializeField] TMP_Text _chosePieceState = null;

        public override void Enter()
        {
            Debug.Log("Entering Choose Piece State");
            _chosePieceState.gameObject.SetActive(true);
            OnEnter();
        }

        public override void Exit()
        {
            Debug.Log("Exiting Choose Piece State");
            StateMachine.Input.PressedConfirm -= OnPressedConfirm;
            StateMachine.Input.PressedCancel -= OnPressedCancel;
            _chosePieceState.gameObject.SetActive(false);
            _controlsText.gameObject.SetActive(false);
        }

        void OnEnter()
        {
            if (StateMachine.BoardManager.PlayerCurrentButton != null)
            {
                ReselectPiece();
            }
            else
            {
                AddListeners();
            }

            _chosenPieceTitle.gameObject.SetActive(true);
            _controlsText.gameObject.SetActive(true);
            _gamePieceUI.gameObject.SetActive(true);
        }

        void AddListeners()
        {
            foreach (GameObject piece in StateMachine.BoardManager.PlayerPieceList)
            {
                Button button = piece.GetComponent<Button>();
                button.onClick.AddListener(() => OnClickButton(button));
            }
        }

        void ReselectPiece()
        {
            StateMachine.BoardManager.SetCurrentButton(null);
            Navigation newNav = new Navigation();

            foreach (GameObject piece in StateMachine.BoardManager.PlayerPieceList)
            {
                Button newButton = piece.GetComponent<Button>();
                newButton.interactable = true;

                newNav.mode = Navigation.Mode.None;
                newButton.navigation = newNav;

                newNav.mode = Navigation.Mode.Explicit;
                newButton.navigation = newNav;

                newButton.onClick.RemoveAllListeners();
            }

            AddListeners();
        }

        public void OnClickButton(Button button)
        {
            StateMachine.BoardManager.SetCurrentButton(button);

            foreach (GameObject piece in StateMachine.BoardManager.PlayerPieceList)
            {
                if (piece.name != button.name)
                {
                    Button newButton = piece.GetComponent<Button>();
                    newButton.interactable = false;
                }
            }

            OnPressedConfirm();
        }

        public void ChooseGamePiece(Button button)
        {
            StateMachine.Input.PressedConfirm += OnPressedConfirm;
            StateMachine.Input.PressedCancel += OnPressedCancel;
        }

        private void OnPressedConfirm()
        {
            StateMachine.ChangeState<GamePieceIdleState>();
        }

        private void OnPressedCancel()
        {
            StateMachine.BoardManager.SetCurrentButton(null);

            foreach (GameObject piece in StateMachine.BoardManager.PlayerPieceList)
            {
                Button newButton = piece.GetComponent<Button>();
                newButton.interactable = true;
            }
        }
    }
}
