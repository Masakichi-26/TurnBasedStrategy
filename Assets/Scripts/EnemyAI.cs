using System;
using UnityEngine;
using VContainer;

public class EnemyAI : MonoBehaviour
{
    private enum State
    {
        WaitingForEnemyTurn,
        TakingTurn,
        Busy,
    }

    private State state;
    private float timer;

    private TurnSystem turnSystem;
    private UnitManager unitManager;

    [Inject]
    private void Construct(TurnSystem turnSystem, UnitManager unitManager)
    {
        this.turnSystem = turnSystem;
        this.unitManager = unitManager;
    }

    private void Awake()
    {
        state = State.WaitingForEnemyTurn;
    }

    private void Start()
    {
        turnSystem.OnTurnChanged += TurnSystem_OnTurnChanged;
    }

    private void Update()
    {
        if (turnSystem.IsPlayerTurn())
        {
            return;
        }

        switch (state)
        {
            case State.WaitingForEnemyTurn:
                break;
            case State.TakingTurn:
                timer -= Time.deltaTime;
                if (timer <= 0f)
                {
                    if (TryTakeEnemyAIAction(SetStateTakingTurn))
                    {
                        state = State.Busy;
                    }
                    else
                    {
                        // no more enemies have actions to take, so end enemy turn
                        turnSystem.NextTurn();
                    }
                }
                break;
            case State.Busy:
                break;
        }
    }

    private void SetStateTakingTurn()
    {
        timer = 0.5f;
        state = State.TakingTurn;
    }

    private void TurnSystem_OnTurnChanged(object sender, EventArgs e)
    {
        if (turnSystem.IsPlayerTurn() is false)
        {
            state = State.TakingTurn;
            timer = 2f;
        }
    }

    private bool TryTakeEnemyAIAction(Action onEnemyAIActionComplete)
    {
        Debug.Log("TakeEnemyAIAction");
        foreach (Unit enemyUnit in unitManager.GetEnemyUnitList())
        {
            if (TryTakeEnemyAIAction(enemyUnit, onEnemyAIActionComplete))
            {
                return true;
            }
        }

        return false;
    }

    private bool TryTakeEnemyAIAction(Unit enemyUnit, Action onEnemyAIActionComplete)
    {
        var spinAction = enemyUnit.GetSpinAction();

        GridPosition actionGridPosition = enemyUnit.GetGridPosition();
        if (spinAction.IsValidActionGridPosition(actionGridPosition) is false)
        {
            return false;
        }

        if (enemyUnit.TrySpendActionPointsToTakeAction(spinAction) is false)
        {
            return false;
        }

        Debug.Log("Spin Action");

        spinAction.TakeAction(actionGridPosition, onEnemyAIActionComplete);

        return true;
    }
}
