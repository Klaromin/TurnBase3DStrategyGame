using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject _actionCameraGameObject;

    private void Start()
    {
        AddEvents();
    }

    private void ShowActionCamera()
    {
        _actionCameraGameObject.SetActive(true);
    }

    private void HideActionCamera()
    {
        _actionCameraGameObject.SetActive(false);
    }

    private void OnAnyActionStarted(object sender, EventArgs e)
    {
        switch (sender)
        {
            case ShootAction shootAction:
                Unit shooterUnit = shootAction.GetUnit();
                Unit targetUnit = shootAction.GetTargetUnit();
                
                Vector3 cameraCharacterHeight = Vector3.up * 1.7f;
                Vector3 shootDir = (targetUnit.GetWorldPosition() - shooterUnit.GetWorldPosition()).normalized;
                float shoulderOffsetAmount = 0.5f;
                Vector3 shoulderOffset = Quaternion.Euler(0, 90, 0) * shootDir * shoulderOffsetAmount;
                Vector3 actionCameraPosition = shooterUnit.GetWorldPosition() + cameraCharacterHeight + shoulderOffset + (shootDir * -2f);

                _actionCameraGameObject.transform.position = actionCameraPosition;
                _actionCameraGameObject.transform.LookAt(targetUnit.GetWorldPosition() +cameraCharacterHeight);
                ShowActionCamera();
                break;
        }
    }
    
    private void OnAnyActionCompleted(object sender, EventArgs e)
    {
        switch (sender)
        {
            case ShootAction shootAction:
                HideActionCamera();
                break;
        }
    }
    
    private void AddEvents()
    {
        BaseAction.OnAnyActionStarted += OnAnyActionStarted;
        BaseAction.OnAnyActionCompleted += OnAnyActionCompleted;
    }

    private void RemoveEvents()
    {
        BaseAction.OnAnyActionStarted -= OnAnyActionStarted;
        BaseAction.OnAnyActionCompleted -= OnAnyActionCompleted;
    }
}
