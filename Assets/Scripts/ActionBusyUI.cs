using UnityEngine;
using VContainer;

public class ActionBusyUI : MonoBehaviour
{
    private UnitActionSystem unitActionSystem;

    [Inject]
    private void Construct(UnitActionSystem unitActionSystem)
    {
        this.unitActionSystem = unitActionSystem;
    }

    private void Start()
    {
        unitActionSystem.OnIsBusyChanged += UnitActionSystem_OnIsBusyChanged;
        UpdateVisual(false);
    }

    public void UpdateVisual(bool isShow)
    {
        gameObject.SetActive(isShow);
    }

    private void UnitActionSystem_OnIsBusyChanged(object sender, bool isBusy)
    {
        UpdateVisual(isBusy);
    }
}
