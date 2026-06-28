
public class PlayerActionBar : ActionBar
{
    public void ClearActionBar()
    {
        foreach (ActionBarItem item in actionItems)
        {
            _ = item.FadeOutAndDestroy();
        }
        actionItems.Clear();
    }
}
