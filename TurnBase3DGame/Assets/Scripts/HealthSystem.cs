using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{

    public event EventHandler OnDeadEvent;
    public event EventHandler OnDamagedEvent;
    [SerializeField] private int _health = 100;
    private int _healthMax;

    private void Awake()
    {
        _healthMax = _health;
    }

    public void Damage(int damageAmount)
    {
        _health -= damageAmount;

        if (_health < 0)
        {
            _health = 0;
        }
        OnDamagedEvent?.Invoke(this, EventArgs.Empty);

        if (_health == 0)
        {
            Die();
        }
        Debug.Log(_health);
    }

    private void Die()
    {
        OnDeadEvent?.Invoke(this, EventArgs.Empty);
    }

    public float GetHealthNormalized()
    {
        return (float)_health / _healthMax;
    }
    
    
}
