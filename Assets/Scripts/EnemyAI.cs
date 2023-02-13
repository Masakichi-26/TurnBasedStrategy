using System;
using UnityEngine;
using VContainer;

public class EnemyAI : MonoBehaviour
{
    private float timer;

    private TurnSystem turnSystem;

    [Inject]
    private void Construct(TurnSystem turnSystem)
    {
        this.turnSystem = turnSystem;
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

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            turnSystem.NextTurn();
        }
    }

    private void TurnSystem_OnTurnChanged(object sender, EventArgs e)
    {
        timer = 2f;
    }
}
