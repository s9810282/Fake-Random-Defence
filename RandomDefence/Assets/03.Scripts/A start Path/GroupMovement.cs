using System.Collections.Generic;
using UnityEngine;

public class GroupMovement : MonoBehaviour
{
    public PathFinding pathFinding; // A* 경로 탐색 시스템
    public Transform target; // 목표 지점 (중앙)
    public float unitSpacing = 2f; // 유닛 간격
    public float moveSpeed = 5f; // 유닛 이동 속도

    private List<GameObject> units; // 그룹 내 유닛 목록
    private List<List<Vector3>> unitPaths; // 각 유닛의 A* 경로 리스트

    void Start()
    {
        units = new List<GameObject>();
        unitPaths = new List<List<Vector3>>();

        // 현재 객체의 자식 오브젝트를 유닛으로 추가
        foreach (Transform child in transform)
        {
            units.Add(child.gameObject);
        }

        AssignPathsToUnits(); // 유닛 경로 설정
    }

    void Update()
    {
        MoveUnits(); // 유닛 이동 실행
    }

    // 🎯 유닛별 목표 위치 설정 및 경로 탐색 (3D 환경 반영)
    void AssignPathsToUnits()
    {
        unitPaths.Clear();

        // 유닛을 격자 형태로 배치 (네모 형태의 포메이션)
        int rowCount = Mathf.CeilToInt(Mathf.Sqrt(units.Count));

        for (int i = 0; i < units.Count; i++)
        {
            int row = i / rowCount;
            int col = i % rowCount;

            Vector3 offset = new Vector3(col * unitSpacing, 0, row * unitSpacing);
            Vector3 potentialTargetPosition = target.position + offset;

            // 🚧 이동 불가능한 위치라면, 가장 가까운 이동 가능 위치 찾기
            PathNode targetNode = pathFinding.GetGrid().GetGridObject2D(potentialTargetPosition);
            if (targetNode == null || !targetNode.isWalkable)
            {
                potentialTargetPosition = FindClosestWalkablePosition(potentialTargetPosition);
            }

            // A* 경로 탐색 실행 (3D 환경에서는 Z축을 고려)
            Vector3 unitPosition = units[i].transform.position;
            PathNode unitNode = pathFinding.GetGrid().GetGridObject2D(unitPosition);
            List<PathNode> pathNodes = pathFinding.FindPath(unitNode.x, unitNode.y, targetNode.x, targetNode.y);

            // PathNode 리스트를 Vector3 리스트로 변환 (y 대신 z를 사용)
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
    }

    // 🚶 유닛을 A* 경로를 따라 이동
    void MoveUnits()
    {
        for (int i = 0; i < units.Count; i++)
        {
            if (unitPaths[i] != null && unitPaths[i].Count > 0)
            {
                Vector3 nextPosition = unitPaths[i][0];
                units[i].transform.position = Vector3.MoveTowards(units[i].transform.position, nextPosition, moveSpeed * Time.deltaTime);

                // 현재 경로 지점에 도착하면 제거
                if (Vector3.Distance(units[i].transform.position, nextPosition) < 0.1f)
                {
                    unitPaths[i].RemoveAt(0);
                }
            }
        }
    }

    // 🛠️ 이동 가능한 가장 가까운 위치 찾기 (Z축 반영)
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
}
