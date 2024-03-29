using System;
using UnityEngine;
using VContainer;

public class GrenadeProjectile : MonoBehaviour
{
    public static event EventHandler OnAnyGrenadeExploded;

    [SerializeField] private Transform grenadeExplodeVfxPrefab;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private AnimationCurve arcYAnimationCurve;

    private Vector3 targetPosition;
    private Action onGrenadeBehaviourComplete;
    private float totalDistance;
    private Vector3 positionXZ;

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

        positionXZ = transform.position;
        positionXZ.y = 0;
        totalDistance = Vector3.Distance(positionXZ, targetPosition);
    }

    private void Update()
    {
        Vector3 moveDir = (targetPosition - positionXZ).normalized;
        float moveSpeed = 15f;
        positionXZ += moveDir * moveSpeed * Time.deltaTime;

        float distance = Vector3.Distance(positionXZ, targetPosition);
        float distanceNormalized = 1 - (distance / totalDistance);

        float maxHeight = totalDistance / 4f;
        float positionY = arcYAnimationCurve.Evaluate(distanceNormalized) * maxHeight;
        transform.position = new Vector3(positionXZ.x, positionY, positionXZ.z);

        float reachedTargetDistance = 0.2f;
        if (Vector3.Distance(positionXZ, targetPosition) < reachedTargetDistance)
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

            if (collider.TryGetComponent<DestructibleCrate>(out DestructibleCrate crate))
            {
                crate.Damage();
            }
        }

        OnAnyGrenadeExploded?.Invoke(this, EventArgs.Empty);

        trailRenderer.transform.parent = null;
        Instantiate(grenadeExplodeVfxPrefab, targetPosition + Vector3.up * 1f, Quaternion.identity);
    }
}
