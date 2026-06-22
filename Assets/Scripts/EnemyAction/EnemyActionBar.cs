using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyActionBar : MonoBehaviour
{    
    [SerializeField] private EnemyActionItem actionItemPrefab;
    [SerializeField] private float itemGap = 15f;

    private ActionConfig config;
    private List<EnemyActionItem> actionItems = new List<EnemyActionItem>();

    public void Initialize(ActionConfig actionConfig)
    {
        config = actionConfig;
    }

    public async Task InitializeTurn(List<CombatAction> upcomingActions)
    {
        for (var i = 0; i < upcomingActions.Count; i++)
        {
            CombatAction action = upcomingActions[i];
            EnemyActionItem item = Instantiate(actionItemPrefab, transform);
            await Task.Yield(); // Wait a frame to ensure the item is properly initialized before setting its icon.
            item.Initialize(action.GetIcon());
            // Position the item based on its index and the gap.
            UpdateItemPosition(i);
            actionItems.Add(item);
        }
        SetActiveItem();
    }

    public void SetActiveItem()
    {
        if (actionItems.Count > 0)
        {
            actionItems[0].SetActive(true);
        }
    }

    public bool IsPatternFinished()
    {
        return actionItems.Count == 0;
    }

    // Remove the first item in the bar (the one that was just executed) and shift the remaining items left.
    public void OnActionExecuted()
    {
        if (transform.childCount == 0) return;

        // Destroy the first item (the one that was just executed).
        Destroy(transform.GetChild(0).gameObject);
        actionItems.RemoveAt(0);

        if (IsPatternFinished())
        {
            return;
        }

        // Shift remaining items left.
        for (var i = 0; i < transform.childCount; i++)
        {
            UpdateItemPosition(i);
        }
        SetActiveItem();
    }

    private void UpdateItemPosition(int index)
    {
        // Debug.Log($"Updating position for item {index}");
        RectTransform itemRect = transform.GetChild(index).GetComponent<RectTransform>();
        itemRect.anchoredPosition = new Vector2(index * (itemRect.sizeDelta.x + itemGap) * -1, 0);
    }
}
