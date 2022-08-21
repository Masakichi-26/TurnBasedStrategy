using UnityEngine;
using VContainer;

public class Testing : MonoBehaviour
{
    [SerializeField] private Transform gridDebugObjectPrefab;

    [SerializeField] private Unit unit;

    private GridSystem gridSystem;

    private MouseWorld mouseWorld;

    private GridSystemVisual gridSystemVisual;

    [Inject]
    private void Construct(MouseWorld mouseWorld, GridSystemVisual gridSystemVisual)
    {
        this.mouseWorld       = mouseWorld;
        this.gridSystemVisual = gridSystemVisual;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            
        }
    }
}
