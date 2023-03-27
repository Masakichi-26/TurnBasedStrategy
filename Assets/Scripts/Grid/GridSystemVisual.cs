using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class GridSystemVisual : MonoBehaviour
{
    [Serializable]
    public struct GridVisualTypeMaterial
    {
        public GridVisualType gridVisualType;
        public Material material;
    }

    public enum GridVisualType
    {
        White,
        Blue,
        Red,
        RedSoft,
        Yellow,
    }

    [SerializeField] private Transform gridSystemVisualSinglePrefab;
    [SerializeField] private List<GridVisualTypeMaterial> gridVisualTypeMaterialList;

    private GridSystemVisualSingle[,] gridSystemVisualSingleArray;

    private LevelGrid levelGrid;

    private UnitActionSystem unitActionSystem;

    [Inject]
    private void Construct(LevelGrid levelGrid, UnitActionSystem unitActionSystem)
    {
        this.levelGrid = levelGrid;
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

        unitActionSystem.OnSelectedActionChanged += UnitActionSystem_OnSelectedActionChanged;
        levelGrid.OnAnyUnitMovedGridPosition += LevelGrid_OnAnyUnitMovedGridPosition;

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

    private void ShowGridPositionRange(GridPosition gridPosition, int range, GridVisualType gridVisualType)
    {
        List<GridPosition> gridPositionList = new List<GridPosition>();
        for (int x = -range; x <= range; x++)
        {
            for (int z = -range; z <= range; z++)
            {
                GridPosition testGridPosition = gridPosition + new GridPosition(x, z);

                if (levelGrid.IsValidGridPosition(testGridPosition) is false)
                {
                    continue;
                }

                int testDistance = Mathf.Abs(x) + Mathf.Abs(z);
                if (testDistance > range)
                {
                    continue;
                }

                gridPositionList.Add(testGridPosition);
            }
        }

        ShowGridPositionList(gridPositionList, gridVisualType);
    }

    private void ShowGridPositionRangeSquare(GridPosition gridPosition, int range, GridVisualType gridVisualType)
    {
        List<GridPosition> gridPositionList = new List<GridPosition>();
        for (int x = -range; x <= range; x++)
        {
            for (int z = -range; z <= range; z++)
            {
                GridPosition testGridPosition = gridPosition + new GridPosition(x, z);

                if (levelGrid.IsValidGridPosition(testGridPosition) is false)
                {
                    continue;
                }

                gridPositionList.Add(testGridPosition);
            }
        }

        ShowGridPositionList(gridPositionList, gridVisualType);
    }

    public void ShowGridPositionList(List<GridPosition> gridPositionList, GridVisualType gridVisualType)
    {
        var material = GetGridVisualTypeMaterial(gridVisualType);
        foreach (var gridPosition in gridPositionList)
        {
            gridSystemVisualSingleArray[gridPosition.x, gridPosition.z].Show(material);
        }
    }

    private void UpdateGridVisual()
    {
        HideAllGridPositions();

        Unit selectedUnit = unitActionSystem.GetSelectedUnit();
        BaseAction selectedAction = unitActionSystem.GetSelectedAction();

        GridVisualType gridVisualType;
        switch (selectedAction)
        {
            default:
            case MoveAction moveAction:
                gridVisualType = GridVisualType.White;
                break;
            case SpinAction spinAction:
                gridVisualType = GridVisualType.Blue;
                break;
            case ShootAction shootAction:
                gridVisualType = GridVisualType.Red;
                ShowGridPositionRange(selectedUnit.GetGridPosition(), shootAction.GetMaxShootDistance(), GridVisualType.RedSoft);
                break;
            case GrenadeAction grenadeAction:
                gridVisualType = GridVisualType.Yellow;
                break;
            case SwordAction swordAction:
                gridVisualType = GridVisualType.Red;
                ShowGridPositionRangeSquare(selectedUnit.GetGridPosition(), swordAction.GetMaxSwordDistance(), GridVisualType.RedSoft);
                break;
        }

        ShowGridPositionList(
            selectedAction.GetValidActionGridPositionList(),
            gridVisualType
        );
    }

    private void UnitActionSystem_OnSelectedActionChanged(object sender, EventArgs e)
    {
        UpdateGridVisual();
    }

    private void LevelGrid_OnAnyUnitMovedGridPosition(object sender, EventArgs e)
    {
        UpdateGridVisual();
    }

    private Material GetGridVisualTypeMaterial(GridVisualType type)
    {
        foreach (GridVisualTypeMaterial material in gridVisualTypeMaterialList)
        {
            if (material.gridVisualType == type)
            {
                return material.material;
            }
        }

        Debug.LogError("Could not find GridVisualTypeMaterial for GridVisualType " + type);
        return null;
    }
}
