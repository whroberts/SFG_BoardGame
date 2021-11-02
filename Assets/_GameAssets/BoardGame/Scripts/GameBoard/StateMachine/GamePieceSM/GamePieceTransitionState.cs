using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BoardGame
{
    public class GamePieceTransitionState : GamePieceState
    {
        [SerializeField] GameObject _boardGameController = null;
        public override void Enter()
        {
            Debug.Log("Entering Game Piece Transition State");
            _boardGameController.SetActive(true);
            gameObject.SetActive(false);
        }

        public override void Exit()
        {
            Debug.Log("Exiting Game Piece Transition State");
        }
    }
}
