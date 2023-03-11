public class PathNode
{
    private GridPosition gridPosition;

    // walking cost from start node
    private int gCost;
    // heuristic cost to reach end node
    private int hCost;
    // g + h
    private int fCost;

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
}
