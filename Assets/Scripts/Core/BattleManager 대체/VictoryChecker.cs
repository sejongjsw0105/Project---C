using System.Linq;

public class VictoryChecker
{
    public void CheckVictoryCondition()
    {
        bool anyFriendlyAlive = UnitManager.Instance.allUnits.Any(u => u.currentHealth > 0 && u.faction == Faction.Friendly);
        bool anyEnemyAlive = UnitManager.Instance.allUnits.Any(u => u.currentHealth > 0 && u.faction == Faction.Enemy);

        if (!anyEnemyAlive)
        {
            BattleManager.Instance.VictoryEnd(BattleManager.BattleEndReason.AllEnemiesDefeated);
        }
        else if (!anyFriendlyAlive)
        {
            BattleManager.Instance.VictoryEnd(BattleManager.BattleEndReason.AllFriendliesDefeated);
        }
        else if (BattleManager.Instance.StateMachine.CurrentState is TurnEndState &&
                 BattleManager.Instance.CommandPoints.Current >= BattleManager.Instance.CommandPoints.Max)
        {
            BattleManager.Instance.VictoryEnd(BattleManager.BattleEndReason.TurnLimitReached);
        }
    }
}
