using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MoveState : BaseState
{
    private List<Vector3> movePath;

    private bool isMove;
    private Vector3 targetPos;
    private int pathCount;
    private int currentCount;

    
    public MoveState(Unit unit, List<Vector3> path) : base(unit, State.Move)
    {
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

    public void MoveTo(List<Vector3> path)
    {
        isMove = true;
        movePath = path;
        pathCount = path.Count;
        currentCount = 0;

        targetPos = new Vector3(path[currentCount].x, 0.2f, path[currentCount].z);
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
            targetPos = movePath[currentCount];
            targetPos = new Vector3(movePath[currentCount].x, 0.2f, movePath[currentCount].z);
        }
    }


    public void Stop()
    {

    }

    public void ResetPath()
    {

    }

}