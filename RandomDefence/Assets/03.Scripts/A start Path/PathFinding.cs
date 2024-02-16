using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PathFinding
{
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    public Grid<PathNode> grid;

    private List<PathNode> openList;
    private List<PathNode> closedList;

    public PathFinding(int width, int heigh, float cellSize, Vector3 originPos)
    {
        grid = new Grid<PathNode>(width, heigh, cellSize, originPos, (Grid<PathNode> grid, int x, int y) => new PathNode(grid, x, y));
    }

    public Grid<PathNode> GetGrid()
    {
        return grid;
    }

    public List<PathNode> FindPath(int startX, int startY, int endX, int endY)
    {
        PathNode startNode = grid.GetGridObject(startX, startY);
        PathNode endNode = grid.GetGridObject(endX, endY);

        Debug.Log(endNode.x);
        Debug.Log(endNode.y);

        openList = new List<PathNode> { startNode };
        closedList = new List<PathNode>();

        for(int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
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

            foreach (PathNode node in GetNeighbourList(currentNode))
            {
                if (closedList.Contains(node)) continue;
                if (!node.isWalkable)
                {
                    closedList.Add(node);
                    continue;
                }

                int tempGCost = currentNode.gCost + CaculateDistance(currentNode, node);

                if(tempGCost < node.gCost)
                {
                    node.cameFromNode = currentNode;
                    node.gCost = tempGCost;
                    node.hCost = CaculateDistance(node, endNode);
                    node.CalculateFCost();

                    if (!openList.Contains(node)) openList.Add(node);

                }
            }
        }

        return null;
    }

    public List<PathNode> GetNeighbourList(PathNode currentNode) //GetNeighbour All Node (8 Node)  
    {
        List<PathNode> neighbourList = new List<PathNode>();

        if (currentNode.x - 1 >= 0)  //current Node x - 1 => grid.MaxX 
        {
            //Left
            neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y));

            //Left Down
            if (currentNode.y - 1 >= 0) neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y - 1));

            //LeftUp
            if (currentNode.y + 1 < grid.GetHeight()) neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y + 1));

        }

        if (currentNode.x + 1 < grid.GetWidth()) //current Node x + 1 < grid.EndX
        {
            //Right
            neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y));

            //Right Down
            if (currentNode.y - 1 >= 0) neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y - 1));

            //Right Up
            if (currentNode.y + 1 < grid.GetHeight()) neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y + 1));
        }

        //Down
        if (currentNode.y - 1 >= 0) neighbourList.Add(GetNode(currentNode.x, currentNode.y - 1));

        //Up
        if (currentNode.y + 1 < grid.GetHeight()) neighbourList.Add(GetNode(currentNode.x, currentNode.y + 1));


        return neighbourList;
    }



    public PathNode GetNode(int x, int y)
    {
        return grid.GetGridObject(x, y);
    }


    private List<PathNode> CalculatePath(PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();
        path.Add(endNode);

        PathNode currentNode = endNode;

        while(currentNode.cameFromNode != null)
        {
            path.Add(currentNode);
            currentNode = currentNode.cameFromNode;
        }

        path.Reverse();
        return path;
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
