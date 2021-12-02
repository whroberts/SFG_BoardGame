using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace BoardGame
{
    public class EnemySelectingPieceState : BoardGameState
    {
        [SerializeField] TMP_Text _enemyPiecesTaken = null;
        [SerializeField] float _pauseDuration = 1.5f;

        public override void Enter()
        {
            Debug.Log("Enemy Selecting Piece");

            _enemyPiecesTaken.text = "Pieces Taken: " + StateMachine.BoardManager.PiecesTakenByEnemy;

            StateMachine.BoardManager.SetCurrentButton(null);
            ResetButtons();
            StartCoroutine(SelectButton());
        }

        public override void Tick()
        {
            if (StateMachine.BoardManager.PlayerPieceList.Count <= 0)
            {
                StateMachine.ChangeState<LoseBoardGameState>();
            }
        }

        public override void Exit()
        {
            Debug.Log("Enemy Selected Piece");
        }

        private void ResetButtons()
        {
            foreach (GameObject piece in StateMachine.BoardManager.EnemyPieceList)
            {
                Button newButton = piece.GetComponent<Button>();

                Navigation newNav = new Navigation();
                newNav.mode = Navigation.Mode.Explicit;
                newButton.navigation = newNav;

                newButton.interactable = true;
            }
        }

        private IEnumerator SelectButton()
        {

            Button button = StateMachine.BoardManager.EnemyPieceList[Random.Range(0, StateMachine.BoardManager.EnemyPieceList.Count)].GetComponent<Button>();

            StateMachine.BoardManager.SetCurrentButton(button);
            button.Select();

            yield return new WaitForSeconds(_pauseDuration / 3);
            End();
        }

        void End()
        {
            StateMachine.ChangeState<EnemyMovingPieceState>();
        }
    }
}
