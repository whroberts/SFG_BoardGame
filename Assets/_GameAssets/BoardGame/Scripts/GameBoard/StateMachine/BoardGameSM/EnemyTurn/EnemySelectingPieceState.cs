using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace BoardGame
{
    public class EnemySelectingPieceState : BoardGameState
    {
        [SerializeField] TMP_Text _enemyThinkingTextUI = null;
        [SerializeField] TMP_Text _enemyTurnsText = null;

        private int _enemyTurns = 0;
        [SerializeField] float _pauseDuration = 1.5f;

        public override void Enter()
        {
            Debug.Log("Enemy Selecting Piece");
            _enemyThinkingTextUI.gameObject.SetActive(true);
            _enemyTurns++;
            _enemyTurnsText.text = "Enemy Turn: " + _enemyTurns.ToString();

            StateMachine.BoardManager.SetCurrentButton(null);
            ResetButtons();
            StartCoroutine(SelectButton());
        }

        public override void Exit()
        {
            _enemyThinkingTextUI.gameObject.SetActive(false);
            Debug.Log("Enemy Selected Piece");
        }

        private void ResetButtons()
        {
            foreach (GameObject piece in StateMachine.BoardManager.EnemyPieces)
            {
                Button newButton = piece.GetComponent<Button>();

                Navigation newNav = new Navigation();
                newNav.mode = Navigation.Mode.Explicit;
                newButton.navigation = newNav;

                newButton.interactable = true;

                ColorBlock cb = newButton.colors;
                cb.disabledColor = Color.white;
                newButton.colors = cb;
            }
        }

        private IEnumerator SelectButton()
        {

            //Button button = StateMachine.BoardManager.EnemyPieces[Random.Range(0, StateMachine.BoardManager.EnemyPieces.Length)].GetComponent<Button>();
            Button button = StateMachine.BoardManager.EnemyPieces[12].GetComponent<Button>();

            StateMachine.BoardManager.SetCurrentButton(button);
            button.Select();

            Debug.Log(button.name);

            yield return new WaitForSeconds(_pauseDuration / 3);
            End();
        }

        void End()
        {
            StateMachine.ChangeState<EnemyMovingPieceState>();
        }
    }
}
