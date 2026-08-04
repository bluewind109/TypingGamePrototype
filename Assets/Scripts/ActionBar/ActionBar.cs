using UnityEngine;
using System.Threading.Tasks;
using System.Collections.Generic;

public class ActionBar : MonoBehaviour
{
    [SerializeField] private ActionBarItem actionItemPrefab;
    [SerializeField] private bool isReverseOrder = false; // If true, items will be added from right to left.

    private const int INITIAL_ITEM_IN_POOL = 6;
    private float _itemGap = 15f;
    private Stack<ActionBarItem> _actionItemPool = new Stack<ActionBarItem>();
    private List<ActionBarItem> _actionItems = new List<ActionBarItem>();

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

    private void ReleaseActionItemToPool(ActionBarItem item)
    {
        item.gameObject.SetActive(false);
        _actionItemPool.Push(item);
    }

    public void AddAction(CombatAction action)
    {
        ActionBarItem item = GetAvailableActionItem();
        item.Initialize(action.GetIcon());
        _actionItems.Add(item);
        UpdateItemPosition(item, _actionItems.Count - 1);
    }

    public void SetActiveAction()
    {
        if (_actionItems.Count == 0) return;
        _actionItems[0].SetActive(true);
    }

    private async Task RemoveCurrentActionBarItem()
    {
        if (_actionItems.Count == 0) return;

        ActionBarItem firstItem = _actionItems[0];
        _actionItems.RemoveAt(0);
        await firstItem.FadeOut();
        ReleaseActionItemToPool(firstItem);

        // Shift remaining items left.
        UpdateAllItemPositions();
    }

    private void UpdateAllItemPositions(bool immediate = false)
    {
        int index = 0;
        foreach (var item in _actionItems)
        {
            UpdateItemPosition(item, index++, immediate);
        }
    }

    private void UpdateItemPosition(ActionBarItem item, int index, bool immediate = false)
    {
        RectTransform itemRect = item.GetComponent<RectTransform>();
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

    private void ClearImmediateItems()
    {
        if (_actionItems.Count == 0) return;

        ActionBarItem item;
        while (_actionItems.Count > 0)
        {
            item = _actionItems[0];
            _actionItems.RemoveAt(0);
            ReleaseActionItemToPool(item);
        }
    }
}
