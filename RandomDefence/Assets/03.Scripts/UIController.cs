using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("PlayerInfi")]
    [SerializeField] PlayerData playerData;

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

    [SerializeField] GameObject selectUnitInfo;
    [SerializeField] TextMeshProUGUI selectUnitRarityText;
    [SerializeField] TextMeshProUGUI selectUnitNameText;
    [SerializeField] TextMeshProUGUI selectUnitAtkText;
    [SerializeField] TextMeshProUGUI selectUnitArmText;

    [Space(20f)]
    [SerializeField] GameObject selectUnitsInfo;
    [SerializeField] GameObject[] selectUnitsIcons;


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

    }
}
