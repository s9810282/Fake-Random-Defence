using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit: MonoBehaviour
{
    [SerializeField] GameObject marker;
    [SerializeField] NavMeshAgent nav;

    [SerializeField] Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        //MoveTo(new Vector3(10, transform.position.y, 10));
    }

    public void SelectUnit()
    {
        marker.gameObject.SetActive(true);
    }

    public void DeSelectUnit()
    {
        marker.gameObject.SetActive(false);
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
            if (Vector3.Distance(transform.position, nav.destination) < 0.1f)
            {
                transform.position = nav.destination;
                nav.ResetPath();

                break;
            }

            yield return null;
        }
    }
}
