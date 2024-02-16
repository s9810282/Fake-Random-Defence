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

    [SerializeField] List<Unit> selectedUnitList = new List<Unit>();
    [SerializeField] List<Unit> unitList = new List<Unit>();


    void Start()
    {
        pathFinding = new PathFinding(maxX, maxY, cellSize, origin);
        selectedUnitList = new List<Unit>();

        //unitList = new List<Unit>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void PathFinding(Vector3 pos)
    {
        //Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

       
    }

    public void ShowPathGrid(Vector3 pos)
    {
        Debug.Log("Show");

        pathFinding.GetGrid().GetXY3D(pos, out int x, out int y);

        Debug.Log(x + " " + y);

        List<PathNode> path = pathFinding.FindPath(0, 0, x, y);

        if (path != null)
        {
            Debug.Log(path[0].x);
            Debug.Log(path[0].y);

            for (int i = 0; i < path.Count - 1; i++)
            {
                Debug.Log(path[i].x + ", " + path[i].y);
                Debug.DrawLine(new Vector3(path[i].x, 0, path[i].y) * cellSize + Vector3.one * 2.5f, new Vector3(path[i + 1].x, 0, path[i + 1].y) * cellSize + Vector3.one * 2.5f, Color.black, 10f);
            }
        }
    }


    public void UnitMoveTo(Vector3 vector)
    {
        Debug.Log(vector);

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

        foreach (Unit unit in selectedUnitList)
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
