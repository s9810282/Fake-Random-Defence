using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyType
{
    Normal,
    Build,
    Boss,
}


[System.Serializable]
public class EnemyInfo
{
    public Sprite enemySprite;
    public Sprite enemyIconSprite;
    public EnemyType enemyRank;

    public string enemyName;
    public float enemyHP = 10;
    public int enemyMinAtk;
    public int enemyMaxAtk;
    public int enemySpeed = 1;
    public int enemyArm;
    public int enemyRange = 3;
}



[CreateAssetMenu(fileName = "new EnemyData", menuName = "EnemyData", order = 2)]
public class EnemyData : ScriptableObject
{
    public EnemyInfo enemyInfo;
    
}


