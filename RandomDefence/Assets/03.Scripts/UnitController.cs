using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{

    [SerializeField] List<Unit> selectedUnitList;
    [SerializeField] List<Unit> unitList;


    void Start()
    {
        selectedUnitList = new List<Unit>();
        unitList = new List<Unit>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickSelectUnit(Unit unit)
    {
        DeSelectUnitAll();

        SelectUnit(unit);
    }

    public void ShiftClickSelectUnit(Unit unit)
    {
        if(selectedUnitList.Contains(unit))
        {
            DeSelectUnit(unit);
        }
        else
        {
            SelectUnit(unit);

            Debug.Log("shift");
        }
    }

    public void DeSelectUnitAll()
    {
        for (int i = 0; i < selectedUnitList.Count; i++)
            selectedUnitList[i].DeSelectUnit();

        selectedUnitList.Clear();
    }

    public void SelectUnit(Unit unit)
    {
        selectedUnitList.Add(unit);
        unit.SelectUnit();
    }

    public void DeSelectUnit(Unit unit)
    {
        selectedUnitList.Remove(unit);
        unit.DeSelectUnit();
    }
}
