using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageAble
{
    [Header("Enemy")]
    [SerializeField] EnemyInfo enemyInfo;
    [SerializeField] EnemyData tmpdata;

    // Start is called before the first frame update
    void Start()
    {
        enemyInfo = tmpdata.enemyInfo;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void IDamageAble.OnDamaged(float damage)
    {
        enemyInfo.enemyHP -= damage;

        if(enemyInfo.enemyHP <= 0)
        {
            enemyInfo.enemyHP = 0;
            Dead();
        }
    }

    public void Dead()
    {

    }

    public Transform GetTransform()
    {
        return transform;
    }

}
