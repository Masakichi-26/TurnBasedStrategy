using UnityEngine;

public abstract class BaseAction : MonoBehaviour
{
    [SerializeField] protected Unit unit;

    protected bool isActive;
}