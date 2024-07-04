using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit: MonoBehaviour
{
    [Header("UserSystem")]
    [SerializeField] UnitInfo unitInfo;

    [SerializeField] GameObject marker;
    [SerializeField] NavMeshAgent nav;
    [SerializeField] Animator anim;
    [SerializeField] FSM fsm;

    [Header("Move")]
    [SerializeField] float stopDistance;
    [SerializeField] bool isMove = false;

    public UnitInfo UnitInfo { get => unitInfo; set => unitInfo = value; }


    //FSM ������ �սô�. �ϴ� ���� ���� �� ��


    // Start is called before the first frame update
    protected virtual void Start()
    {
        fsm = new FSM(new IdleState(this));
    }

    protected virtual void Update()
    {
        fsm.UpdateState();
    }



    public void SelectUnit()
    {
        marker.gameObject.SetActive(true);
    }
    public void DeSelectUnit()
    {
        marker.gameObject.SetActive(false);
    }


    #region Move

    public void ClearTarget()
    {
        StopCoroutine("MoveOn");
        nav.ResetPath();
    }
    public void MoveTo(Vector3 pos)
    {
        
    }
    public void MoveTo(List<PathNode> path)
    {
        anim.SetBool("isMove", true);
        fsm.ChangeState(new MoveState(this, path));
    }
    public void ArriveTarget()
    {
        anim.SetBool("isMove", false);
        fsm.ChangeState(new IdleState(this));
    }

    #endregion

    public void Hold()
    {
        anim.SetBool("isMove", false);
        fsm.ChangeState(new IdleState(this));
    }
}
