using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

namespace BoardGame
{
    public class EnemyTurnBoardGameState : BoardGameState
    {
        public static event Action EnemyTurnBegan;
        public static event Action EnemyTurnEnded;

        [SerializeField] float _pauseDuration = 1.5f;
        [SerializeField] TMP_Text _enemyTurnsText = null;

        private int _enemyTurns = 0;

        public override void Enter()
        {
            Debug.Log("Enemy Turn: ...Enter");
            _enemyTurns++;
            _enemyTurnsText.text = "Enemy Turn: " + _enemyTurns.ToString();
            EnemyTurnBegan?.Invoke();

            StartCoroutine(EnemyThinkingRoutine(_pauseDuration));
        }

        public override void Exit()
        {
            Debug.Log("Enemy Turn: Exit...");
        }

        private IEnumerator EnemyThinkingRoutine(float pauseDuration)
        {
            Debug.Log("Enemy thinking...");
            SetButtons();

            yield return new WaitForSeconds(pauseDuration);

            Debug.Log("Enemy performs action");
            EnemyTurnEnded?.Invoke();
            ResetButtons();

            StateMachine.ChangeState<PlayerTurnBoardGameState>();
        }

        private void SetButtons()
        {
            foreach (GameObject piece in StateMachine.BoardManager.EnemyPieces)
            {
                Button newButton = piece.GetComponent<Button>();

                Navigation newNav = new Navigation();
                newNav.mode = Navigation.Mode.Explicit;
                newButton.navigation = newNav;
                newButton.interactable = true;
            }
        }

        private void ResetButtons()
        {
            foreach (GameObject piece in StateMachine.BoardManager.EnemyPieces)
            {
                Button newButton = piece.GetComponent<Button>();

                newButton.interactable = false;
            }
        }
    }
}
