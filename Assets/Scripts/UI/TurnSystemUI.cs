using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class TurnSystemUI : MonoBehaviour
{
    [SerializeField] private Button endTurnButton;
    [SerializeField] private TextMeshProUGUI turnNumberText;
    [SerializeField] private GameObject enemyTurnVisual;

    private TurnSystem turnSystem;

    [Inject]
    private void Construct(TurnSystem turnSystem)
    {
        this.turnSystem = turnSystem;
        Debug.Log("injecting TurnSystem into TurnSystemUI");
    }

    private void Start()
    {
        endTurnButton.onClick.AddListener(() =>
        {
            turnSystem.NextTurn();
        });

        turnSystem.OnTurnChanged += TurnSystem_OnTurnChanged;

        UpdateTurnNumberText();
        UpdateEnemyTurnVisual();
        UpdateEndTurnVisibility();
    }

    private void TurnSystem_OnTurnChanged(object sender, EventArgs e)
    {
        UpdateTurnNumberText();
        UpdateEnemyTurnVisual();
        UpdateEndTurnVisibility();
    }

    private void UpdateTurnNumberText()
    {
        turnNumberText.text = $"TURN {turnSystem.GetTurnNumber()}";
    }

    private void UpdateEnemyTurnVisual()
    {
        enemyTurnVisual.SetActive(turnSystem.IsPlayerTurn() is false);
    }

    private void UpdateEndTurnVisibility()
    {
        endTurnButton.gameObject.SetActive(turnSystem.IsPlayerTurn());
    }
}
