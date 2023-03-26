using System;
using UnityEngine;
using VContainer;

public class DestructibleCrate : MonoBehaviour
{
    public static event EventHandler OnAnyDestroyed;

    [SerializeField] private Transform crateDestroyedPrefab;

    private GridPosition gridPosition;
    private LevelGrid levelGrid;

    [Inject]
    private void Construct(LevelGrid levelGrid)
    {
        this.levelGrid = levelGrid;
    }

    private void Start()
    {
        gridPosition = levelGrid.GetGridPosition(transform.position);
    }

    public void Damage()
    {
        Transform crateDestroyedTransform = Instantiate(crateDestroyedPrefab, transform.position, transform.rotation);
        ApplyExplosionToChildren(crateDestroyedTransform, 150f, transform.position, 10f);
        
        Destroy(gameObject);
        OnAnyDestroyed?.Invoke(this, EventArgs.Empty);
    }

    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }

    private void ApplyExplosionToChildren(Transform root, float explosionForce, Vector3 explostionPosition, float explosionRange)
    {
        foreach (Transform child in root)
        {
            if (child.TryGetComponent<Rigidbody>(out Rigidbody childRigidBody))
            {
                childRigidBody.AddExplosionForce(explosionForce, explostionPosition, explosionRange);
            }
            
            ApplyExplosionToChildren(child, explosionForce, explostionPosition, explosionRange);
        }
    }
}
