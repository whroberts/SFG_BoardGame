using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BoardGame
{
    public class LoseBoardGameState : BoardGameState
    {
        public override void Enter()
        {
            Debug.Log("Entering Lose State");
            SceneManager.LoadScene(3);
        }

        public override void Exit()
        {
            Debug.Log("Leaving Lose State");
        }
    }
}
