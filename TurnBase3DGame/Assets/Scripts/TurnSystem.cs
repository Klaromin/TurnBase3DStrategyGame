using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    public event EventHandler OnTurnChangedEvent;
    public static TurnSystem Instance { get; private set; }
    private int _turnNumber = 1;
    private bool _isPlayerTurn = true;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one Turn System! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void NextTurn()
    {
        _turnNumber++;
        _isPlayerTurn = !_isPlayerTurn;
        OnTurnChangedEvent?.Invoke(this, EventArgs.Empty);
    }

    public int GetTurnNumber()
    {
        return _turnNumber;
    }

    public bool IsPlayerTurn()
    {
        return _isPlayerTurn;
    }
}
