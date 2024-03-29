using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer;

public class UnitManager : MonoBehaviour
{
    private List<Unit> unitList = new List<Unit>();
    private List<Unit> friendlyUnitList = new List<Unit>();
    private List<Unit> enemyUnitList = new List<Unit>();

    private UnitActionSystem unitActionSystem;

    [Inject]
    private void Construct(UnitActionSystem unitActionSystem)
    {
        this.unitActionSystem = unitActionSystem;
    }

    private void Start()
    {
        Unit.OnAnyUnitSpawned += Unit_OnAnyUnitSpawned;
        Unit.OnAnyUnitDead += Unit_OnAnyUnitDead;
    }

    private void Unit_OnAnyUnitSpawned(object sender, EventArgs e)
    {
        Unit unit = sender as Unit;

        unitList.Add(unit);

        if (unit.IsEnemy())
        {
            enemyUnitList.Add(unit);
        }
        else
        {
            friendlyUnitList.Add(unit);
        }
    }

    private void Unit_OnAnyUnitDead(object sender, EventArgs e)
    {
        Unit unit = sender as Unit;

        unitList.Remove(unit);

        if (unit.IsEnemy())
        {
            enemyUnitList.Remove(unit);
        }
        else
        {
            friendlyUnitList.Remove(unit);
            if (unitActionSystem.GetSelectedUnit() == unit && friendlyUnitList.Any())
            {
                unitActionSystem.SetSelectedUnit(friendlyUnitList[0]);
            }
        }
    }

    public List<Unit> GetUnitList()
    {
        return unitList;
    }

    public List<Unit> GetFriendlyUnitList()
    {
        return friendlyUnitList;
    }

    public List<Unit> GetEnemyUnitList()
    {
        return enemyUnitList;
    }
}
