using UnityEngine;
using VContainer;

public class Testing : MonoBehaviour
{
    private GridSystem gridSystem;

    private MouseWorld mouseWorld;

    [Inject]
    private void Construct(MouseWorld mouseWorld)
    {
        this.mouseWorld = mouseWorld;
    }

    private void Start()
    {
        gridSystem = new GridSystem(10, 10, 2f);

        Debug.Log(new GridPosition(5, 7));
    }

    private void Update()
    {
        Debug.Log(gridSystem.GetGridPosition(mouseWorld.GetPosition()));
    }
}
