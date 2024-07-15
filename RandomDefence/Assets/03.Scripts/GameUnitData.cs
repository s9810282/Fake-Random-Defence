using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "new GameUnitData", menuName = "GameUnitData", order = 1)]
public class GameUnitData : ScriptableObject
{
    [SerializeField] List<Unit> selectedUnitList = new List<Unit>();
    [SerializeField] List<Unit> unitList = new List<Unit>();

    public List<Unit> SelectedUnitList { get => selectedUnitList; set => selectedUnitList = value; }
    public List<Unit> UnitList { get => unitList; set => unitList = value; }


    public void Init()
    {
        selectedUnitList = new List<Unit>();
        unitList = new List<Unit>();
    }

    public bool AddSelectUnit(Unit unit)
    {
        if (!selectedUnitList.Contains(unit))
        {
            if(selectedUnitList.Count >= 8)
            {
                selectedUnitList.RemoveAt(0);
            }

            selectedUnitList.Add(unit);
            return true;
        }

        return false;
    }
    public bool AddUnit(Unit unit)
    {
        if (!unitList.Contains(unit))
        {
            Debug.Log("Add");
            unitList.Add(unit);
            return true;
        }

        return false;
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
    public void DeSelectUnitAll()
    {
        for (int i = 0; i < selectedUnitList.Count; i++)
            selectedUnitList[i].DeSelectUnit();

        selectedUnitList.Clear();
    }

    public void UnitMoveTo(Vector3 vector)
    {
        for (int i = 0; i < selectedUnitList.Count; i++)
            selectedUnitList[i].MoveTo(vector);
    }
    public void UnitMoveTo(List<PathNode> path)
    {
        for (int i = 0; i < selectedUnitList.Count; i++)
            selectedUnitList[i].MoveTo(path);
    }
    public void UnitMoveTo(List<PathNode> path, int index)
    {
        if (selectedUnitList[index] != null)
            selectedUnitList[index].MoveTo(path);
    }

}
