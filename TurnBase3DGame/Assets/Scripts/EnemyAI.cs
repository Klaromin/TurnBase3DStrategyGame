using System;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private float _timer;

    private void Start()
    {
        AddEvent();
    }

    void Update()
    {
        if (TurnSystem.Instance.IsPlayerTurn())
        {
            return;
        }

        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            TurnSystem.Instance.NextTurn();
        }
    }

    private void OnTurnChanged(object sender, EventArgs e)
    {
        _timer = 2f;
    }

    private void AddEvent()
    {
        TurnSystem.Instance.OnTurnChangedEvent += OnTurnChanged;
    }
}
