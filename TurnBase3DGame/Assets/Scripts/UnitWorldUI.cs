using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class UnitWorldUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _actionPointsText;
    [SerializeField] private Image _healthBarImage;
    [SerializeField] private HealthSystem _healthSystem;
    [SerializeField] private Unit _unit;

    private void Start()
    {
        AddEvents();
        UpdateActionPointsText();
        UpdateHealthBar();
    }

    private void OnDisable()
    {
        RemoveEvents();
    }

    private void UpdateActionPointsText()
    {
        _actionPointsText.text = _unit.GetActionPoints().ToString();
    }

    private void UpdateHealthBar()
    {
        _healthBarImage.fillAmount = _healthSystem.GetHealthNormalized();
    }
    
    private void OnActionPointsChanged(object sender, EventArgs e)
    {
        UpdateActionPointsText();
    }
    
    private void OnDamaged(object sender, EventArgs e)
    {
       UpdateHealthBar();
       
       //Alta bir fonksiyon oluştur health birden çat diye düşmek yerine yavaşça düşsün efekt olarak. Daha önce bunu yapmıştın.
    }

    private void AddEvents()
    {
        _unit.OnActionPointsChangedEvent += OnActionPointsChanged;
        _healthSystem.OnDamagedEvent += OnDamaged;
    }



    private void RemoveEvents()
    {
        _unit.OnActionPointsChangedEvent -= OnActionPointsChanged;
        _healthSystem.OnDamagedEvent -= OnDamaged;
    }
}
