using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BoardGame
{
    public class WinBoardGameState : BoardGameState
    {

        bool _changedScene = false;

        public override void Enter()
        {
            Debug.Log("Entering Win State");
            SceneManager.LoadScene(2);
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
            Debug.Log("Leaving Win State");
        }
    }
}
