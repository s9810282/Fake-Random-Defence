using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnitController : MonoBehaviour
{

    [SerializeField] PathFinding pathFinding;

    [SerializeField] int maxX;
    [SerializeField] int maxY;
    [SerializeField] float cellSize;
    [SerializeField] float scale;
    [SerializeField] Vector3 origin;

    [SerializeField] GameUnitData gameUnitData;

    [SerializeField] bool isDebug = true;

    [Header("Event")]
    [SerializeField] GameEvent unitSelectEvent;
    [SerializeField] GameEvent unitsSelectEvent;


    private void OnEnable()
    {
        DebugTool.isDebug = isDebug;
        gameUnitData.Init();

    }

    void Start()
    {
        pathFinding = new PathFinding(maxX, maxY, cellSize, origin);
    }

    
    void Update()
    {

    }



    #region Move

    public void PathFinding(Vector3 pos) //이걸 선택한 유닛수만큼 따로 따로 돌려야함
    {
        //이동불가 지역인가? 


        pathFinding.GetGrid().GetXY3D(pos, out int x, out int y);

        int count = 0;

        for(int i = 0; i < gameUnitData.SelectedUnitList.Count; i++)
        {
            count += i;

            pathFinding.GetGrid().GetXY3D(gameUnitData.SelectedUnitList[i].transform.position, out int a, out int b);
            List<PathNode> path = pathFinding.FindPath(a, b, x, y);

            UnitMoveTo(path, i);
        }

        //pathFinding.GetGrid().GetXY3D(testUnit.transform.position, out int a, out int b);
        //List<PathNode> path = pathFinding.FindPath(a, b, x, y); //지금은 그냥 0,0 인덱스부터 검색
            
        //UnitMoveTo(path);

        //if (path != null)
        //{
        //    DebugTool.DrawLine(transform.position, path[0].worldPosition, Color.black, 10f);

        //    for (int i = 0; i < path.Count - 1; i++)
        //    {
        //        //DebugTool.DrawLine(new Vector3(path[i].x, 0, path[i].y) * cellSize + Vector3.one * 2.5f, new Vector3(path[i + 1].x, 0, path[i + 1].y) * cellSize + Vector3.one * 2.5f, Color.black, 10f);
        //        DebugTool.DrawLine(path[i].worldPosition, path[i + 1].worldPosition, Color.black, 10f);
        //    }
        //}
    }


    public void UnitMoveTo(Vector3 vector)
    {
        gameUnitData.UnitMoveTo(vector);
    }

    public void UnitMoveTo(List<PathNode> path)
    {
        gameUnitData.UnitMoveTo(path);
    }
    public void UnitMoveTo(List<PathNode> path, int index)
    {
        gameUnitData.UnitMoveTo(path, index);
    }

    #endregion



    #region Select

    public void DragSelect(Unit unit)
    {
        //if(selectedUnitList.Count <= 0)
        //    SelectUnit(unit);

        if (!gameUnitData.SelectedUnitList.Contains(unit))
        {
            SelectUnit(unit);
        }

        unitsSelectEvent.Raise();
    }

    public void ClickSelectUnit(Unit unit)
    {
        DeSelectUnitAll();
        SelectUnit(unit);

        unitSelectEvent.Raise();
    }
    public void ClickSelectUnit(UIIconSlot slot)
    {
        Unit unit = slot.Unit;

        DeSelectUnitAll();
        SelectUnit(unit);

        unitSelectEvent.Raise();
    }

    public void ShiftClickSelectUnit(Unit unit)
    {
        if(gameUnitData.SelectedUnitList.Contains(unit))
        {
            DeSelectUnit(unit);
        }
        else
        {
            SelectUnit(unit);

            DebugTool.Log("shift");
        }
    }




    public void SelectUnit(Unit unit)
    {
        gameUnitData.SelectUnit(unit);
    }

    public void DeSelectUnitAll()
    {
        gameUnitData.DeSelectUnitAll();
    }

    public void DeSelectUnit(Unit unit)
    {
        gameUnitData.DeSelectUnit(unit);
    }


    public List<Unit> ReturnUnitList()
    {
        return gameUnitData.UnitList;
    }

    #endregion



    private void UnitRightClick(Vector3 pos)
    {

        ////적 색적 및 공격
        //Collider2D[] collider2DArray = Physics2D.OverlapPointAll(UtilsClass.GetMouseWorldPosition());
        //foreach (Collider2D collider2D in collider2DArray)
        //{
        //    EnemyRTS enemyRTS = collider2D.GetComponent<EnemyRTS>();
        //    if (enemyRTS != null)
        //    {
        //        // Right Click on Enemy, Attack!
        //        foreach (UnitRTS unitRTS in selectedUnitRTSList)
        //        {
        //            unitRTS.SetTarget(enemyRTS);
        //        }
        //        return;
        //    }
        //}

        // Move To Position
        //Vector3 moveToPosition = UtilsClass.GetMouseWorldPosition();

        List<Vector3> targetPositionList = GetPositionListAround(pos, new float[] { 10f, 20f, 30f }, new int[] { 5, 10, 20 });

        int targetPositionListIndex = 0;

        foreach (Unit unit in gameUnitData.SelectedUnitList)
        {
            //unit.ClearTarget();
            unit.MoveTo(targetPositionList[targetPositionListIndex]);
            targetPositionListIndex = (targetPositionListIndex + 1) % targetPositionList.Count;
        }
    }



    private List<Vector3> GetPositionListAround(Vector3 startPosition, float[] ringDistanceArray, int[] ringPositionCountArray)
    {
        List<Vector3> positionList = new List<Vector3>();
        positionList.Add(startPosition);
        for (int i = 0; i < ringDistanceArray.Length; i++)
        {
            positionList.AddRange(GetPositionListAround(startPosition, ringDistanceArray[i], ringPositionCountArray[i]));
        }
        return positionList;
    }

    private List<Vector3> GetPositionListAround(Vector3 startPosition, float distance, int positionCount)
    {
        List<Vector3> positionList = new List<Vector3>();
        for (int i = 0; i < positionCount; i++)
        {
            float angle = i * (360f / positionCount);
            Vector3 dir = ApplyRotationToVector(new Vector3(1, 0), angle);
            Vector3 position = startPosition + dir * distance;
            positionList.Add(position);
        }
        return positionList;
    }

    private Vector3 ApplyRotationToVector(Vector3 vec, float angle)
    {
        return Quaternion.Euler(0, 0, angle) * vec;
    }

}
