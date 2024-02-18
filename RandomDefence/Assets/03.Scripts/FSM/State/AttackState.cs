using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    public AttackState(Unit unit) : base(unit, State.Attack)
    {

    }

    public override void OnStateEnter()
    {
        Debug.Log("Enter Attack");
    }

    public override void OnStateExit()
    {
        Debug.Log("Exit Attack");
    }

    public override void OnStateUpdate()
    {
       
    }
}
