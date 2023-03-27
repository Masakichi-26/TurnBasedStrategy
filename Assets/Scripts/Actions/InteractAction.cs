using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class InteractAction : BaseAction
{
    private int maxInteractDistance = 1;

    private LevelGrid levelGrid;

    [Inject]
    private void Construct(LevelGrid levelGrid)
    {
        this.levelGrid = levelGrid;
    }

    private void Update()
    {
        if (isActive is false)
        {
            return;
        }
    }

    public override string GetActionName()
    {
        return "Interact";
    }

    public override EnemyAIAction GetBestEnemyAIAction(GridPosition gridPosition)
    {
        return new EnemyAIAction(gridPosition, 0);
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new();
        GridPosition unitGridPosition = unit.GetGridPosition();

        for (int x = -maxInteractDistance; x <= maxInteractDistance; x++)
        {
            for (int z = -maxInteractDistance; z <= maxInteractDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (levelGrid.IsValidGridPosition(testGridPosition) is false)
                {
                    continue;
                }

                if (levelGrid.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    // unit at door position, so don't allow interaction
                    continue;
                }

                Door door = levelGrid.GetDoorAtGridPosition(testGridPosition);
                if (door == null)
                {
                    // no door on this grid position
                    continue;
                }

                validGridPositionList.Add(testGridPosition);
            }
        }

        return validGridPositionList;
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        Door door = levelGrid.GetDoorAtGridPosition(gridPosition);
        door.Interact(OnInteractComplete);
        ActionStart(onActionComplete);
    }

    private void OnInteractComplete()
    {
        ActionComplete();
    }
}
