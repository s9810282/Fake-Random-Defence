using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : BaseState
{
    private List<PathNode> movePath;

    private bool isMove;
    private Vector3 targetPos;
    private int pathCount;
    private int currentCount;

    public MoveState(Unit unit, List<PathNode> path) : base(unit, State.Move)
    {
        movePath = path;
        currentCount = 0;
    }


    public override void OnStateEnter()
    {
        Debug.Log("Enter Move");
        MoveTo(movePath);
    }

    public override void OnStateExit()
    {
        Debug.Log("Exit Move");
    }

    public override void OnStateUpdate()
    {
        if (isMove)
        {
            Debug.Log("Update Move");

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
        

        Debug.Log("Move");
    }

    public void MoveTo(Vector3 pos)
    {

    }

    //음 도착지 도착했을 때 FSM 즉, Unit측으로 어떻게 회신할 것인가
    //1. delegate (func, action 등 을 이용하여 Invoke
    //2. 외부에 관찰자를 따로 설정하여 전달

    public void ChekcMoveTarget()
    {
        if (Vector3.Distance(unit.transform.position, targetPos) < 1f)
        {
            if(currentCount == movePath.Count - 1)
            {
                //목적지 도착
                isMove = false;
                unit.ArriveTarget(); //다시 Idle 상태로
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