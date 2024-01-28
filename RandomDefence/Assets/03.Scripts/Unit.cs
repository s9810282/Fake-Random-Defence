using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit: MonoBehaviour
{
    [SerializeField] GameObject marker;
    [SerializeField] NavMeshAgent nav;

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
        nav.SetDestination(pos);
    }
}
