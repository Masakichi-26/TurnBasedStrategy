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
    [SerializeField] private UnitManager unitManager;
    [SerializeField] private Pathfinding pathfinding;
    [SerializeField] private ScreenShake screenShake;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(mouseWorld);
        builder.RegisterComponent(unitActionSystem);
        builder.RegisterComponent(levelGrid);
        builder.RegisterComponent(gridSystemVisual);
        builder.RegisterComponent(turnSystem);
        builder.RegisterComponent(unitManager);
        builder.RegisterComponent(pathfinding);
        builder.RegisterComponent(screenShake);
    }
}
