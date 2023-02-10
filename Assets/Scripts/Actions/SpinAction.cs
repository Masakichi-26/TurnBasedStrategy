using System;
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

    public void Spin(Action onComplete)
    {
        onActionComplete = onComplete;
        totalSpinAmount = 0;
        isActive = true;
    }
}