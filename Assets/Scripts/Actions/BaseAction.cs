using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class BaseAction : MonoBehaviour
{
    public static event EventHandler OnAnyActionStarted;
    public static event EventHandler OnAnyActionCompleted;

    [SerializeField] protected Unit unit;

    protected bool isActive;
    protected Action onActionComplete;

    public abstract string GetActionName();

    public abstract void TakeAction(GridPosition gridPosition, Action onActionComplete);

    public virtual bool IsValidActionGridPosition(GridPosition gridPosition)
    {
        List<GridPosition> validGridPositionList = GetValidActionGridPositionList();
        return validGridPositionList.Contains(gridPosition);
    }

    public abstract List<GridPosition> GetValidActionGridPositionList();

    public virtual int GetActionPointsCost()
    {
        return 1;
    }

    protected void ActionStart(Action onActionComplete)
    {
        isActive = true;
        this.onActionComplete = onActionComplete;

        OnAnyActionStarted?.Invoke(this, EventArgs.Empty);
    }

    protected void ActionComplete()
    {
        isActive = false;
        onActionComplete();

        OnAnyActionCompleted?.Invoke(this, EventArgs.Empty);
    }

    public Unit GetUnit()
    {
        return unit;
    }

    public EnemyAIAction GetBestEnemyAIAction()
    {
        var enemyAIActionList = new List<EnemyAIAction>();

        var validActionGridPositionList = GetValidActionGridPositionList();

        foreach (var gridPosition in validActionGridPositionList)
        {
            var enemyAIAction = GetBestEnemyAIAction(gridPosition);
            enemyAIActionList.Add(enemyAIAction);
        }

        // no possible enemy AI actions
        if (enemyAIActionList.Any() is false)
        {
            return null;
        }

        enemyAIActionList.Sort((a, b) => b.actionValue - a.actionValue);
        return enemyAIActionList[0];
    }

    public abstract EnemyAIAction GetBestEnemyAIAction(GridPosition gridPosition);
}