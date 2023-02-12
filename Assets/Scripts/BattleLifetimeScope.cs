using VContainer;
using VContainer.Unity;
using UnityEngine;

public class BattleLifetimeScope : LifetimeScope
{
    [SerializeField] private MouseWorld mouseWorld;
    [SerializeField] private UnitActionSystem unitActionSystem;
    [SerializeField] private LevelGrid levelGrid;
    [SerializeField] private GridSystemVisual gridSystemVisual;
    [SerializeField] private TurnSystem turnSystem;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(mouseWorld);
        builder.RegisterComponent(unitActionSystem);
        builder.RegisterComponent(levelGrid);
        builder.RegisterComponent(gridSystemVisual);
        builder.RegisterComponent(turnSystem);
    }
}
