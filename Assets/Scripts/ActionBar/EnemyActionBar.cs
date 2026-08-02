using System.Collections.Generic;
using System.Threading.Tasks;

public class EnemyActionBar : ActionBar
{
    public async Task InitializeTurn(List<CombatAction> upcomingActions)
    {
        ClearImmediateItems();

        if (upcomingActions == null || upcomingActions.Count == 0) return;

        for (var i = 0; i < upcomingActions.Count; i++)
        {
            CombatAction action = upcomingActions[i];
            AddAction(action);
            await Task.Delay(250);
        }
        SetActiveAction();
    }
}
