using System;
using UnityEngine;
using VContainer;

public class PathfindingUpdater : MonoBehaviour
{
    private Pathfinding pathfinding;

    [Inject]
    private void Construct(Pathfinding pathfinding)
    {
        this.pathfinding = pathfinding;
    }

    private void Start()
    {
        DestructibleCrate.OnAnyDestroyed += DestructibleCrate_OnAnyDestroyed;
    }

    private void DestructibleCrate_OnAnyDestroyed(object sender, EventArgs e)
    {
        DestructibleCrate crate = sender as DestructibleCrate;
        pathfinding.SetIsPassableGridPosition(crate.GetGridPosition(), true);
    }
}
