using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAction : BaseAction
{
    private float _totalSpinAmount;
    
    private void Update()
    {
        if (!IsActive)
        {
            return;
        }
        float spinAddAmount = 360f * Time.deltaTime;
        transform.eulerAngles += new Vector3(0,spinAddAmount, 0);
        
        _totalSpinAmount += spinAddAmount;
        if (_totalSpinAmount >= 360)
        {
            ActionComplete();
        }
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();
        GridPosition unitGridPosition = Unit.GetGridPosition();

        return new List<GridPosition>
        {
            unitGridPosition
        };
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        _totalSpinAmount = 0f;
        ActionStart(onActionComplete);
    }
    public override string GetActionName()
    {
        return "Spin";
    }

    public override Sprite GetActionSprite()
    {
        return Configuration.SpinButton;
    }

    public override int GetActionPointsCost()
    {
        return 2;
    }
}