using System.Collections.Generic;
using UnityEngine;

public class MoveAction : MonoBehaviour
{
    [SerializeField] private Animator unitAnimator;
    [SerializeField] private int maxMoveDistance = 4;
    [SerializeField] private Unit unit;
    private Vector3 targetPosition;
    
    private void Awake()
    {
        targetPosition = transform.position;
    }

    private void Update()
    {
        float stoppingDistance = 0.01f;
        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
        {
            Vector3 direction = (targetPosition - transform.position).normalized;
            float moveSpeed = 6f;
            transform.position += direction * moveSpeed * Time.deltaTime;

            float rotateSpeed = 10f;
            transform.forward = Vector3.Lerp(transform.forward, direction, Time.deltaTime * rotateSpeed);
            
            unitAnimator.SetBool("IsWalking", true);
        }
        else
        {
            unitAnimator.SetBool("IsWalking", false);
        }
    }

    public void Move(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }

    public List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new();

        GridPosition unitGridPosition = unit.GetGridPosition();

        for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
        {
            for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;
                Debug.Log(testGridPosition);
            }
        }

        return validGridPositionList;
    }
}
