using System;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    public event EventHandler OnTurnChanged;

    private int turnNumber = 1;

    public void NextTurn()
    {
        turnNumber++;

        OnTurnChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetTurnNumber()
    {
        return turnNumber;
    }
}
