public interface IBattleState
{
    void Enter();
    void Exit();

    void HandleActionClick(Action action);
    void HandleUnitClick(Unit unit);
    void HandleAreaClick(Area area);
}
