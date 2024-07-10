using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("PlayerInfo")]
    [SerializeField] PlayerData playerData;
    [SerializeField] GameUnitData unitData;

    [Header("Top Bar")]
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI treeText;
    [SerializeField] TextMeshProUGUI meetText;


    [Header("InfoBox")]
    [Header("Unit Slot")]
    [SerializeField] GameObject unitUI;
    [SerializeField] GameObject[] itemSlot;
    [SerializeField] GameObject[] behavioirSlot;

    [Space(20f)]
    [SerializeField] Image selectUnitImage;
    [Space(10f)]
    [SerializeField] GameObject selectUnitInfo;
    [SerializeField] TextMeshProUGUI selectUnitRarityText;
    [SerializeField] TextMeshProUGUI selectUnitNameText;
    [SerializeField] TextMeshProUGUI selectUnitAtkText;
    [SerializeField] TextMeshProUGUI selectUnitArmText;

    [Space(20f)]
    [SerializeField] GameObject selectUnitsInfo;
    [SerializeField] UIIconSlot[] selectUnitsIcons;


    [Header("Build Slot")]
    [SerializeField] GameObject[] buildSlot;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void UnitSelectEvent()
    {
        DebugTool.Log("Unit Select");

        if (unitData.SelectedUnitList.Count == 0)
            return;

        Unit unit = unitData.SelectedUnitList[0];

        selectUnitInfo.gameObject.SetActive(true);
        selectUnitsInfo.gameObject.SetActive(false);


        selectUnitImage.sprite = unit.UnitInfo.unitSprite;
        selectUnitRarityText.text = unit.UnitInfo.unitRank.ToString();
        selectUnitNameText.text = unit.UnitInfo.unitName;
        selectUnitAtkText.text = unit.UnitInfo.unitMinAtk + " - " + unit.UnitInfo.unitMaxAtk;
        selectUnitArmText.text = unit.UnitInfo.unitArm.ToString();

    }


    public void UnitsSelectEvent()
    {
        DebugTool.Log("Units Select");

        if (unitData.SelectedUnitList.Count == 0)
            return;

        selectUnitInfo.gameObject.SetActive(false);
        selectUnitsInfo.gameObject.SetActive(true);

        List<Unit> units = unitData.SelectedUnitList;

        selectUnitImage.sprite = units[0].UnitInfo.unitSprite;

        for (int i = 0; i < selectUnitsIcons.Length; i++)
            selectUnitsIcons[i].gameObject.SetActive(false);

        for (int i = 0; i < units.Count; i++)
        {
            selectUnitsIcons[i].SlotIcon.sprite = units[i].UnitInfo.unitIconSprite;
            selectUnitsIcons[i].gameObject.SetActive(true);
        }


    }



}
