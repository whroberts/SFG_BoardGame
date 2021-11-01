using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace BoardGame
{
    public class BoardGameUIController : MonoBehaviour
    {
        [SerializeField] TMP_Text _enemyThinkingTextUI = null;

        private void OnEnable()
        {
            EnemyTurnBoardGameState.EnemyTurnBegan += OnEnemyTurnBegan;
            EnemyTurnBoardGameState.EnemyTurnEnded += OnEnemyTurnEnded;
        }

        private void OnDisable()
        {
            EnemyTurnBoardGameState.EnemyTurnBegan -= OnEnemyTurnBegan;
            EnemyTurnBoardGameState.EnemyTurnEnded -= OnEnemyTurnEnded;
        }

        private void Start()
        {
            // make sure text is disabled on start
            _enemyThinkingTextUI.gameObject.SetActive(false);
        }

        private void OnEnemyTurnBegan()
        {
            _enemyThinkingTextUI.gameObject.SetActive(true);
        }

        private void OnEnemyTurnEnded()
        {
            _enemyThinkingTextUI.gameObject.SetActive(false);
        }
    }
}
