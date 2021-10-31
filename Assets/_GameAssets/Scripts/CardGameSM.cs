using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGameSM : StateMachine
{
    private void Start()
    {
        // set starting State here
        ChangeState<SetupCardGameState>();

    }

}
