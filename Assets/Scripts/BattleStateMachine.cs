public class BattleStateMachine
{
    public IBattleState CurrentState { get; private set; }

    public void SetState(IBattleState nextState)
    {
        CurrentState?.Exit();
        CurrentState = nextState;
        CurrentState.Enter();
    }

    public void OnActionClick(Action action) => CurrentState?.HandleActionClick(action);
    public void OnUnitClick(Unit unit) => CurrentState?.HandleUnitClick(unit);
    public void OnAreaClick(Area area) => CurrentState?.HandleAreaClick(area);
}