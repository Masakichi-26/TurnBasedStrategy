using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class LevelGrid : MonoBehaviour
{
    public event EventHandler OnAnyUnitMovedGridPosition;

    [SerializeField] private Transform gridDebugObjectPrefab;
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float cellSize;

    // private Pathfinding pathfinding;
    private GridSystem<GridObject> gridSystem;

    public int Width => width;
    public int Height => height;
    public float CellSize => cellSize;

    // [Inject]
    // private void Construct(Pathfinding pathfinding)
    // {
    //     this.pathfinding = pathfinding;
    // }

    private void Awake()
    {
        gridSystem = new GridSystem<GridObject>(width, height, cellSize,
            (GridSystem<GridObject> g, GridPosition pos) => new GridObject(g, pos));
        // gridSystem.CreateDebugObjects(gridDebugObjectPrefab);
    }

    private void Start()
    {
        // pathfinding.Setup(width, height, cellSize);
    }

    public void AddUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
        gridSystem.GetGridObject(gridPosition).AddUnit(unit);
    }

    public List<Unit> GetUnitListAtGridPosition(GridPosition gridPosition)
    {
        return gridSystem.GetGridObject(gridPosition).GetUnitList();
    }

    public void RemoveUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
        gridSystem.GetGridObject(gridPosition).RemoveUnit(unit);
    }

    public void UnitMoveGridPosition(Unit unit, GridPosition from, GridPosition to)
    {
        RemoveUnitAtGridPosition(from, unit);
        AddUnitAtGridPosition(to, unit);
        OnAnyUnitMovedGridPosition?.Invoke(this, EventArgs.Empty);
    }

    public GridPosition GetGridPosition(Vector3 worldPosition) => gridSystem.GetGridPosition(worldPosition);

    public Vector3 GetWorldPosition(GridPosition gridPosition) => gridSystem.GetWorldPosition(gridPosition);

    public bool IsValidGridPosition(GridPosition gridPosition) => gridSystem.IsValidGridPosition(gridPosition);

    public int GetWidth() => gridSystem.GetWidth();

    public int GetHeight() => gridSystem.GetHeight();

    public bool HasAnyUnitOnGridPosition(GridPosition gridPosition)
    {
        return gridSystem.GetGridObject(gridPosition).HasAnyUnit();
    }

    public Unit GetUnitAtGridPosition(GridPosition gridPosition)
    {
        return gridSystem.GetGridObject(gridPosition).GetUnit();
    }
}
