using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit: MonoBehaviour
{
    [SerializeField] GameObject marker;
    [SerializeField] NavMeshAgent nav;

    [SerializeField] float stopDistance;

    [SerializeField] Animator anim;
    [SerializeField] bool isMove = false;

    [SerializeField] FSM fsm;

    //FSM 쓰도록 합시다. 일단 무빙 구현 ㄱ ㄱ


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
}
