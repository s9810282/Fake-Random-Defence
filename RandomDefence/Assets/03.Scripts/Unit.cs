using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit: MonoBehaviour
{
    [Header("UserSystem")]
    [SerializeField] UnitInfo unitInfo;
    [SerializeField] UnitData tmpdata;

    [SerializeField] GameObject marker;
    [SerializeField] NavMeshAgent nav;
    [SerializeField] Animator anim;
    [SerializeField] FSM fsm;

    [Header("Move")]
    [SerializeField] float stopDistance;
    [SerializeField] bool isMove = false;
    [SerializeField] bool isAttack = false;

    [SerializeField] GameUnitData testData;

    public UnitInfo UnitInfo { get => unitInfo; set => unitInfo = value; }

    //FSM 쓰도록 합시다. 일단 무빙 구현 ㄱ ㄱ


    // Start is called before the first frame update
    protected virtual void Start()
    {
        unitInfo = tmpdata.unitInfo;
        testData.AddUnit(this);

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


    #region Attack

    public void Attack(IDamageAble target)
    {
        isMove = false;
        isAttack = true;

        anim.SetBool("isMove", false);
        anim.SetBool("isAttack", true);

        fsm.ChangeState(new AttackState(this, target));
    }

    public void AttackMove(IDamageAble target, List<PathNode> path)
    {
        fsm.ChangeState(new AttackMoveState(this, target, path, unitInfo.unitRange));
    }

    public void AtkAnimExitEvent()
    {

    }

    #endregion



    #region Move

    public void ClearTarget()
    {
        StopCoroutine("MoveOn");
        nav.ResetPath();
    }
    public void MoveTo(Vector3 pos)
    {
        
    }
    public void MoveTo(List<Vector3> path)
    {
        isMove = true;
        isAttack = false;

        anim.SetBool("isMove", true);
        anim.SetBool("isAttack", false);

        fsm.ChangeState(new MoveState(this, path));
    }
    public void ArriveTarget()
    {
        isMove = false;
        isAttack = false;

        anim.SetBool("isMove", false);
        anim.SetBool("isAttack", false);

        fsm.ChangeState(new IdleState(this));
    }

    #endregion



    public void Hold()
    {
        isMove = false;
        isAttack = false;

        anim.SetBool("isMove", false);
        anim.SetBool("isAttack", false);

        fsm.ChangeState(new IdleState(this));
    }
}
