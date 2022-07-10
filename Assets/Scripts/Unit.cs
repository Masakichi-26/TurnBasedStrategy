using UnityEngine;
using VContainer;

public class Unit : MonoBehaviour
{
    private Vector3 targetPosition;

    private MouseWorld mouseWorld;

    [Inject]
    private void Construct(MouseWorld mouseWorld)
    {
        this.mouseWorld = mouseWorld;
    }

    private void Update()
    {
        float stoppingDistance = 0.01f;
        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
        {
            Vector3 direction = (targetPosition - transform.position).normalized;
            float moveSpeed = 4f;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
        
        
        if (Input.GetMouseButtonDown(0))
        {
            Move(mouseWorld.GetPosition());
        }
    }

    private void Move(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }
}
