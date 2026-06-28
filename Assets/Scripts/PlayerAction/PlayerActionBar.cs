using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TODO:
/// - Add a method to clear the action bar on turn end or when the player dies.
/// - Animate action items added in or removed from the bar.
/// </summary>

public class PlayerActionBar : MonoBehaviour
{
    [SerializeField] private ActionBarItem actionItemPrefab;
    [SerializeField] private float itemGap = 15f;

    private List<ActionBarItem> actionItems = new List<ActionBarItem>();

    public void AddAction(CombatAction action)
    {
        ActionBarItem item = Instantiate(actionItemPrefab, transform);
        item.Initialize(action.GetIcon());
        actionItems.Add(item);
        UpdateItemPosition(actionItems.Count - 1);
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

    public void ClearActionBar()
    {
        foreach (ActionBarItem item in actionItems)
        {
            _ = item.FadeOutAndDestroy();
        }
        actionItems.Clear();
    }

    private void UpdateItemPosition(int index)
    {
        RectTransform itemRect = actionItems[index].GetComponent<RectTransform>();
        itemRect.anchoredPosition = new Vector2(index * (itemRect.sizeDelta.x + itemGap) * 1, 0);
    }
}
