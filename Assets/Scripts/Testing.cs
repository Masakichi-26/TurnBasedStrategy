using UnityEngine;
using VContainer;

public class Testing : MonoBehaviour
{
    [SerializeField] private Transform gridDebugObjectPrefab;

    private GridSystem gridSystem;

    private MouseWorld mouseWorld;

    [SerializeField] private Unit unit;

    [Inject]
    private void Construct(MouseWorld mouseWorld)
    {
        this.mouseWorld = mouseWorld;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            unit.GetMoveAction().GetValidActionGridPositionList();
        }
    }
}
