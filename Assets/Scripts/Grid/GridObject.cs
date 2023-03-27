using System.Linq;
using System.Collections.Generic;

public class GridObject
{
    private GridSystem<GridObject> gridSystem;
    private GridPosition gridPosition;

    private List<Unit> unitList = new();
    private Door door;

    public GridObject(GridSystem<GridObject> gridSystem, GridPosition gridPosition)
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
    }

    public override string ToString()
    {
        string unitString = "";
        foreach (Unit unit in unitList)
        {
            unitString += unit + "\n";
        }
        return gridPosition.ToString() + "\n" + unitString;
    }

    public List<Unit> GetUnitList()
    {
        return unitList;
    }

    public void AddUnit(Unit unit)
    {
        unitList.Add(unit);
    }

    public void RemoveUnit(Unit unit)
    {
        unitList.Remove(unit);
    }

    public bool HasAnyUnit()
    {
        return unitList.Any();
    }

    public Unit GetUnit()
    {
        if (HasAnyUnit())
        {
            return unitList.First();
        }

        return null;
    }

    public Door GetDoor()
    {
        return door;
    }

    public void SetDoor(Door door)
    {
        this.door = door;
    }
}
