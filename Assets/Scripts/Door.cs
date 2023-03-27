using System;
using UnityEngine;
using VContainer;

public class Door : MonoBehaviour
{
    [SerializeField] private bool isOpen;
    [SerializeField] private Animator animator;

    private GridPosition gridPosition;
    private Action onInteractComplete;
    private float timer;
    private bool isActive;

    private LevelGrid levelGrid;
    private Pathfinding pathfinding;

    [Inject]
    private void Construct(LevelGrid levelGrid, Pathfinding pathfinding)
    {
        this.levelGrid = levelGrid;
        this.pathfinding = pathfinding;
    }

    private void Start()
    {
        gridPosition = levelGrid.GetGridPosition(transform.position);
        levelGrid.SetDoorAtGridPosition(gridPosition, this);

        if (isOpen)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
    }

    private void Update()
    {
        if (isActive is false)
        {
            return;
        }

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            isActive = false;
            onInteractComplete();
        }
    }

    public void Interact(Action onInteractComplete)
    {
        this.onInteractComplete = onInteractComplete;
        isActive = true;
        timer = 0.5f;

        if (isOpen)
        {
            CloseDoor();
        }
        else
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        isOpen = true;
        UpdateAnimator();
        UpdateIsPassable();
    }

    private void CloseDoor()
    {
        isOpen = false;
        UpdateAnimator();
        UpdateIsPassable();
    }

    private void UpdateAnimator()
    {
        animator.SetBool("IsOpen", isOpen);
    }

    private void UpdateIsPassable()
    {
        pathfinding.SetIsPassableGridPosition(gridPosition, isOpen);
    }
}
