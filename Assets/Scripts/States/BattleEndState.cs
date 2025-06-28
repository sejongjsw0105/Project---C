public class BattleEndState : IBattleState
{
    public void Enter()
    {
        foreach (var unit in UnitManager.Instance.allUnits)
            unit.ToUnitData();
    }

    public void Exit() { }
    public void HandleActionClick(Action action) { }
    public void HandleUnitClick(Unit unit) { }
    public void HandleAreaClick(Area area) { }
}