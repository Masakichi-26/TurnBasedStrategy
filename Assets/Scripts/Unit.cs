using UnityEngine;
using VContainer;

public class Unit : MonoBehaviour
{
    [SerializeField] private MoveAction moveAction;

    private LevelGrid levelGrid;

    private GridPosition gridPosition;

    [Inject]
    private void Construct(LevelGrid levelGrid)
    {
        this.levelGrid = levelGrid;
    }

    private void Start()
    {
        gridPosition = levelGrid.GetGridPosition(transform.position);
        levelGrid.AddUnitAtGridPosition(gridPosition, this);
    }

    private void Update()
    {
        GridPosition newGridPosition = levelGrid.GetGridPosition(transform.position);
        if (newGridPosition != gridPosition)
        {
            levelGrid.UnitMoveGridPosition(this, gridPosition, newGridPosition);
            gridPosition = newGridPosition;
        }
    }

    public MoveAction GetMoveAction()
    {
        return moveAction;
    }

    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }
}
