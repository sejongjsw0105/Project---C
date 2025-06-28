public class AreaSelectionState : IBattleState
{
    public void Enter() { }
    public void Exit() { }

    public void HandleAreaClick(Area area)
    {
        BattleManager.Instance.SetCurrentArea(area);
        switch (BattleManager.Instance.currentAction)
        {
            case Action.Move:
                BattleManager.Instance.stateMachine.SetState(new MoveState());
                break;
            case Action.Support:
                BattleManager.Instance.stateMachine.SetState(new SupportState());
                break;
        }
    }

    public void HandleActionClick(Action action) { }
    public void HandleUnitClick(Unit unit) { }
}