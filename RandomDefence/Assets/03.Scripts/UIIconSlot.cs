using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIIconSlot : MonoBehaviour
{
    [SerializeField] GameObject slot;
    [SerializeField] Image slotBG;
    [SerializeField] Image slotIcon;

    [SerializeField] Button button;
    [SerializeField] Unit unit;


    public GameObject Slot { get => slot; set => slot = value; }
    public Image SlotBG { get => slotBG; set => slotBG = value; }
    public Image SlotIcon { get => slotIcon; set => slotIcon = value; }
    public Unit Unit { get => unit; set => unit = value; }
    public Button Button { get => button; set => button = value; }


}
