using System;
using UnityEngine;
using VContainer;

public class UnitActionSystem : MonoBehaviour
{
    public event EventHandler OnSelectedUnitChanged;

    [SerializeField] private Unit selectedUnit;

    [SerializeField] private LayerMask unitLayerMask;

    private MouseWorld mouseWorld;

    private LevelGrid levelGrid;

    [Inject]
    private void Construct(MouseWorld mouseWorld, LevelGrid levelGrid)
    {
        this.mouseWorld = mouseWorld;
        this.levelGrid  = levelGrid;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (TryHandleUnitSelection()) return;

            GridPosition mouseGridPosition = levelGrid.GetGridPosition(mouseWorld.GetPosition());
            MoveAction unitMoveAction = selectedUnit.GetMoveAction();
            
            if (unitMoveAction.IsValidActionGridPosition(mouseGridPosition))
            {
                unitMoveAction.Move(mouseGridPosition);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            selectedUnit.GetSpinAction().Spin();
        }
    }

    private bool TryHandleUnitSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitLayerMask) == false)
        {
            return false;
        }

        if (raycastHit.transform.TryGetComponent<Unit>(out Unit unit) == false)
        {
            return false;
        }

        SetSelectedUnit(unit);
        return true;
    }

    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;
        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
    }

    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }
}
