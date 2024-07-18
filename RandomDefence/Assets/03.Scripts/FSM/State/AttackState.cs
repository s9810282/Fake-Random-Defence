using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    IDamageAble target;

    bool isAttack;

    int attackRange;
    float attackDelay;


    public AttackState(Unit unit, IDamageAble target) : base(unit, State.Attack)
    {
        this.target = target;
    }

    public override void OnStateEnter()
    {
        isAttack = true;

    }

    public override void OnStateExit()
    {
        this.target = null;
    }

    public override void OnStateUpdate()
    {
       
    }
}
