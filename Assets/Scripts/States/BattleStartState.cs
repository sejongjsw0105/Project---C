public class BattleStartState : IBattleState
{
    public void Enter()
    {
        BattleManager.Instance.turnCount = 0; // �� �� �ʱ�ȭ
        AreaManager.Instance.BeginBattle(); // ���� �ʱ�ȭ
        foreach (Unit unit in UnitManager.Instance.allUnits)
        {
            unit.BeginBattle();
        }
        BattleManager.Instance.stateMachine.SetState(new TurnStartState());
    }

    public void Exit() { }
    public void HandleActionClick(Action action) { }
    public void HandleUnitClick(Unit unit) { }
    public void HandleAreaClick(Area area) { }
}
