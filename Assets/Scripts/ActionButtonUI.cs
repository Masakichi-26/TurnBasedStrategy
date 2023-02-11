using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class ActionButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private Button button;

    public void SetBaseAction(BaseAction baseAction, UnitActionSystem unitActionSystem)
    {
        textMeshPro.text = baseAction.GetActionName().ToUpper();

        button.onClick.AddListener(() => {
            unitActionSystem.SetSelectedAction(baseAction);
        });
    }
}
