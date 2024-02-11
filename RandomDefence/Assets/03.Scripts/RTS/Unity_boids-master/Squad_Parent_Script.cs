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
        //�ڽ� ������Ʈ���� BaseBehavior�� ������ �ش� ������Ʈ�� target���� �����ؾ���

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
