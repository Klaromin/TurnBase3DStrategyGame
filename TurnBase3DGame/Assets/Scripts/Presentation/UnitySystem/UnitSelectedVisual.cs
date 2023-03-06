using System;

using UnityEngine;

public class UnitSelectedVisual : MonoBehaviour
{
    [SerializeField] private Unit _unit;
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        AddEvents();
        UpdateVisual();
    }

    private void OnDisable()
    {
        RemoveEvents();
    }

    private void OnSelectedUnitChanged(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        _meshRenderer.enabled = UnitActionSystem.Instance.GetSelectedUnit() == _unit;
    }

    private void AddEvents()
    {
        UnitActionSystem.Instance.OnSelectedUnitChangedEvent += OnSelectedUnitChanged;
    }

    private void RemoveEvents()
    {
        UnitActionSystem.Instance.OnSelectedUnitChangedEvent -= OnSelectedUnitChanged;
    }
}
