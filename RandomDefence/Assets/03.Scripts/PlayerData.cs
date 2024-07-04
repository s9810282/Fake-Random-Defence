using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "new PlayerData", menuName = "PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    public string userNickName;

    public int gold;
    public int tree;
    public int meet;

    public int wispCount;
}


