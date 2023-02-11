using System;
using UnityEngine;

public abstract class BaseAction : MonoBehaviour
{
    [SerializeField] protected Unit unit;

    protected bool isActive;
    protected Action onActionComplete;

    public abstract string GetActionName();
}