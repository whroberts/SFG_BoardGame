using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CardGame
{
    public class PlayerTurnCardGameState : CardGameState
    {
        [SerializeField] Text _playerTurnTextUI = null;

        private int _playerTurnCount = 0;

        public override void Enter()
        {
            Debug.Log("Player Turn: ...Entering");
            _playerTurnTextUI.gameObject.SetActive(true);

            _playerTurnCount++;
            _playerTurnTextUI.text = "Player Turn: " + _playerTurnCount.ToString();

            // hook into events
            StateMachine.Input.PressedConfirm += OnPressedConfirm;
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
            StateMachine.ChangeState<EnemyTurnCardGameState>();
        }
    }
}
