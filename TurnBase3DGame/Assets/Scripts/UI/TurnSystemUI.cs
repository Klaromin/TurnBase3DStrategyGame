using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystemUI : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _turnText;
    [SerializeField] private GameObject _enemyTurnVisualObject;

    private void Start()
    {
        AddEvents();
        _button.onClick.AddListener(TurnSystem.Instance.NextTurn);
        UpdateTurnText();
        UpdateEnemyTurnVisual();
    }

    private void UpdateTurnText()
    {
        _turnText.text ="Turn: "+ TurnSystem.Instance.GetTurnNumber();
    }

    private void OnTurnChanged(object sender, EventArgs e)
    {
        UpdateTurnText();
        UpdateEnemyTurnVisual();
    }

    private void UpdateEnemyTurnVisual()
    {
        _enemyTurnVisualObject.SetActive(!TurnSystem.Instance.IsPlayerTurn());
    }

    private void UpdateEnemyTurnButtonVisibility()
    {
        _button.gameObject.SetActive(TurnSystem.Instance.IsPlayerTurn());
    }

    private void AddEvents()
    {
        TurnSystem.Instance.OnTurnChangedEvent += OnTurnChanged;
    }
}
