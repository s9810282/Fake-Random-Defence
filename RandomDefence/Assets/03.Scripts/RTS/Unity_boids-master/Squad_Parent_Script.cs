using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Squad_Parent_Script : MonoBehaviour
{
    public GameObject target;
    public GameObject child_Prefab;

    public List<GameObject> children;

    [SerializeField] NavMeshAgent nav;

    private void Start()
    {
        //자식 오브젝트들이 BaseBehavior를 가지고 해당 오브젝트를 target으로 설정해야함

        Targeting();
    }

    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.R))
        {
            Targeting();
        }


        //transform.position += (target.transform.position - transform.position).normalized * Time.deltaTime * 5.0f;
    }

    public void Targeting()
    {
        nav.SetDestination(target.transform.position);
    }    
}
