using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding
{
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    private Grid<PathNode> grid;

    private List<PathNode> openList;
    private List<PathNode> closedList;

    public PathFinding(int width, int heigh)
    {
        grid = new Grid<PathNode>(width, heigh, 10f, Vector3.zero, (Grid<PathNode> grid, int x, int y) => new PathNode(grid, x, y));
    }

    private List<PathNode> FindPath(int startX, int startY, int endX, int endY)
    {
        PathNode startNode = grid.GetGridObject(startX, startY);
        PathNode endNode = grid.GetGridObject(endX, endY);

        openList = new List<PathNode> { startNode };
        closedList = new List<PathNode>();

        for(int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; x < grid.GetHeight(); y++)
            {
                PathNode pathNode = grid.GetGridObject(x, y);
                pathNode.gCost = int.MaxValue;
                pathNode.CalculateFCost();
                pathNode.cameFromNode = null;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = CaculateDistance(startNode, endNode);
        startNode.CalculateFCost();

        while(openList.Count > 0)
        {
            PathNode currentNode = GetLowerstFCostNode(openList);

            if(currentNode == endNode)
            {
                return CalculatePath(endNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);


        }

        return null;
    }

    public List<PathNode> GetNeighbourList(PathNode currentNode)
    {
        List<PathNode> neighbourList = new List<PathNode>();

        if (currentNode.x - 1 >= 0)
        {
            neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y));

            if(currentNode.y - 1 >= 0)
            {
                neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y - 1));
            }
        }
    }

    private PathNode GetNode(int x, int y)
    {
        return grid.GetGridObject(x, y);
    }


    private List<PathNode> CalculatePath(PathNode endNode)
    {
        throw new NotImplementedException();
    }

    public int CaculateDistance(PathNode a, PathNode b)
    {
        int xDistnace = Mathf.Abs(a.x - b.x);
        int yDistnace = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistnace - yDistnace);

        return MOVE_DIAGONAL_COST * Mathf.Min(xDistnace, yDistnace) + MOVE_STRAIGHT_COST * remaining;
    }

    public PathNode GetLowerstFCostNode(List<PathNode> pathNodeList)
    {
        PathNode lowestNode = pathNodeList[0];

        for(int i = 0; i < pathNodeList.Count; i++)
        {
            if(pathNodeList[i].fCost < lowestNode.fCost)
            {
                lowestNode = pathNodeList[i];
            }
        }

        return lowestNode;
    }
}
