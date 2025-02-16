using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
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
    [SerializeField] float unitSpacing = 2f; // 유닛 간격

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

    public void PathFindingToMove(Vector3 targetPos) //이걸 선택한 유닛수만큼 따로 따로 돌려야함
    {
        //이동불가 지역인가?  아 시발 이것도 해야함 ㅋ

        List<List<Vector3>> unitPaths = new List<List<Vector3>>();

        unitPaths = AssignPathsToUnits(targetPos);
        UnitMoveTo(unitPaths);
    }
    public void PathFindingToAttack(IDamageAble target, Vector3 targetPos) //이걸 선택한 유닛수만큼 따로 따로 돌려야함
    {
        //이동불가 지역인가?  아 시발 이것도 해야함 ㅋ


        pathFinding.GetGrid().GetXY3D(targetPos, out int x, out int y); //target XY

        int count = 0;

        for (int i = 0; i < gameUnitData.SelectedUnitList.Count; i++)
        {
            count += i;

            if (Vector3.Distance(transform.position, targetPos) < 
                gameUnitData.SelectedUnitList[i].UnitInfo.unitRange)
            {

            }
            else
            {
                pathFinding.GetGrid().GetXY3D(gameUnitData.SelectedUnitList[i].transform.position, out int a, out int b);
                List<PathNode> path = pathFinding.FindPath(a, b, x, y);

                AttackMove(target, path, i);
            }
        }
    }

    public void UnitMoveTo(List<List<Vector3>> unitPaths)
    {
        for (int i = 0; i < gameUnitData.SelectedUnitList.Count; i++)
        {
            if (gameUnitData.SelectedUnitList[i] == null) Debug.LogError($"UnitData index {i} Null");
            if (unitPaths[i] == null) Debug.LogError($"UnitPaths index {i} Null");

            gameUnitData.SelectedUnitList[i].MoveTo(unitPaths[i]);
        }
    }




    List<List<Vector3>> AssignPathsToUnits(Vector3 target)
    {
        List<List<Vector3>> unitPaths = new List<List<Vector3>>();
        unitPaths.Clear();

        // 유닛을 격자 형태로 배치 (네모 형태의 포메이션)
        int rowCount = Mathf.CeilToInt(Mathf.Sqrt(gameUnitData.SelectedUnitList.Count));

        for (int i = 0; i < gameUnitData.SelectedUnitList.Count; i++)
        {
            int row = i / rowCount;
            int col = i % rowCount;

            Vector3 offset = new Vector3(col * unitSpacing, 0, row * unitSpacing);
            Vector3 potentialTargetPosition = target + offset;

            // 🚧 이동 불가능한 위치라면, 가장 가까운 이동 가능 위치 찾기
            PathNode targetNode = pathFinding.GetGrid().GetGridObject3D(potentialTargetPosition);
            if (targetNode == null || !targetNode.isWalkable)   
            {
                Debug.Log("targetNode isWalkable");
                potentialTargetPosition = FindClosestWalkablePosition(potentialTargetPosition);
            }


            // A* 경로 탐색 실행
            Vector3 unitPosition = gameUnitData.SelectedUnitList[i].transform.position;
            PathNode unitNode = pathFinding.GetGrid().GetGridObject3D(unitPosition);
            List<PathNode> pathNodes = pathFinding.FindPath(unitNode.x, unitNode.y, targetNode.x, targetNode.y);

            // PathNode 리스트를 Vector3 리스트로 변환
            List<Vector3> path = new List<Vector3>();
            if (pathNodes != null)
            {
                foreach (var node in pathNodes)
                {
                    path.Add(new Vector3(node.worldPosition.x, 0, node.worldPosition.z));
                }
            }

            unitPaths.Add(path);
        }

        return unitPaths;
    }


    Vector3 FindClosestWalkablePosition(Vector3 position)
    {
        PathNode node = pathFinding.GetGrid().GetGridObject2D(position);
        if (node != null && node.isWalkable)
        {
            return position;
        }

        foreach (PathNode neighbor in pathFinding.GetNeighbourList(node))
        {
            if (neighbor.isWalkable)
            {
                return new Vector3(neighbor.worldPosition.x, 0, neighbor.worldPosition.z);
            }
        }

        return position; // 만약 이동 가능한 위치가 없으면 원래 위치 반환
    }


    #endregion

    #region Attack

    public void UnitAttack(IDamageAble target, int index)
    {
        if (gameUnitData.SelectedUnitList[index] != null)
            gameUnitData.SelectedUnitList[index].Attack(target);
    }

    public void AttackMove(IDamageAble target, List<PathNode> path, int index)
    {
        if (gameUnitData.SelectedUnitList[index] != null)
            gameUnitData.SelectedUnitList[index].AttackMove(target, path);
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
        if (gameUnitData.SelectedUnitList.Contains(unit))
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
