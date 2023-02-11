using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class ActionButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private Button button;
    [SerializeField] private Image selectedVisual;

    private BaseAction baseAction;
    private UnitActionSystem unitActionSystem;

    public void SetBaseAction(BaseAction baseAction, UnitActionSystem unitActionSystem)
    {
        this.baseAction = baseAction;
        this.unitActionSystem = unitActionSystem;
        textMeshPro.text = baseAction.GetActionName().ToUpper();

        button.onClick.AddListener(() => {
            unitActionSystem.SetSelectedAction(baseAction);
        });
    }

    public BaseAction GetBaseAction()
    {
        return baseAction;
    }

    public void UpdateSelectedVisual()
    {
        var selectedAction = unitActionSystem.GetSelectedAction();
        selectedVisual.gameObject.SetActive(selectedAction == baseAction);
    }
}
