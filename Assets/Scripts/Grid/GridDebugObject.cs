using TMPro;
using UnityEngine;

public class GridDebugObject : MonoBehaviour
{
    private GridObject gridObject;

    [SerializeField] private TextMeshPro debugText;

    public void SetGridObject(GridObject gridObject)
    {
        this.gridObject = gridObject;
    }

    private void Update()
    {
        debugText.text = gridObject.ToString();
    }
}
