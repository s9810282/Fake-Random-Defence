using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EUnitState
{ 
    Idle,
    Move,
    Attack,
    AttackMove, // 공격 중 멀어지면 따라가면서 떄림
    Hold, //제자리를 유지하면서 적 감지 시 공격
}




[System.Serializable]
public class FSM
{
    public FSM(BaseState initState)
    {
        currentState = initState;
    }


    [SerializeField] BaseState currentState;
    [SerializeField] State current;

    public void ChangeState(BaseState changeState)
    {
        if (changeState == currentState)
            return;

        if (currentState != null)
            currentState.OnStateExit();

        currentState = changeState;
        currentState.OnStateEnter();
    }

    public void UpdateState()
    {
        if (currentState != null)
            currentState.OnStateUpdate();
    }
}
