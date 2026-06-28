using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ActionMenuInput))]
public class ActionMenu : MonoBehaviour
{
    public Action onMenuOpened;
    public Action<ActionButtonType> onActionButtonPressed;
    public Action onSkillPressed;

    [SerializeField] private ActionMenuInput input;
    private List<ActionItemUI> items = new List<ActionItemUI>();

    private int selectedIndex = 0;
    private int totalActions => items.Count;

    void Awake()
    {
        input = GetComponent<ActionMenuInput>();


        items = new List<ActionItemUI>(GetComponentsInChildren<ActionItemUI>());

        if (items.Count > 0)
        {
            UpdateSelection();
        }
    }

    void OnEnable()
    {
        input.onNavigateUp += OnNavigateUp;
        input.onNavigateDown += OnNavigateDown;
        input.onEnter += OnActionSelect;
        input.onBack += OnBack;
    }

    void OnDisable()
    {
        input.onNavigateUp -= OnNavigateUp;
        input.onNavigateDown -= OnNavigateDown;
        input.onEnter -= OnActionSelect;
        input.onBack -= OnBack;
    }

    void OnDestroy()
    {
        input.onNavigateUp -= OnNavigateUp;
        input.onNavigateDown -= OnNavigateDown;
        input.onEnter -= OnActionSelect;
        input.onBack -= OnBack;
    }

    public void ShowMenu()
    {
        this.gameObject.SetActive(true);
        input.ToggleInput(true);
        onMenuOpened?.Invoke();
    }

    public void HideMenu()
    {
        this.gameObject.SetActive(false);
        input.ToggleInput(false);
    }

    private void UpdateSelection()
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].ToggleActive(i == selectedIndex);
        }
    }

    #region Input Handling
    private void OnBack()
    {
        // HideMenu();
    }

    private void OnActionSelect()
    {
        if (totalActions == 0) return;

        // Handle action selection based on the selectedIndex
        var selectedItem = items[selectedIndex];

        switch (selectedItem.ButtonType)
        {
            case ActionButtonType.BasicAttack:
            case ActionButtonType.BasicDefend:
                // Debug.Log($"<color=green>{selectedItem.ButtonType}</color> selected");
                onActionButtonPressed?.Invoke(selectedItem.ButtonType);
                break;
            case ActionButtonType.Skill:
                // Debug.Log("<color=yellow>Skill</color> selected");
                onSkillPressed?.Invoke();
                break;
            default:
                Debug.Log("<color=red>Unknown action</color> selected");
                break;
        }
    }

    private void OnNavigateDown()
    {
        if (totalActions == 0) return;

        selectedIndex = (selectedIndex + 1) % totalActions;
        selectedIndex = Mathf.Clamp(selectedIndex, 0, totalActions - 1);
        UpdateSelection();
    }

    private void OnNavigateUp()
    {
        if (totalActions == 0) return;

        selectedIndex = (selectedIndex - 1 + totalActions) % totalActions;
        selectedIndex = Mathf.Clamp(selectedIndex, 0, totalActions - 1);
        UpdateSelection();
    }
    #endregion
}
