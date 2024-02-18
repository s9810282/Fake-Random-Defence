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

    //�� ������ �������� �� FSM ��, Unit������ ��� ȸ���� ���ΰ�
    //1. delegate (func, action �� �� �̿��Ͽ� Invoke
    //2. �ܺο� �����ڸ� ���� �����Ͽ� ����

    public void ChekcMoveTarget()
    {
        if (Vector3.Distance(unit.transform.position, targetPos) < 1f)
        {
            if(currentCount == movePath.Count - 1)
            {
                //������ ����
                isMove = false;
                unit.ArriveTarget(); //�ٽ� Idle ���·�
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