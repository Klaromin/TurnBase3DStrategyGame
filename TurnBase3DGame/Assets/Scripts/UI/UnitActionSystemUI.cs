using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitActionSystemUI : MonoBehaviour
{
    [SerializeField] private Transform _actionButtonPrefab;
    [SerializeField] private Transform _actionButtonContainer;
    [SerializeField] private TextMeshProUGUI _actionPointsText;
    private List<ActionButtonUI> _actionButtonUIList;

    private void Awake()
    {
        _actionButtonUIList = new List<ActionButtonUI>();
    }

    private void Start()
    {
        AddEvent();
        
        UpdateActionPoints();
        CreateUnitActionButtons();
        UpdateSelectedVisual();
        
        
    }

    private void CreateUnitActionButtons()
    {
        foreach (Transform button in _actionButtonContainer)
        {
            Destroy(button.gameObject);
        }
        _actionButtonUIList.Clear();
        
        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();

        foreach (var baseAction in selectedUnit.GetBaseActions())
        {
           Transform actionButton = Instantiate(_actionButtonPrefab, _actionButtonContainer);
           var actionButtonUI = actionButton.GetComponent<ActionButtonUI>();
           actionButtonUI.SetBaseAction(baseAction);
           actionButtonUI.SetBaseSprite(baseAction);
           _actionButtonUIList.Add(actionButtonUI);
        }
    }
    
    private void OnSelectedUnitChanged(object sender, EventArgs e)
    {
        CreateUnitActionButtons();
        UpdateSelectedVisual();
        UpdateActionPoints();
    }

    private void OnSelectedActionChanged(object sender, EventArgs e)
    {
        UpdateSelectedVisual();
    }

    private void OnActionStarted(object sender, EventArgs e)
    {
        UpdateActionPoints();
    }
    
    private void OnTurnChanged(object sender, EventArgs e)
    {
        UpdateActionPoints();
    }

    private void UpdateSelectedVisual()
    {
        foreach (var actionButtonUI in _actionButtonUIList)
        {
            actionButtonUI.UpdateSelectedVisual();
        }
    }

    private void UpdateActionPoints()
    {
        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
        _actionPointsText.text = "Action Points: "+selectedUnit.GetActionPoints();
    }

    private void AddEvent()
    {
        UnitActionSystem.Instance.OnSelectedUnitChangedEvent += OnSelectedUnitChanged;
        UnitActionSystem.Instance.OnSelectedActionChangedEvent += OnSelectedActionChanged;
        UnitActionSystem.Instance.OnActionStartedEvent += OnActionStarted;
        TurnSystem.Instance.OnTurnChangedEvent += OnTurnChanged;
    }


}
