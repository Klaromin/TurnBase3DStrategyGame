using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance { get; private set; }
    
    private List<Unit> _unitList;
    private List<Unit> _friendlyUnitList;
    private List<Unit> _enemyUnitList;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one "+ name +" " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
        _unitList = new List<Unit>();
        _friendlyUnitList = new List<Unit>();
        _enemyUnitList = new List<Unit>();
    }

    private void Start()
    {
        AddEvents();
    }

    private void OnDisable()
    {
        RemoveEvents();
    }

    public List<Unit> GetUnitList()
    {
        return _unitList;
    }
    
    public List<Unit> GetEnemyUnitList()
    {
        return _enemyUnitList;
    }
    
    public List<Unit> GetFriendlyUnitList()
    {
        return _friendlyUnitList;
    }

    private void OnAnyUnitSpawned(object sender, EventArgs e)
    {
        Unit unit = sender as Unit;

        _unitList.Add(unit);
        if (unit.IsEnemy())
        {
            _enemyUnitList.Add(unit);
        }
        else
        {
           _friendlyUnitList.Add(unit);
        }
    }

    private void OnAnyUnitDead(object sender, EventArgs e)
    {
        Unit unit = sender as Unit;

        _unitList.Remove(unit);
        if (unit.IsEnemy())
        {
            _enemyUnitList.Remove(unit);
        }
        else
        {
            _friendlyUnitList.Remove(unit);
        }
    }
    
    private void AddEvents()
    {
        Unit.OnAnyUnitSpawnedEvent += OnAnyUnitSpawned;
    }

    private void RemoveEvents()
    {
        Unit.OnAnyUnitDeadEvent += OnAnyUnitDead;
    }


}