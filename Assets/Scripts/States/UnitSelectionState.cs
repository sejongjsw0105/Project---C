public class UnitSelectionState : IBattleState
{
    public void Enter() { }
    public void Exit() { }

    public void HandleUnitClick(Unit unit)
    {
        var action = BattleManager.Instance.currentAction;
        BattleManager.Instance.SetCurrentUnit(unit);

        if (action == Action.Support)
        {
            BattleManager.Instance.stateMachine.SetState(new AreaSelectionState());
        }
        else if (action == Action.Defend)
        {
            BattleManager.Instance.stateMachine.SetState(new DefendState());
        }
        else
        {
            BattleManager.Instance.stateMachine.SetState(new AreaSelectionState());
        }
    }

    public void HandleActionClick(Action action) { }
    public void HandleAreaClick(Area area) { }
}