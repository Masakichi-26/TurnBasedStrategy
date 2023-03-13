public class PathNode
{
    private GridPosition gridPosition;

    // walking cost from start node
    private int gCost;
    // heuristic cost to reach end node
    private int hCost;
    // g + h
    private int fCost;

    private bool isPassable = true;

    private PathNode cameFromPathNode;

    public PathNode(GridPosition pos)
    {
        gridPosition = pos;
    }

    public override string ToString()
    {
        return gridPosition.ToString();
    }

    public int GetGCost()
    {
        return gCost;
    }

    public int GetHCost()
    {
        return hCost;
    }

    public int GetFCost()
    {
        return fCost;
    }

    public void SetGCost(int cost)
    {
        gCost = cost;
    }

    public void SetHCost(int cost)
    {
        hCost = cost;
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

    public PathNode GetCameFromPathNode()
    {
        return cameFromPathNode;
    }

    public void SetCameFromPathNode(PathNode node)
    {
        cameFromPathNode = node;
    }

    public void ResetCameFromPathNode()
    {
        cameFromPathNode = null;
    }

    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }

    public bool IsPassable()
    {
        return isPassable;
    }

    public void SetIsPassable(bool isPassable)
    {
        this.isPassable = isPassable;
    }
}
