using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    public IdleState(Unit unit) : base(unit, State.Idle)
    {

    }


    public override void OnStateEnter()
    {
        Debug.Log("Enter Idle");
    }

    public override void OnStateExit()
    {
        Debug.Log("Exit Idle");
    }

    public override void OnStateUpdate()
    {

    }
}
