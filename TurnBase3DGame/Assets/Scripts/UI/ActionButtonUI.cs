using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ActionButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _tmPro;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject selectedGameObject;
    private BaseAction _baseAction;

    public void SetBaseAction(BaseAction baseAction)
    {
        _baseAction = baseAction;
        _tmPro.text = baseAction.GetActionName().ToUpper();
        _button.onClick.AddListener((() =>
        {
            UnitActionSystem.Instance.SetSelectedAction(baseAction);
        }));
    }

    public void SetBaseSprite(BaseAction baseAction)
    {
        _button.image.sprite = baseAction.GetActionSprite();
    }

    public void UpdateSelectedVisual()
    {
        BaseAction selectedBaseAction = UnitActionSystem.Instance.GetSelectedAction();
        selectedGameObject.SetActive(selectedBaseAction == _baseAction );
    }
}
