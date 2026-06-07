using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ActionMenuInput))]
public class ActionMenu : MonoBehaviour
{
    public Action<ActionButtonType> onActionButtonPressed;

    private ActionMenuInput input;
    private List<ActionItemUI> actionItems = new List<ActionItemUI>();

    private int selectedIndex = 0;
    private int totalActions => actionItems.Count;

    void Awake()
    {
        input = GetComponent<ActionMenuInput>();
        input.onNavigateUp += OnNavigateUp;
        input.onNavigateDown += OnNavigateDown;
        input.onEnter += OnActionSelect;
        input.onBack += OnBack;

        actionItems = new List<ActionItemUI>(GetComponentsInChildren<ActionItemUI>());

        if (actionItems.Count > 0)
        {
            UpdateSelection();
        }

        ShowMenu();
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
    }

    public void HideMenu()
    {
        this.gameObject.SetActive(false);
    }

    private void UpdateSelection()
    {
        for (int i = 0; i < actionItems.Count; i++)
        {
            actionItems[i].ToggleActive(i == selectedIndex);
        }
    }

    #region Input Handling
    private void OnBack()
    {
        HideMenu();
    }

    private void OnActionSelect()
    {
        if (totalActions == 0) return;

        // Handle action selection based on the selectedIndex
        Debug.Log("Action selected: " + selectedIndex);
        var actionItem = actionItems[selectedIndex];

        switch (actionItem.ButtonType)
        {
            case ActionButtonType.BasicAttack:
                Debug.Log("<color=green>Basic Attack</color> selected");
                onActionButtonPressed?.Invoke(actionItem.ButtonType);
                break;
            case ActionButtonType.BasicDefend:
                Debug.Log("<color=blue>Basic Defend</color> selected");
                onActionButtonPressed?.Invoke(actionItem.ButtonType);
                break;
            case ActionButtonType.Skill:
                Debug.Log("<color=yellow>Skill</color> selected");
                onActionButtonPressed?.Invoke(actionItem.ButtonType);
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
