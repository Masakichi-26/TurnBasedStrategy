using UnityEngine;
using VContainer;

public class Testing : MonoBehaviour
{
    [SerializeField] private Transform gridDebugObjectPrefab;

    [SerializeField] private Unit unit;

    private MouseWorld mouseWorld;

    private GridSystemVisual gridSystemVisual;

    private LevelGrid levelGrid;

    private Pathfinding pathfinding;

    [Inject]
    private void Construct(MouseWorld mouseWorld, GridSystemVisual gridSystemVisual,
        LevelGrid levelGrid, Pathfinding pathfinding)
    {
        this.mouseWorld = mouseWorld;
        this.gridSystemVisual = gridSystemVisual;
        this.levelGrid = levelGrid;
        this.pathfinding = pathfinding;

        Debug.Log("injecting into Testing");
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("T key pressed");

            GridPosition mouseGridPosition = levelGrid.GetGridPosition(mouseWorld.GetPosition());
            GridPosition startGridPosition = new GridPosition(0, 0);

            var gridPositionList = pathfinding.FindPath(startGridPosition, mouseGridPosition);

            for (int i = 0; i < gridPositionList.Count - 1; i++)
            {
                Debug.DrawLine(
                    levelGrid.GetWorldPosition(gridPositionList[i]),
                    levelGrid.GetWorldPosition(gridPositionList[i + 1]),
                    Color.red,
                    10f
                );
            }
        }
    }
}
