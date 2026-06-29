using UnityEngine;
using System.Threading.Tasks;
using System.Collections.Generic;

public class ActionBar : MonoBehaviour
{
    [SerializeField] protected ActionBarItem actionItemPrefab;
    [SerializeField] protected float itemGap = 15f;
    [SerializeField] protected bool isReverseOrder = false; // If true, items will be added from right to left.

    protected List<ActionBarItem> actionItems = new List<ActionBarItem>();
    public bool IsPatternFinished() => actionItems.Count == 0;

    public virtual void AddAction(CombatAction action)
    {
        ActionBarItem item = Instantiate(actionItemPrefab, transform);
        item.Initialize(action.GetIcon());
        actionItems.Add(item);
        UpdateItemPosition(actionItems.Count - 1, true);
    }

    public virtual void SetActiveItem()
    {
        if (actionItems.Count == 0) return;
        actionItems[0].SetActive(true);
    }

    // Remove the first item in the bar (the one that was just executed) 
    // and shift the remaining items left.
    public virtual void OnActionExecuted()
    {
        // Destroy the first item (the one that was just executed).
        _ = RemoveCurrentActionBarItem();

        if (IsPatternFinished())
        {
            return;
        }

        SetActiveItem();
    }

    protected virtual async Task RemoveCurrentActionBarItem()
    {
        if (actionItems.Count == 0) return;

        ActionBarItem firstItem = actionItems[0];
        await firstItem.FadeOutAndDestroy();
        actionItems.RemoveAt(0);

        // Shift remaining items left.
        for (var i = 0; i < actionItems.Count; i++)
        {
            UpdateItemPosition(i);
        }
    }

    protected virtual void UpdateItemPosition(int index, bool immediate = false)
    {
        RectTransform itemRect = actionItems[index].GetComponent<RectTransform>();
        ActionBarItem item = actionItems[index];
        float direction = isReverseOrder ? -1 : 1;
        Vector2 targetPosition = new Vector2(index * (itemRect.sizeDelta.x + itemGap) * direction, 0);

        if (immediate)
        {
            item.SetTargetPosition(targetPosition);
            itemRect.anchoredPosition = targetPosition;
            return;
        }
        item.SetTargetPosition(targetPosition);
    }

    protected virtual void ClearImmediateItems()
    {
        if (actionItems.Count == 0) return;

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
