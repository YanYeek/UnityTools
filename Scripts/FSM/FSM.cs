using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YanYeek
{

    public class FSM
    {
        private Dictionary<int, FSMState> statesDict = new Dictionary<int, FSMState>();
        public FSMState currentState { get; private set; }

        public void OnUpdate()
        {
            currentState.Action();
            currentState.Reason(this);
        }

        public void SetState(FSMState state)
        {
            currentState.OnEnter();

            currentState = state;

            currentState.OnExit();
        }
    }
}