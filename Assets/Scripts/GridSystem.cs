using UnityEngine;

public class GridSystem
{
    private int width;
    private int height;
    private float cellSize;

    public GridSystem(int width, int height, float cellSize)
    {
        this.width    = width;
        this.height   = height;
        this.cellSize = cellSize;

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                var pos = GetWorldPosition(x, z);
                Debug.DrawLine(pos, pos + Vector3.right * 0.2f, Color.white, 1000);
            }
        }
        
    }

    public Vector3 GetWorldPosition(int x, int z)
    {
        return new Vector3(x, 0, z) * cellSize;
    }

    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return new GridPosition(
            Mathf.RoundToInt(worldPosition.x / cellSize),
            Mathf.RoundToInt(worldPosition.z / cellSize)
        );
    }
}
