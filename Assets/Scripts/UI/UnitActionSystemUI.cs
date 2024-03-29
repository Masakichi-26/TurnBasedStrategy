using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using VContainer;

public class UnitActionSystemUI : MonoBehaviour
{
    [SerializeField] private Transform actionButtonPrefab;
    [SerializeField] private Transform actionButtonContainerTransform;
    [SerializeField] private TextMeshProUGUI actionPointsText;

    private UnitActionSystem unitActionSystem;
    private TurnSystem turnSystem;
    private List<ActionButtonUI> actionButtonUIList = new();

    [Inject]
    private void Construct(UnitActionSystem unitActionSystem, TurnSystem turnSystem)
    {
        this.unitActionSystem = unitActionSystem;
        this.turnSystem = turnSystem;
        Debug.Log("injecting UnitActionSystem into UnitActionSystemUI");
        Debug.Log("injecting TurnSystem into UnitActionSystemUI");
    }

    private void Start()
    {
        unitActionSystem.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
        unitActionSystem.OnSelectedActionChanged += UnitActionSystem_OnSelectedActionChanged;
        unitActionSystem.OnActionStarted += UnitActionSystem_OnActionStarted;
        turnSystem.OnTurnChanged += TurnSystem_OnTurnChanged;
        Unit.OnAnyActionPointsChanged += Unit_OnAnyActionPointsChanged;

        UpdateActionPoints();
        CreateUnitActionButtons();
    }

    private void CreateUnitActionButtons()
    {
        foreach (Transform buttonTransform in actionButtonContainerTransform)
        {
            Destroy(buttonTransform.gameObject);
        }

        actionButtonUIList.Clear();

        Unit selectedUnit = unitActionSystem.GetSelectedUnit();

        foreach (var baseAction in selectedUnit.GetBaseActionArray())
        {
            Transform actionButton = Instantiate(actionButtonPrefab, actionButtonContainerTransform);
            var actionButtonUI = actionButton.GetComponent<ActionButtonUI>();
            actionButtonUI.SetBaseAction(baseAction, unitActionSystem);

            actionButtonUIList.Add(actionButtonUI);
        }

        UpdateSelectedVisual();
    }

    private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs e)
    {
        UpdateActionPoints();
        CreateUnitActionButtons();
    }

    private void UnitActionSystem_OnSelectedActionChanged(object sender, EventArgs e)
    {
        UpdateSelectedVisual();
    }

    private void UnitActionSystem_OnActionStarted(object sender, EventArgs e)
    {
        UpdateActionPoints();
    }

    private void TurnSystem_OnTurnChanged(object sender, EventArgs e)
    {
        UpdateActionPoints();
    }

    private void Unit_OnAnyActionPointsChanged(object sender, EventArgs e)
    {
        UpdateActionPoints();
    }

    private void UpdateSelectedVisual()
    {
        foreach (var actionButtonUI in actionButtonUIList)
        {
            actionButtonUI.UpdateSelectedVisual();
        }
    }

    private void UpdateActionPoints()
    {
        var selectedUnit = unitActionSystem.GetSelectedUnit();
        actionPointsText.text = $"Action Points: {selectedUnit.GetActionPoints()}";
    }
}
