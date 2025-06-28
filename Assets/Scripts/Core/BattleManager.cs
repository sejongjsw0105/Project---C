using UnityEngine;
using static UnityEditor.VersionControl.Asset;
GameEvents.OnUnitDied.Subscribe(unit =>
{
    BattleManager.Instance.CheckVictoryCondition();
});
public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    public Unit currentUnit;
    public Area currentArea;
    public Action currentAction;

    public int turnCount = 0;
    public int currentCommandPoints = 0;
    public int maxCommandPointsPerTurn = 3;
    public int maxTurnCount = 10;
    public BattleEndReason battleEndReason = BattleEndReason.None;

    public BattleStateMachine stateMachine;

    public enum BattleEndReason { None, TurnLimitReached, AllEnemiesDefeated, CommanderKilled, AllFriendliesDefeated }

    private void Awake()
    {
        Instance = this;
        stateMachine = new BattleStateMachine();
    }

    private void Start()
    {
        if (GameContext.Instance.nextEnemyPlan != null)
            GameContext.Instance.nextEnemyPlan.Spawn();

        stateMachine.SetState(new BattleStartState());
    }
    public void SetCurrentUnit(Unit unit)
    {
        currentUnit = unit;
    }
    public void SetCurrentArea(Area area)
    {
        currentArea = area;
    }
    public void SetCurrentAction(Action action)
    {
        currentAction = action;
    }
    public void ResetBattle()
    {
        UnitManager.Instance.ClearAllUnits();
        AreaManager.Instance.ClearAllAreas();
        currentUnit = null; // 현재 유닛 초기화    
        currentArea = null; // 현재 지역 초기화
        currentAction = Action.None; // 현재 행동 초기화
        stateMachine.SetState(new BattleStartState()); // 전투 시작 상태로 전환
    }
    public bool CanAffordCommandPoints(int amount)
    {
        if (currentCommandPoints >= amount)
        {
            return true;
        }
        return false;
    }
    public void SpendCommandPoints(int amount)
    {
        currentCommandPoints -= amount;
    }
    public void RefundCommandPoints(int amount)
    {
        currentCommandPoints += amount;
        if (currentCommandPoints > maxCommandPointsPerTurn)
        {
            currentCommandPoints = maxCommandPointsPerTurn; // 최대 전령 수 초과 방지
        }
    }
    public int GetActionCost(Action action)
    {
        return action switch
        {
            Action.None => 0,
            Action.Move => 1,
            Action.Defend => 1,
            Action.Support => 2,
            _ => 1
        };
    }
    public void CheckVictoryCondition()
    {
        bool anyFriendlyAlive = false;
        bool anyEnemyAlive = false;

        foreach (Unit unit in UnitManager.Instance.allUnits)
        {
            if (unit.currentHealth <= 0) continue;

            if (unit.faction == Faction.Friendly)
                anyFriendlyAlive = true;
            else if (unit.faction == Faction.Enemy)
                anyEnemyAlive = true;
        }

        if (!anyEnemyAlive)
        {
            battleEndReason = BattleEndReason.AllEnemiesDefeated;
            foreach (Unit unit in UnitManager.Instance.GetUnitsByFaction(Faction.Friendly))
            {
                unit.Win();
            }
            stateMachine.SetState(new BattleEndState());
            GameEvents.OnBattleEnded.Invoke(battleEndReason);

        }
        else if (!anyFriendlyAlive)
        {
            battleEndReason = BattleEndReason.AllFriendliesDefeated;
            foreach (Unit unit in UnitManager.Instance.GetUnitsByFaction(Faction.Friendly))
            {
                unit.Lose();
            }
            stateMachine.SetState(new BattleEndState());
            GameEvents.OnBattleEnded.Invoke(battleEndReason);
        }
        else if(turnCount >= maxTurnCount)
        {
            battleEndReason = BattleEndReason.TurnLimitReached;
            foreach (Unit unit in UnitManager.Instance.GetUnitsByFaction(Faction.Friendly))
            {
                unit.Lose();
            }
            stateMachine.SetState(new BattleEndState());
            GameEvents.OnBattleEnded.Invoke(battleEndReason);
            GameEvents.OnBattleEnded.Invoke(battleEndReason);
        }
    }
}

