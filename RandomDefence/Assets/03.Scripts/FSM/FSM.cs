using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EUnitState
{ 
    Idle,
    Move,
    Attack,
    AttackMove, // ���� �� �־����� ���󰡸鼭 ����
    Hold, //���ڸ��� �����ϸ鼭 �� ���� �� ����
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
