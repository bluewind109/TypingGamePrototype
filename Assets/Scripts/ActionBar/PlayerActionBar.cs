
public class PlayerActionBar : ActionBar
{
    public void ClearActionBar()
    {
        foreach (ActionBarItem item in _actionItems)
        {
            _ = item.FadeOutAndDestroy();
        }
        _actionItems.Clear();
    }
}
