using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathTEst : MonoBehaviour
{

    [SerializeField] PathFinding pathFinding;


    // Start is called before the first frame update
    void Start()
    {
        pathFinding = new PathFinding(10, 10, 10f, Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            pathFinding.GetGrid().GetXY2D(pos, out int x, out int y);

            List<PathNode> path = pathFinding.FindPath(0, 0, x, y);

            if (path != null)
            {
                DebugTool.Log(path[0].x);
                DebugTool.Log(path[0].y);

                for (int i = 0; i < path.Count - 1; i++)
                {
                    DebugTool.Log(path[i].x + ", " + path[i].y);
                    DebugTool.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, new Vector3(path[i + 1].x, path[i + 1].y) * 10f + Vector3.one * 5f, Color.black, 10f);
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            pathFinding.GetGrid().GetXY2D(pos, out int x, out int y);
            pathFinding.GetNode(x, y).SetIsWalkAble(!pathFinding.GetNode(x,y).isWalkable);
        } 
    }
}

