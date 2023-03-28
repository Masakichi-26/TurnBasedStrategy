using System;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;

public class UnitActionSystem : MonoBehaviour
{
    public event EventHandler OnSelectedUnitChanged;
    public event EventHandler OnSelectedActionChanged;
    public event EventHandler<bool> OnIsBusyChanged;
    public event EventHandler OnActionStarted;

    [SerializeField] private Unit selectedUnit;

    [SerializeField] private LayerMask unitLayerMask;

    private BaseAction selectedAction;
    private bool isBusy;

    private MouseWorld mouseWorld;
    private LevelGrid levelGrid;
    private TurnSystem turnSystem;
    private InputManager inputManager;

    [Inject]
    private void Construct(MouseWorld mouseWorld, LevelGrid levelGrid, TurnSystem turnSystem,
        InputManager inputManager)
    {
        this.mouseWorld = mouseWorld;
        this.levelGrid = levelGrid;
        this.turnSystem = turnSystem;
        this.inputManager = inputManager;
    }

    private void Start()
    {
        SetSelectedUnit(selectedUnit);
    }

    private void Update()
    {
        if (isBusy)
        {
            return;
        }

        if (turnSystem.IsPlayerTurn() == false)
        {
            return;
        }

        // if mouse is over button, it won't take action
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (TryHandleUnitSelection())
        {
            return;
        }

        HandleSelectedAction();
    }

    private void HandleSelectedAction()
    {
        if (inputManager.IsMouseButtonDown())
        {
            GridPosition mouseGridPosition = levelGrid.GetGridPosition(mouseWorld.GetPosition());
            if (selectedAction.IsValidActionGridPosition(mouseGridPosition) is false)
            {
                return;
            }

            if (selectedUnit.TrySpendActionPointsToTakeAction(selectedAction) is false)
            {
                return;
            }

            SetBusy();
            selectedAction.TakeAction(mouseGridPosition, ClearBusy);

            OnActionStarted?.Invoke(this, EventArgs.Empty);
        }
    }

    private void SetBusy()
    {
        isBusy = true;
        OnIsBusyChanged?.Invoke(this, isBusy);
    }

    private void ClearBusy()
    {
        isBusy = false;
        OnIsBusyChanged?.Invoke(this, isBusy);
    }

    private bool TryHandleUnitSelection()
    {
        if (inputManager.IsMouseButtonDown())
        {
            Ray ray = Camera.main.ScreenPointToRay(inputManager.GetMouseScreenPosition());
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitLayerMask) == false)
            {
                return false;
            }

            if (raycastHit.transform.TryGetComponent<Unit>(out Unit unit) == false)
            {
                return false;
            }

            if (unit == selectedUnit)
            {
                return false;
            }

            if (unit.IsEnemy())
            {
                // enemy was selected
                return false;
            }

            SetSelectedUnit(unit);
            return true;
        }

        return false;
    }

    public void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;
        SetSelectedAction(unit.GetAction<MoveAction>());
        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
    }

    public void SetSelectedAction(BaseAction action)
    {
        selectedAction = action;
        OnSelectedActionChanged?.Invoke(this, EventArgs.Empty);
    }

    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }

    public BaseAction GetSelectedAction()
    {
        return selectedAction;
    }

    public bool GetIsBusy()
    {
        return isBusy;
    }
}
