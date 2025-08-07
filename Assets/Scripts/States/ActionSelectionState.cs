public class ActionSelectionState : IBattleState
{
    public void Enter()
    {
        BattleManager.Selection.SetAction(Action.None);
    }

    public void Exit() { }

    public void HandleActionClick(Action action)
    {
        if (action == Action.None)
            return;

        BattleManager.Instance.SetCurrentAction(action);
        BattleManager.Instance.stateMachine.SetState(new UnitSelectionState());
    }

    public void HandleUnitClick(Unit unit) { }
    public void HandleAreaClick(Area area) { }
}
