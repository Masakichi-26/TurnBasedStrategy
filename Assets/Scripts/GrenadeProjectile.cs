using System;
using UnityEngine;
using VContainer;

public class GrenadeProjectile : MonoBehaviour
{
    private Vector3 targetPosition;
    private Action onGrenadeBehaviourComplete;

    private LevelGrid levelGrid;

    [Inject]
    private void Construct(LevelGrid levelGrid)
    {
        this.levelGrid = levelGrid;
        Debug.Log("injecting LevelGrid into GrenadeProjectile");
    }

    public void Setup(GridPosition targetGridPosition, Action onGrenadeBehaviourComplete)
    {
        this.onGrenadeBehaviourComplete = onGrenadeBehaviourComplete;
        targetPosition = levelGrid.GetWorldPosition(targetGridPosition);
    }

    private void Update()
    {
        Vector3 moveDir = (targetPosition - transform.position).normalized;
        float moveSpeed = 15f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        float reachedTargetDistance = 0.2f;
        if (Vector3.Distance(transform.position, targetPosition) < reachedTargetDistance)
        {
            TryDamageUnits();
            Destroy(gameObject);

            onGrenadeBehaviourComplete.Invoke();
        }
    }

    private void TryDamageUnits()
    {
        float damageRadius = 4f;
        Collider[] colliderArray = Physics.OverlapSphere(targetPosition, damageRadius);
        
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent<Unit>(out Unit targetUnit))
            {
                targetUnit.Damage(3);
            }
        }
    }
}
