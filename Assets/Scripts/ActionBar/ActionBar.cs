using UnityEngine;
using System.Threading.Tasks;
using System.Collections.Generic;

public class ActionBar : MonoBehaviour
{
    [SerializeField] protected ActionBarItem actionItemPrefab;
    [SerializeField] protected bool isReverseOrder = false; // If true, items will be added from right to left.

    private const int INITIAL_ITEM_IN_POOL = 6;
    private float _itemGap = 15f;
    protected Stack<ActionBarItem> _actionItemPool = new Stack<ActionBarItem>();

    protected List<ActionBarItem> _actionItems = new List<ActionBarItem>();

    void Awake()
    {
        // Pre-instantiate a pool of ActionBarItems to avoid runtime instantiation overhead.
        for (int i = 0; i < INITIAL_ITEM_IN_POOL; i++)
        {
            ActionBarItem item = Instantiate(actionItemPrefab, transform);
            item.gameObject.SetActive(false);
            _actionItemPool.Push(item);
        }
    }

    private ActionBarItem GetAvailableActionItem()
    {
        if (_actionItemPool.Count > 0)
        {
            ActionBarItem item = _actionItemPool.Pop();
            item.gameObject.SetActive(true);
            return item;
        }
        else
        {
            ActionBarItem item = Instantiate(actionItemPrefab, transform);
            item.gameObject.SetActive(true);
            return item;
        }
    }

    public virtual void AddAction(CombatAction action)
    {
        ActionBarItem item = GetAvailableActionItem();
        item.Initialize(action.GetIcon());
        _actionItems.Add(item);
        UpdateItemPosition(_actionItems.Count - 1, true);
    }

    public void SetActiveAction(int index = 0)
    {
        if (_actionItems.Count == 0) return;
        _actionItems[index].SetActive(true);
    }

    protected virtual async Task RemoveCurrentActionBarItem()
    {
        if (_actionItems.Count == 0) return;

        ActionBarItem firstItem = _actionItems[0];
        await firstItem.FadeOutAndDestroy();
        _actionItems.RemoveAt(0);
        ReleaseActionItemToPool(firstItem);

        // Shift remaining items left.
        for (var i = 0; i < _actionItems.Count; i++)
        {
            UpdateItemPosition(i);
        }
    }

    private void ReleaseActionItemToPool(ActionBarItem item)
    {
        item.gameObject.SetActive(false);
        _actionItemPool.Push(item);
    }

    protected virtual void UpdateItemPosition(int index, bool immediate = false)
    {
        RectTransform itemRect = _actionItems[index].GetComponent<RectTransform>();
        ActionBarItem item = _actionItems[index];
        float direction = isReverseOrder ? -1 : 1;
        Vector2 targetPosition = new Vector2(index * (itemRect.sizeDelta.x + _itemGap) * direction, 0);

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
        if (_actionItems.Count == 0) return;

        for (int i = 0; i < _actionItems.Count; i++)
        {
            if (_actionItems[i] != null)
            {
                Destroy(_actionItems[i].gameObject);
            }
        }

        _actionItems.Clear();
    }
}
