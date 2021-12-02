using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BoardGame
{
    public class WinBoardGameState : BoardGameState
    {

        bool _changedScene = false;

        AudioManager _audioManager;

        public override void Enter()
        {
            Debug.Log("Entering Win State");
            SceneManager.LoadScene(2);
            _changedScene = true;

            _audioManager = FindObjectOfType<AudioManager>();
            StartCoroutine(_audioManager.PlayWinSound());
        }

        public override void Tick()
        {
            if (_changedScene)
            {
                Exit();
            }
        }

        public override void Exit()
        {
            Debug.Log("Leaving Win State");
        }
    }
}
