using System;
using UnityEngine;

public class ActionBusyUI : MonoBehaviour
{

    private void Start()
    {
        AddEvents();
        Hide();
    }

    private void Show()
    {
        gameObject.SetActive(true);   
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnBusyChanged(object sender, bool isBusy)
    {
        if (isBusy)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void AddEvents()
    {
        UnitActionSystem.Instance.OnBusyChangedEvent += OnBusyChanged;
    }

    private void RemoveEvents()
    {
        UnitActionSystem.Instance.OnBusyChangedEvent -= OnBusyChanged;
    }
}