using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Idle,
    Move,
    Attack,
    Stop,
    Hold,
    Patrol,
}


public abstract class BaseState
{
    protected Unit unit;
    protected State state;

    protected BaseState(Unit unit, State state)
    {
        this.unit = unit;
        this.state = state;
    }


    public abstract void OnStateEnter();
    public abstract void OnStateUpdate();
    public abstract void OnStateExit();

}
