using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardGame
{
    public abstract class State : MonoBehaviour
    {
        public virtual void Enter()
        {

        }

        public virtual void Tick()
        {

        }

        public virtual void Exit()
        {

        }
    }
}
