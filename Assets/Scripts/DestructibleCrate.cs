using System;
using UnityEngine;
using VContainer;

public class DestructibleCrate : MonoBehaviour
{
    public static event EventHandler OnAnyDestroyed;

    private GridPosition gridPosition;
    private LevelGrid levelGrid;

    [Inject]
    private void Construct(LevelGrid levelGrid)
    {
        this.levelGrid = levelGrid;
    }

    private void Start()
    {
        gridPosition = levelGrid.GetGridPosition(transform.position);
    }

    public void Damage()
    {
        Destroy(gameObject);
        OnAnyDestroyed?.Invoke(this, EventArgs.Empty);
    }

    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }
}
