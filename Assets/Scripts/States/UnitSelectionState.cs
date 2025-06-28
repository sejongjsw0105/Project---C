public class UnitSelectionState : IBattleState
{
    public void Enter() { }
    public void Exit() { }

    public void HandleUnitClick(Unit unit)
    {
        if (BattleManager.Instance.currentAction == Action.Support && unit.GetSupport() == null)
            return;

        BattleManager.Instance.SetCurrentUnit(unit);

        if (BattleManager.Instance.currentAction == Action.Defend)
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