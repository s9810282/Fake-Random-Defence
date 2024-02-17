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


    //FSM 쓰도록 합시다. 일단 무빙 구현 ㄱ ㄱ


    // Start is called before the first frame update
    void Start()
    {
        //MoveTo(new Vector3(10, transform.position.y, 10));
    }

    private void Update()
    {
        
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
        StopCoroutine("MoveOn");

        nav.SetDestination(pos);

        StartCoroutine("MoveOn");
    }

    IEnumerator MoveOn()
    {
        Debug.Log("Move");

        while(true)
        {
            if (Vector3.Distance(transform.position, nav.destination) < stopDistance)
            {
                transform.position = nav.destination;
                nav.ResetPath();

                break;
            }

            yield return null;
        }
    }
}
