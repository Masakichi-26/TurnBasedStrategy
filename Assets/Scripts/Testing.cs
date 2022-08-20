using UnityEngine;
using VContainer;

public class Testing : MonoBehaviour
{
    [SerializeField] private Transform gridDebugObjectPrefab;

    private GridSystem gridSystem;

    private MouseWorld mouseWorld;

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
        
    }
}
