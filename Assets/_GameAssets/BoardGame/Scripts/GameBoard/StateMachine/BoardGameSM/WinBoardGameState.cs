using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BoardGame
{
    public class WinBoardGameState : BoardGameState
    {
        public override void Enter()
        {
            Debug.Log("Entering Win State");
            SceneManager.LoadScene(2);
        }

        public override void Exit()
        {
            Debug.Log("Leaving Win State");
        }
    }
}
