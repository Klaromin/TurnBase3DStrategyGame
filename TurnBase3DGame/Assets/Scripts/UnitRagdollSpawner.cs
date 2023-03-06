using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRagdollSpawner : MonoBehaviour
{
    [SerializeField] private Transform _ragdollPrefab;
    [SerializeField] private Transform _originalRootBone;

    private HealthSystem _healthSystem;

    private void Awake()
    {
        _healthSystem = GetComponent<HealthSystem>();

        AddEvents();
    }

    private void AddEvents()
    {
        _healthSystem.OnDeadEvent += OnDead;
    }

    private void OnDead(object sender, EventArgs e)
    {
     var ragdollTransform = Instantiate(_ragdollPrefab, transform.position, transform.rotation);
     var unitRagdoll = ragdollTransform.GetComponent<UnitRagdoll>();
     unitRagdoll.Setup(_originalRootBone);

    }
}
