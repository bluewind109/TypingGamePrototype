using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SkillMenu : MonoBehaviour
{
    public Action onMenuOpened;
    public Action<CombatAction> onActionButtonPressed;
    public Action onBackPressed;

    [SerializeField] private SkillItemUI skillItemPrefab;
    [SerializeField] private ActionMenuInput input;
    
    private List<SkillItemUI> items = new List<SkillItemUI>();

    private int selectedIndex = 0;
    private int totalActions => items.Count;

    void Awake()
    {
        input = GetComponent<ActionMenuInput>();
    }

    public async Task Init(List<CombatAction> skills)
    {
        foreach (var skill in skills)
        {
            var item = Instantiate(skillItemPrefab, transform);
            item.SetData(skill);
            items.Add(item);
        }

        await Task.Yield(); // Wait a frame to ensure all UI elements are initialized
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
        onBackPressed?.Invoke();
    }

    private void OnActionSelect()
    {
        if (totalActions == 0) return;

        // Handle action selection based on the selectedIndex
        var selectedItem = items[selectedIndex];
        CombatAction action = selectedItem.ActionData;
        onActionButtonPressed?.Invoke(action);
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
