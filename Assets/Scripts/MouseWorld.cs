using UnityEngine;
using VContainer;

public class MouseWorld : MonoBehaviour
{
    [SerializeField] private LayerMask mousePlaneLayerMask;

    private InputManager inputManager;

    [Inject]
    private void Construct(InputManager inputManager)
    {
        this.inputManager = inputManager;
    }

    public Vector3 GetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(inputManager.GetMouseScreenPosition());
        Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, mousePlaneLayerMask);
        return raycastHit.point;
    }
}
