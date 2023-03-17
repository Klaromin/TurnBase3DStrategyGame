using System;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Unit : MonoBehaviour
{
    private const int ACTION_POINTS_MAX = 2;
    public event EventHandler OnActionPointsChangedEvent;
    public static EventHandler OnAnyUnitSpawnedEvent;
    public static EventHandler OnAnyUnitDeadEvent;
    
    [SerializeField] private bool _isEnemy;
    
    private MoveAction _moveAction;
    private SpinAction _spinAction;
    private ShootAction _shootAction;
    private BaseAction[] _baseActionArray;
    private GridPosition _gridPosition;
    private HealthSystem _healthSystem;
    private int _actionPoints = ACTION_POINTS_MAX;

    private void Awake()
    {
        _healthSystem = GetComponent<HealthSystem>();
        _moveAction = GetComponent<MoveAction>();
        _spinAction = GetComponent<SpinAction>();
        _shootAction = GetComponent<ShootAction>();
        _baseActionArray = GetComponents<BaseAction>();
    }

    private void Start()
    {
        AddEvents();
        _gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(_gridPosition, this);
        OnAnyUnitSpawnedEvent?.Invoke(this, EventArgs.Empty);
    }

    private void OnDestroy()
    {
        RemoveEvents();
    }

    private void Update()
    {

        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if (newGridPosition != _gridPosition)
        {
            GridPosition oldGridPosition = _gridPosition;
            _gridPosition = newGridPosition;
            LevelGrid.Instance.UnitMovedGridPosition(this, oldGridPosition, newGridPosition);
        }
    }

    public MoveAction GetMoveAction()
    {
        return _moveAction;
    }

    public SpinAction GetSpinAction()
    {
        return _spinAction;
    }
    
    public ShootAction GetShootAction()
    {
        return _shootAction;
    }

    public GridPosition GetGridPosition()
    {
        return _gridPosition;
    }

    public Vector3 GetWorldPosition()
    {
        return transform.position;
    }

    public BaseAction[] GetBaseActions()
    {
        return _baseActionArray;
    }

    public float GetHealthNormalized()
    {
        return _healthSystem.GetHealthNormalized();
    }

    public bool TrySpendActionPointsToTakeAction(BaseAction baseAction)
    {
        if (CanSpendActionPointsToTakeAction(baseAction))
        {
            SpendActionPoints(baseAction.GetActionPointsCost());
            OnActionPointsChangedEvent?.Invoke(this, EventArgs.Empty);
            return true;
        }

        return false;
    }

    public bool CanSpendActionPointsToTakeAction(BaseAction baseAction)
    {
        if (_actionPoints >= baseAction.GetActionPointsCost())
        {
            return true;
        }

        return false;
    }

    public int GetActionPoints()
    {
        return _actionPoints;
    }

    public bool IsEnemy()
    {
        return _isEnemy;
    }

    private void SpendActionPoints(int amount)
    {
        _actionPoints -= amount;
    }

    private void OnTurnChanged(object sender, EventArgs e)
    {
        if ((IsEnemy() && !TurnSystem.Instance.IsPlayerTurn()) || (!IsEnemy() && TurnSystem.Instance.IsPlayerTurn()))
        {
            _actionPoints = ACTION_POINTS_MAX;
            OnActionPointsChangedEvent?.Invoke(this, EventArgs.Empty);
        }
        
    }

    public void Damage(int damageAmount)
    {
        _healthSystem.Damage(damageAmount);
    }
    
    private void OnDead(object sender, EventArgs e)
    {
        LevelGrid.Instance.RemoveUnitAtGridPosition(_gridPosition, this);
        Destroy(gameObject);
        OnAnyUnitDeadEvent?.Invoke(this, EventArgs.Empty);
    }
    
    private void AddEvents()
    {
        TurnSystem.Instance.OnTurnChangedEvent += OnTurnChanged;
        _healthSystem.OnDeadEvent += OnDead;
    }

    private void RemoveEvents()
    {
        TurnSystem.Instance.OnTurnChangedEvent -= OnTurnChanged;
        _healthSystem.OnDeadEvent -= OnDead;
    }


}