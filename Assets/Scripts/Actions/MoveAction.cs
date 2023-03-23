using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class MoveAction : BaseAction
{
    public event EventHandler OnStartMoving;
    public event EventHandler OnStopMoving;

    [SerializeField] private int maxMoveDistance = 5;

    private List<Vector3> positionList;
    private int currentPositionIndex;
    private LevelGrid levelGrid;
    private Pathfinding pathfinding;

    [Inject]
    private void Construct(LevelGrid levelGrid, Pathfinding pathfinding)
    {
        this.levelGrid = levelGrid;
        this.pathfinding = pathfinding;
    }

    private void Update()
    {
        if (isActive is false)
        {
            return;
        }

        Vector3 targetPosition = positionList[currentPositionIndex];
        Vector3 direction = (targetPosition - transform.position).normalized;

        float rotateSpeed = 10f;
        transform.forward = Vector3.Lerp(transform.forward, direction, Time.deltaTime * rotateSpeed);

        float stoppingDistance = 0.01f;
        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
        {
            float moveSpeed = 8f;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
        else
        {
            currentPositionIndex++;
            if (currentPositionIndex >= positionList.Count)
            {
                OnStopMoving?.Invoke(this, EventArgs.Empty);
                ActionComplete();
            }
        }
    }

    public override void TakeAction(GridPosition gridPosition, Action onComplete)
    {
        List<GridPosition> pathGridPositionList = pathfinding.FindPath(unit.GetGridPosition(), gridPosition, out int pathLength);
        
        currentPositionIndex = 0;
        positionList = new List<Vector3>();

        foreach (GridPosition pathGridPosition in pathGridPositionList)
        {
            positionList.Add(levelGrid.GetWorldPosition(pathGridPosition));
        }

        OnStartMoving?.Invoke(this, EventArgs.Empty);

        ActionStart(onComplete);
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new();

        GridPosition unitGridPosition = unit.GetGridPosition();

        for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
        {
            for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (levelGrid.IsValidGridPosition(testGridPosition) == false)
                {
                    continue;
                }

                int testDistance = Mathf.Abs(x) + Mathf.Abs(z);
                if (testDistance > maxMoveDistance)
                {
                    continue;
                }

                if (unitGridPosition == testGridPosition)
                {
                    continue;
                }

                if (levelGrid.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    continue;
                }

                if (pathfinding.IsPassableGridPosition(testGridPosition) is false)
                {
                    continue;
                }

                if (pathfinding.HasPath(unitGridPosition, testGridPosition) is false)
                {
                    continue;
                }

                int pathFindingDistanceMulitplier = 10;
                if (pathfinding.GetPathLength(unitGridPosition, testGridPosition) > maxMoveDistance * pathFindingDistanceMulitplier)
                {
                    // path length is too long
                    continue;
                }

                validGridPositionList.Add(testGridPosition);
            }
        }

        return validGridPositionList;
    }

    public override string GetActionName()
    {
        return "Move";
    }

    public override EnemyAIAction GetBestEnemyAIAction(GridPosition gridPosition)
    {
        int targetCountAtGridPosition = unit.GetAction<ShootAction>().GetTargetCountAtPosition(gridPosition);
        return new EnemyAIAction(gridPosition, targetCountAtGridPosition * 10);
    }
}
