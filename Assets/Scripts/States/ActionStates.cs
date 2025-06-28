
public class MoveState : IBattleState
{
    public void Enter()
    {
        MoveAction.Execute(BattleManager.Instance.currentUnit, BattleManager.Instance.currentArea);
        BattleManager.Instance.SpendCommandPoints(BattleManager.Instance.GetActionCost(Action.Move));
        BattleManager.Instance.stateMachine.SetState(new ActionSelectionState());
    }

    public void Exit() { }
    public void HandleActionClick(Action action) { }
    public void HandleUnitClick(Unit unit) { }
    public void HandleAreaClick(Area area) { }
}

public class SupportState : IBattleState
{
    public void Enter()
    {
        SupportAction.Execute(BattleManager.Instance.currentUnit, BattleManager.Instance.currentArea);
        BattleManager.Instance.SpendCommandPoints(BattleManager.Instance.GetActionCost(Action.Support));
        BattleManager.Instance.CheckVictoryCondition();
        BattleManager.Instance.stateMachine.SetState(new ActionSelectionState());
    }

    public void Exit() { }
    public void HandleActionClick(Action action) { }
    public void HandleUnitClick(Unit unit) { }
    public void HandleAreaClick(Area area) { }
}

public class DefendState : IBattleState
{
    public void Enter()
    {
        DefendAction.Execute(BattleManager.Instance.currentUnit, BattleManager.Instance.currentArea);
        BattleManager.Instance.SpendCommandPoints(BattleManager.Instance.GetActionCost(Action.Defend));
        BattleManager.Instance.stateMachine.SetState(new ActionSelectionState());
    }

    public void Exit() { }
    public void HandleActionClick(Action action) { }
    public void HandleUnitClick(Unit unit) { }
    public void HandleAreaClick(Area area) { }
}

public class CombatState : IBattleState
{
    public void Enter()
    {
        CombatManager.Instance.Combat();
        BattleManager.Instance.CheckVictoryCondition();
        BattleManager.Instance.stateMachine.SetState(new TurnEndState());
    }

    public void Exit() { }
    public void HandleActionClick(Action action) { }
    public void HandleUnitClick(Unit unit) { }
    public void HandleAreaClick(Area area) { }
}