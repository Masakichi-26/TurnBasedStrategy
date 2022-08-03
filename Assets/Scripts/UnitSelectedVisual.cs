using System;
using UnityEngine;
using VContainer;

public class UnitSelectedVisual : MonoBehaviour
{
    [SerializeField] private Unit unit;

    [SerializeField] private MeshRenderer meshRenderer;

    private UnitActionSystem unitActionSystem;

    [Inject]
    private void Construct(UnitActionSystem unitActionSystem)
    {
        this.unitActionSystem = unitActionSystem;
        this.unitActionSystem.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
        Debug.Log("injecting unitActionSystem");
    }

    private void Start()
    {
        // unitActionSystem.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;

        UpdateVisual();
    }

    private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs empty)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        var selectedUnit = unitActionSystem.GetSelectedUnit();
        meshRenderer.enabled = selectedUnit == unit;
    }
}
