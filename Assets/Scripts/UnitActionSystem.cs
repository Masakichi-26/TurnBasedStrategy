using UnityEngine;
using VContainer;

public class UnitActionSystem : MonoBehaviour
{
    [SerializeField] private Unit selectedUnit;

    [SerializeField] private LayerMask unitLayerMask;

    private MouseWorld mouseWorld;

    [Inject]
    private void Construct(MouseWorld mouseWorld)
    {
        this.mouseWorld = mouseWorld;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (TryHandleUnitSelection()) return;
            selectedUnit.Move(mouseWorld.GetPosition());
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

        selectedUnit = unit;
        return true;
    }
}
