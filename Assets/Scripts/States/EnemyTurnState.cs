public class EnemyTurnState : IBattleState
{
    public void Enter()
    {
        //BattleManager.Instance.StartCoroutine(EnemyAIUtility.ExecuteEnemyTurn());
    }

    public void Exit() { }
    public void HandleActionClick(Action action) { }
    public void HandleUnitClick(Unit unit) { }
    public void HandleAreaClick(Area area) { }
}
