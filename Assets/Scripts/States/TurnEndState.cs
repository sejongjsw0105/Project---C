
public class TurnEndState : IBattleState
{
    public void Enter()
    {
        foreach (var unit in UnitManager.Instance.allUnits)
            unit.AfterTurnEnd();
        foreach (var area in AreaManager.Instance.allAreas)
            area.AfterTurnEnd();

        BattleManager.Instance.CheckVictoryCondition();
        BattleManager.Instance.turnCount++;

        if (BattleManager.Instance.turnCount > BattleManager.Instance.maxTurnCount)
        {
            BattleManager.Instance.battleEndReason = BattleManager.BattleEndReason.TurnLimitReached;
            BattleManager.Instance.stateMachine.SetState(new BattleEndState());
            return;
        }

        BattleManager.Instance.stateMachine.SetState(new EnemyTurnState());
    }

    public void Exit() { }
    public void HandleActionClick(Action action) { }
    public void HandleUnitClick(Unit unit) { }
    public void HandleAreaClick(Area area) { }
}
