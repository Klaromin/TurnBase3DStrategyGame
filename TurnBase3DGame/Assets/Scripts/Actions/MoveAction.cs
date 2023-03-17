using System;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : BaseAction
{

    public event EventHandler OnStartMovingEvent;
    public event EventHandler OnStopMovingEvent;
    [SerializeField] private int maxMoveDistance = 4;

    private Vector3 _targetPosition;



    protected override void Awake()
    {
        base.Awake();
       _targetPosition = transform.position;
    }


    private void Update()
    {
        if (!IsActive)
        {
            return;
        }
        
        float stoppingDistance = .1f;
        Vector3 moveDirection = (_targetPosition - transform.position).normalized;
        
        if (Vector3.Distance(transform.position, _targetPosition) > stoppingDistance)
        {
            
            float moveSpeed = 4f;
            transform.position += moveDirection * (moveSpeed * Time.deltaTime);
           
        }
        else
        {
            OnStopMovingEvent?.Invoke(this, EventArgs.Empty);
            ActionComplete();
        }
        float rotateSpeed = 10f;
        transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);

    }


    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        this._targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);

        OnStartMovingEvent?.Invoke(this, EventArgs.Empty);
        
        ActionStart(onActionComplete);
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = Unit.GetGridPosition();

        for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
        {
            for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }

                if (unitGridPosition == testGridPosition)
                {
                    // Same Grid Position where the Unit is already at
                    continue;
                }

                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    // Grid Position already occupied with another Unit
                    continue;
                }

                validGridPositionList.Add(testGridPosition);
            }
        }

        return validGridPositionList;
    }
    
    public override string GetActionName()
    {
        return "Move";
    }

    public override Sprite GetActionSprite()
    {
        return Configuration.MoveButton;
    }
    
    public override EnemyAIAction GetEnemyAIAction(GridPosition gridPosition)
    {
        int targetCountAtGridPosition = Unit.GetShootAction().GetTargetCountAtPosition(gridPosition);
        
        return new EnemyAIAction
        {
            GridPosition = gridPosition,
            ActionValue = targetCountAtGridPosition * 10,

        };
    }
}