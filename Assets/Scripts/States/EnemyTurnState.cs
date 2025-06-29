using System.Linq;

public class EnemyTurnState : IBattleState
{
    public void Enter()
    {
        /*var snapshot = SimWorldSnapshot.Capture(UnitManager.Instance.allUnits.Cast<IUnit>().ToList(), AreaManager.Instance.allAreas.Cast<IArea>().ToList());
        var enemyActions = AITurnDecider.DecideBestEnemyAction(snapshot, enemyFactor, friendlyFactor, difficulty);

        foreach (var (unitId, actionPlan) in enemyActions)
        {
            var unit = UnitManager.Instance.allUnits.First(u => u.unitId == unitId);
            var area = actionPlan.targetArea as Area;
            switch (actionPlan.action)
            {
                case Action.Move: MoveAction.Execute(unit, area); break;
                case Action.Support: SupportAction.Execute(unit, area); break;
                case Action.Defend: DefendAction.Execute(unit, area); break;
            }
        }
        BattleManager.Instance.stateMachine.SetState(new TurnStartState());*/
    }

    public void Exit() { }
    public void HandleActionClick(Action action) { }
    public void HandleUnitClick(Unit unit) { }
    public void HandleAreaClick(Area area) { }
}
