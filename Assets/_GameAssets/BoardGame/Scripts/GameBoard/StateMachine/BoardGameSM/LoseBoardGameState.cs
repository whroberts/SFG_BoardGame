using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BoardGame
{
    public class LoseBoardGameState : BoardGameState
    {
        bool _changedScene = false;

        public override void Enter()
        {
            Debug.Log("Entering Lose State");
            SceneManager.LoadScene(3);
            _changedScene = true;
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
