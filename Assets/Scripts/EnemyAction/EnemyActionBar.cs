using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyActionBar : MonoBehaviour
{    
    [SerializeField] private ActionBarItem actionItemPrefab;
    [SerializeField] private float itemGap = 15f;

    private List<ActionBarItem> actionItems = new List<ActionBarItem>();

    public async Task InitializeTurn(List<CombatAction> upcomingActions)
    {
        ClearImmediateItems();

        if (upcomingActions == null || upcomingActions.Count == 0) return;

        for (var i = 0; i < upcomingActions.Count; i++)
        {
            CombatAction action = upcomingActions[i];
            ActionBarItem item = Instantiate(actionItemPrefab, transform);
            await Task.Yield(); // Wait a frame to ensure the item is properly initialized before setting its icon.
            item.Initialize(action.GetIcon());
            actionItems.Add(item);
            // Position the item based on its index and the gap.
            UpdateItemPosition(i);
            await Task.Delay(250);
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
        if (actionItems.Count == 0) return;
        Debug.Log($"[EnemyActionBar] OnActionExecuted");

        // Destroy the first item (the one that was just executed).
        ActionBarItem firstItem = actionItems[0];
        _ = firstItem.FadeOutAndDestroy();
        actionItems.RemoveAt(0);

        if (IsPatternFinished())
        {
            return;
        }

        // Shift remaining items left.
        for (var i = 0; i < actionItems.Count; i++)
        {
            UpdateItemPosition(i);
        }
        SetActiveItem();
    }

    private void UpdateItemPosition(int index)
    {
        // Debug.Log($"Updating position for item {index}");
        RectTransform itemRect = actionItems[index].GetComponent<RectTransform>();
        itemRect.anchoredPosition = new Vector2(index * (itemRect.sizeDelta.x + itemGap) * -1, 0);
    }

    private void ClearImmediateItems()
    {
        for (int i = 0; i < actionItems.Count; i++)
        {
            if (actionItems[i] != null)
            {
                Destroy(actionItems[i].gameObject);
            }
        }

        actionItems.Clear();
    }
}
