using System.Collections.Generic;

public class GridObject
{
    private GridSystem gridSystem;
    private GridPosition gridPosition;

    private List<Unit> unitList = new();

    public GridObject(GridSystem gridSystem, GridPosition gridPosition)
    {
        this.gridSystem   = gridSystem;
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
}
