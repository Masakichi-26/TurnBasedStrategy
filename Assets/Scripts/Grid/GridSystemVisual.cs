using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class GridSystemVisual : MonoBehaviour
{
    [SerializeField] private Transform gridSystemVisualSinglePrefab;

    private GridSystemVisualSingle[,] gridSystemVisualSingleArray;

    private LevelGrid levelGrid;

    private UnitActionSystem unitActionSystem;

    [Inject]
    private void Construct(LevelGrid levelGrid, UnitActionSystem unitActionSystem)
    {
        this.levelGrid        = levelGrid;
        this.unitActionSystem = unitActionSystem;
    }

    private void Start()
    {
        gridSystemVisualSingleArray = new GridSystemVisualSingle[
            levelGrid.GetWidth(),
            levelGrid.GetHeight()
        ];

        for (int x = 0; x < levelGrid.GetWidth(); x++)
        {
            for (int z = 0; z < levelGrid.GetHeight(); z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                Transform gridSystemVisualSingleTransform =
                    Instantiate(gridSystemVisualSinglePrefab, levelGrid.GetWorldPosition(gridPosition), Quaternion.identity);
                
                gridSystemVisualSingleArray[x, z] = gridSystemVisualSingleTransform.GetComponent<GridSystemVisualSingle>();
            }
        }
    }

    private void Update()
    {
        UpdateGridVisual();
    }

    public void HideAllGridPositions()
    {
        for (int x = 0; x < levelGrid.GetWidth(); x++)
        {
            for (int z = 0; z < levelGrid.GetHeight(); z++)
            {
                gridSystemVisualSingleArray[x, z].Hide();
            }
        }
    }

    public void ShowGridPositionList(List<GridPosition> gridPositionList)
    {
        foreach (var gridPosition in gridPositionList)
        {
            gridSystemVisualSingleArray[gridPosition.x, gridPosition.z].Show();
        }
    }

    private void UpdateGridVisual()
    {
        HideAllGridPositions();

        Unit selectedUnit = unitActionSystem.GetSelectedUnit();
        
        ShowGridPositionList(
            selectedUnit.GetMoveAction().GetValidActionGridPositionList()
        );
    }
}
