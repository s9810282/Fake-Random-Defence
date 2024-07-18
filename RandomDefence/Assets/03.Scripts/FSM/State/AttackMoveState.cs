using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMoveState : BaseState
{
    private List<PathNode> movePath;
    private IDamageAble target;

    private bool isMove;
    private Vector3 targetPos;
    private int pathCount;
    private int currentCount;

    float attackRange;

    public AttackMoveState(Unit unit, IDamageAble target, List<PathNode> path, float attackRange) : base(unit, State.Move)
    {
        this.target = target;
        this.attackRange = attackRange;
        movePath = path;
        currentCount = 0;
    }


    public override void OnStateEnter()
    {
        MoveTo(movePath);
    }

    public override void OnStateExit()
    {

    }

    public override void OnStateUpdate()
    {
        if (isMove)
        {
            var step = 10f * Time.deltaTime; // calculate distance to move
            unit.transform.position = Vector3.MoveTowards(unit.transform.position, targetPos, step);
            unit.transform.LookAt(targetPos, Vector3.up);
        }

        ChekcMoveTarget();
    }



    public void MoveTo(List<PathNode> path)
    {
        isMove = true;
        movePath = path;
        pathCount = path.Count;
        currentCount = 0;

        targetPos = new Vector3(path[currentCount].worldPosition.x, 0.2f, path[currentCount].worldPosition.z);
    }

    public void MoveTo(Vector3 pos)
    {

    }

    //음 도착지 도착했을 때 FSM 즉, Unit측으로 어떻게 회신할 것인가
    //1. delegate (func, action 등 을 이용하여 Invoke
    //2. 외부에 관찰자를 따로 설정하여 전달
    //3. 아 이게 State가 Unit을 들고있는게 말이 안되는데 ㄹㅇ

    public void ChekcMoveTarget()
    {
        if (Vector3.Distance(unit.transform.position,
           target.GetTransform().position) < attackRange)
        {
            DebugTool.Log("Arrive Target");

            isMove = false;
            unit.Attack(target);
            return;
        }


        if (Vector3.Distance(unit.transform.position, targetPos) < 1)
        {
            if (currentCount == movePath.Count - 1)
            {
                //목적지 도착
                DebugTool.Log("Arrive Pos");
                isMove = false;
                unit.Attack(target);
                return;
            }

            currentCount++;
            targetPos = movePath[currentCount].worldPosition;
            targetPos = new Vector3(movePath[currentCount].worldPosition.x, 0.2f, movePath[currentCount].worldPosition.z);
        }
    }


    public void Stop()
    {

    }

    public void ResetPath()
    {

    }

}