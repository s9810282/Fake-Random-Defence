using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squad_Parent_Script : MonoBehaviour
{
    public GameObject target;
    public GameObject child_Prefab;

    public List<GameObject> children;


    private void Start()
    {
        //�ڽ� ������Ʈ���� BaseBehavior�� ������ �ش� ������Ʈ�� target���� �����ؾ���
    }

    private void Update()
    {
        transform.position += (target.transform.position - transform.position).normalized * Time.deltaTime * 5.0f;
    }
}
