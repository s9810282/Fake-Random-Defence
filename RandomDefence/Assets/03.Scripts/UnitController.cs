using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{

    [SerializeField] List<Unit> selectedUnitList = new List<Unit>();
    [SerializeField] List<Unit> unitList = new List<Unit>();


    void Start()
    {
        selectedUnitList = new List<Unit>();
        //unitList = new List<Unit>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UnitMoveTo(Vector3 vector)
    {
        for (int i = 0; i < selectedUnitList.Count; i++)
            selectedUnitList[i].MoveTo(vector);
    }


    public void DragSelect(Unit unit)
    {
        //if(selectedUnitList.Count <= 0)
        //    SelectUnit(unit);

        if (!selectedUnitList.Contains(unit))
        {
            SelectUnit(unit);
        }
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
        Debug.Log("RemoveAll");

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

    public List<Unit> ReturnUnitList()
    {
        return unitList;
    }
}
