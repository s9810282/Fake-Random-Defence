using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitRank
{
    Normal = 0,
    Rare,
    Unique,
    Legend,
    Epic,
}


[System.Serializable]
public class UnitInfo
{
    public Sprite unitSprite;
    public Sprite unitIconSprite;
    public UnitRank unitRank;

    public string unitName;
    public int unitMinAtk;
    public int unitMaxAtk;
    public int unitSpeed = 1;
    public int unitArm;
    public int unitRange = 3;
    public float unitAttackDelay = 0.5f;
}



[CreateAssetMenu(fileName = "new UnitData", menuName = "UnitData", order = 2)]
public class UnitData : ScriptableObject
{
    public UnitInfo unitInfo;
    
}


