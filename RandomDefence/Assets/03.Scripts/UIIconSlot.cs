using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIIconSlot : MonoBehaviour
{
    [SerializeField] GameObject slot;
    [SerializeField] Image slotBG;
    [SerializeField] Image slotIcon;


    public GameObject Slot { get => slot; set => slot = value; }
    public Image SlotBG { get => slotBG; set => slotBG = value; }
    public Image SlotIcon { get => slotIcon; set => slotIcon = value; }
}
