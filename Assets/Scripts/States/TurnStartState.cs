public class TurnStartState : IBattleState
{
    public void Enter()
    {
        foreach (var unit in UnitManager.Instance.allUnits)
            unit.BeforeTurnStart();

        BattleManager.Instance.currentCommandPoints = BattleManager.Instance.maxCommandPointsPerTurn;
        BattleManager.Instance.stateMachine.SetState(new ActionSelectionState());
    }

    public void Exit() { }
    public void HandleActionClick(Action action) { }
    public void HandleUnitClick(Unit unit) { }
    public void HandleAreaClick(Area area) { }
}