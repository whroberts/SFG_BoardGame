using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CardGame
{
    public class CardGameUIController : MonoBehaviour
    {
        [SerializeField] Text _enemyThinkingTextUI = null;

        private void OnEnable()
        {
            EnemyTurnCardGameState.EnemyTurnBegan += OnEnemyTurnBegan;
            EnemyTurnCardGameState.EnemyTurnEnded += OnEnemyTurnEnded;
        }

        private void OnDisable()
        {
            EnemyTurnCardGameState.EnemyTurnBegan -= OnEnemyTurnBegan;
            EnemyTurnCardGameState.EnemyTurnEnded -= OnEnemyTurnEnded;
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
