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

    private ScreenShake screenShake;


    [Inject]
    private void Construct(MouseWorld mouseWorld, GridSystemVisual gridSystemVisual,
        LevelGrid levelGrid, Pathfinding pathfinding, ScreenShake screenShake)
    {
        this.mouseWorld = mouseWorld;
        this.gridSystemVisual = gridSystemVisual;
        this.levelGrid = levelGrid;
        this.pathfinding = pathfinding;
        this.screenShake = screenShake;

        Debug.Log("injecting into Testing");
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            // PathfindingDebug();

            // ScreenShake();
        }
    }

    private void PathfindingDebug()
    {
        Debug.Log("T key pressed");

        GridPosition mouseGridPosition = levelGrid.GetGridPosition(mouseWorld.GetPosition());
        GridPosition startGridPosition = new GridPosition(2, 0);

        var gridPositionList = pathfinding.FindPath(startGridPosition, mouseGridPosition, out int pathLength);

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

    private void ScreenShake()
    {
        screenShake.Shake(5f);
    }
}
