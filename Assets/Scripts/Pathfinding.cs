using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    [SerializeField] private Transform gridDebugObjectPrefab;

    private int width;
    private int height;
    private float cellSize;

    private GridSystem<PathNode> gridSystem;

    private void Awake()
    {
        gridSystem = new GridSystem<PathNode>(10, 10, 2f,
            (GridSystem<PathNode> g, GridPosition pos) => new PathNode(pos));
        gridSystem.CreateDebugObjects(gridDebugObjectPrefab);
    }

    public List<GridPosition> FindPath(GridPosition startGridPosition, GridPosition endGridPosition)
    {
        List<PathNode> openList = new List<PathNode>();
        List<PathNode> closedList = new List<PathNode>();

        PathNode startNode = gridSystem.GetGridObject(startGridPosition);
        PathNode endNode = gridSystem.GetGridObject(endGridPosition);
        openList.Add(startNode);

        for (int x = 0; x < gridSystem.GetWidth(); x++)
        {
            for (int z = 0; z < gridSystem.GetHeight(); z++)
            {
                var gridPosition = new GridPosition(x, z);
                PathNode pathNode = gridSystem.GetGridObject(gridPosition);

                pathNode.SetGCost(int.MaxValue);
                pathNode.SetHCost(0);
                pathNode.CalculateFCost();
                pathNode.ResetCameFromPathNode();
            }
        }

        startNode.SetGCost(0);
        startNode.SetHCost(CalculateDistance(startGridPosition, endGridPosition));
        startNode.CalculateFCost();

        while (openList.Any())
        {
            PathNode currentNode = GetLowestFCostPathNode(openList);

            // reached final node
            if (currentNode == endNode)
            {
                return CalculatePath(endNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (PathNode neighbourNode in GetNeighbourList(currentNode))
            {
                if (closedList.Contains(neighbourNode))
                {
                    continue;
                }

                int tentativeGCost = currentNode.GetGCost()
                    + CalculateDistance(currentNode.GetGridPosition(), neighbourNode.GetGridPosition());

                if (tentativeGCost < neighbourNode.GetGCost())
                {
                    neighbourNode.SetCameFromPathNode(currentNode);
                    neighbourNode.SetGCost(tentativeGCost);
                    neighbourNode.SetHCost(CalculateDistance(neighbourNode.GetGridPosition(), endGridPosition));
                    neighbourNode.CalculateFCost();

                    if (openList.Contains(neighbourNode) is false)
                    {
                        openList.Add(neighbourNode);
                    }
                }
            }
        }

        // no path found
        return null;
    }

    public int CalculateDistance(GridPosition a, GridPosition b)
    {
        var gridPositionDistance = a - b;
        int distance = Mathf.Abs(gridPositionDistance.x) + Mathf.Abs(gridPositionDistance.z);

        // if using diagonal
        // int xDistance = Mathf.Abs(gridPositionDistance.x);
        // int zDistance = Mathf.Abs(gridPositionDistance.z);
        // int diagonalCost = MOVE_DIAGONAL_COST * Mathf.Min(xDistance, zDistance);
        // int remaining = Mathf.Abs(xDistance - zDistance);
        // return diagonalCost + MOVE_STRAIGHT_COST * remaining;

        return distance * MOVE_STRAIGHT_COST;
    }

    private PathNode GetLowestFCostPathNode(List<PathNode> pathNodeList)
    {
        PathNode lowestFCostPathNode = pathNodeList[0];

        for (int i = 0; i < pathNodeList.Count; i++)
        {
            if (pathNodeList[i].GetFCost() < lowestFCostPathNode.GetFCost())
            {
                lowestFCostPathNode = pathNodeList[i];
            }
        }

        return lowestFCostPathNode;
    }

    private PathNode GetNode(int x, int z)
    {
        return gridSystem.GetGridObject(new GridPosition(x, z));
    }

    private List<PathNode> GetNeighbourList(PathNode currentNode)
    {
        List<PathNode> neighbourList = new();

        GridPosition gridPosition = currentNode.GetGridPosition();

        bool canGoLeft = gridPosition.x - 1 >= 0;
        bool canGoRight = gridPosition.x + 1 < gridSystem.GetWidth();
        bool canGoUp = gridPosition.z + 1 < gridSystem.GetHeight();
        bool canGoDown = gridPosition.z - 1 >= 0;

        if (canGoLeft)
        {
            // left
            neighbourList.Add(GetNode(gridPosition.x - 1, gridPosition.z + 0));

            // if (canGoUp)
            // {
            //     // left up
            //     neighbourList.Add(GetNode(gridPosition.x - 1, gridPosition.z + 1));
            // }

            // if (canGoDown)
            // {
            //     // left down
            //     neighbourList.Add(GetNode(gridPosition.x - 1, gridPosition.z - 1));
            // }
        }

        if (canGoRight)
        {
            // right
            neighbourList.Add(GetNode(gridPosition.x + 1, gridPosition.z + 0));

            // if (canGoUp)
            // {
            //     // right up
            //     neighbourList.Add(GetNode(gridPosition.x + 1, gridPosition.z + 1));
            // }

            // if (canGoDown)
            // {
            //     // right down
            //     neighbourList.Add(GetNode(gridPosition.x + 1, gridPosition.z - 1));
            // }
        }

        if (canGoUp)
        {
            // up
            neighbourList.Add(GetNode(gridPosition.x + 0, gridPosition.z + 1));
        }

        if (canGoDown)
        {
            // down
            neighbourList.Add(GetNode(gridPosition.x + 0, gridPosition.z - 1));
        }

        return neighbourList;
    }

    private List<GridPosition> CalculatePath(PathNode endNode)
    {
        List<PathNode> pathNodeList = new();
        pathNodeList.Add(endNode);

        PathNode currentNode = endNode;
        while (currentNode.GetCameFromPathNode() is not null)
        {
            pathNodeList.Add(currentNode.GetCameFromPathNode());
            currentNode = currentNode.GetCameFromPathNode();
        }

        pathNodeList.Reverse();

        List<GridPosition> gridPositionList = new();
        foreach (var node in pathNodeList)
        {
            gridPositionList.Add(node.GetGridPosition());
        }

        return gridPositionList;
    }
}
