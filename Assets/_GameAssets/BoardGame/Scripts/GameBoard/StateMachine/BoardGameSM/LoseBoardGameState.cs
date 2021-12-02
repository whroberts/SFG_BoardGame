using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BoardGame
{
    public class LoseBoardGameState : BoardGameState
    {
        bool _changedScene = false;

        AudioManager _audioManager;

        public override void Enter()
        {
            Debug.Log("Entering Lose State");
            SceneManager.LoadScene(3);
            _changedScene = true;

            _audioManager = FindObjectOfType<AudioManager>();
            StartCoroutine(_audioManager.PlayLoseSound());
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
            Debug.Log("Leaving Lose State");
        }
    }
}
