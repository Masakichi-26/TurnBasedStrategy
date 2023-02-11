using System;
using System.Collections.Generic;
using UnityEngine;

public class SpinAction : BaseAction
{
    private float totalSpinAmount;

    private void Update()
    {
        if (isActive is false)
        {
            return;
        }

        float spinAddAmount = 360f * Time.deltaTime;
        transform.eulerAngles += new Vector3(0, spinAddAmount, 0);
        totalSpinAmount += spinAddAmount;

        if (totalSpinAmount >= 360f)
        {
            isActive = false;
            onActionComplete.Invoke();
        }
    }

    public override void TakeAction(GridPosition gridPosition, Action onComplete)
    {
        onActionComplete = onComplete;
        totalSpinAmount = 0;
        isActive = true;
    }

    public override string GetActionName()
    {
        return "Spin";
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new();
        GridPosition unitGridPosition = unit.GetGridPosition();
        return new List<GridPosition>
        {
            unitGridPosition
        };
    }
}