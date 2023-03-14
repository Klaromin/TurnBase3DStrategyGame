using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitActionSystem : MonoBehaviour
{

    public static UnitActionSystem Instance { get; private set; }

    public event EventHandler OnSelectedUnitChangedEvent;
    public event EventHandler OnSelectedActionChangedEvent;
    public event EventHandler<bool> OnBusyChangedEvent;
    public event EventHandler OnActionStartedEvent;


    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitLayerMask;

    private BaseAction _selectedAction;
    private bool _isBusy;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one UnitActionSystem! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
    }
    // 1-) Run order yüzünden selected unit Start'ta düşmüyordu o yüzden yukarıda awakete üniti seçmeye koydum. Kesin çözüm olduğunu hissedince startı sil.
    // 2-) script execution orderda grid system visualı bir sonraya koyarak durumu çözdüm.
    private void Start()
    {
        SetSelectedUnit(selectedUnit);
        // SetSelectedUnit(selectedUnit);
    }

    private void Update()
    {
        if (_isBusy)
        {
            return;
        }

        if (!TurnSystem.Instance.IsPlayerTurn())
        {
            return;
        }

        if (EventSystem.current.IsPointerOverGameObject()) //UI ve Valid Grid pozisyonu üst üste gelince mouse ile tıklayınca karakterin uı altındaki gride gitmesini engelliyor.
        {
            return;
        }

        if (TryHandleUnitSelection())
        {
            return;
        }
        HandleSelectedAction();
    }

    private void HandleSelectedAction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());
            if (_selectedAction.IsValidActionGridPosition(mouseGridPosition))
            {
                if (selectedUnit.TrySpendActionPointsToTakeAction(_selectedAction))
                {
                    SetBusy();
                    _selectedAction.TakeAction(mouseGridPosition, ClearBusy);
                    OnActionStartedEvent?.Invoke(this, EventArgs.Empty);
                }

            }
            
        }
    }

    private void SetBusy()
    {
        _isBusy = true;
        OnBusyChangedEvent.Invoke(this, _isBusy);
    }

    private void ClearBusy()
    {
        _isBusy = false;
        OnBusyChangedEvent.Invoke(this, _isBusy);
    }

    private bool TryHandleUnitSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitLayerMask))
            {
                if (raycastHit.transform.TryGetComponent<Unit>(out Unit unit))
                {
                    if (unit == selectedUnit)
                    {
                        //already selected
                        return false;
                    }

                    if (unit.IsEnemy())
                    {
                        //clicked on an enemy.
                        return false;
                    }
                    SetSelectedUnit(unit);
                    return true;
                }
            }
        }
        return false;
    }

    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;
        SetSelectedAction(unit.GetMoveAction());
        OnSelectedUnitChangedEvent?.Invoke(this, EventArgs.Empty);
    }

    public void SetSelectedAction(BaseAction baseAction)
    {
        _selectedAction = baseAction;
        OnSelectedActionChangedEvent?.Invoke(this,EventArgs.Empty);
    }

    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }

    public BaseAction GetSelectedAction()
    {
        return _selectedAction;
    }

}