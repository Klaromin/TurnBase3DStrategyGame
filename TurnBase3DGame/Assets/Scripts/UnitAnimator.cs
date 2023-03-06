using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _bulletProjectilePrefab;
    [SerializeField] private Transform _shootPosition;

    private void Awake()
    {
        AddEvents();
    }

    private void OnStartMoving(object sender, EventArgs e)
    {
        _animator.SetBool("IsWalking", true);
    }
    
    private void OnStopMoving(object sender, EventArgs e)
    {
        _animator.SetBool("IsWalking", false);
    }

    private void OnShoot(object sender, ShootAction.OnShootEventArgs e)
    {
        _animator.SetTrigger("Shoot");
        Transform bulletProjectileTransform = Instantiate(_bulletProjectilePrefab, _shootPosition.position, Quaternion.identity);
        var bulletProjectile = bulletProjectileTransform.GetComponent<BulletProjectile>();
        Vector3 targetUnitShootAtPosition = e.targetUnit.GetWorldPosition();
        targetUnitShootAtPosition.y = _shootPosition.position.y;
        bulletProjectile.Setup(targetUnitShootAtPosition);
    }

    private void AddEvents()
    {
        if (TryGetComponent<MoveAction>(out var moveAction))
        {
            moveAction.OnStartMovingEvent += OnStartMoving;
            moveAction.OnStopMovingEvent += OnStopMoving;
        }

        if (TryGetComponent<ShootAction>(out var shootAction))
        {
            shootAction.OnShootEvent += OnShoot;
        }
    }


}
