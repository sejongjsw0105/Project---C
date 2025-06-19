using UnityEngine;

public class BattleManager : MonoBehaviour
{

    public static BattleManager Instance;

    public enum States { BattleStart, TurnStart, ActionSelection, UnitSelection, UnitSupport,UnitDefend, AreaSelection, UnitMove, Combat, TurnEnd, BattleEnd,  }    
    public enum BattleEndReason { None, TurnLimitReached, AllEnemiesDefeated, CommanderKilled , AllFriendliesDefeated }
    public BattleEndReason battleEndReason = BattleEndReason.None;
    public States currentState = States.TurnStart;
    public Unit currentUnit; // 현재 선택된 유닛
    public Area currentArea; // 현재 선택된 지역
    public Action currentAction = Action.None; // 현재 선택된 행동
    public Unit.Support currentSupport; // 현재 선택된 지원 행동
    public int turnCount = 0; // 현재 턴 수
    public int currentCommandPoints = 0;
    public int maxCommandPointsPerTurn = 3;
    public int maxTurnCount = 10;

    private void Awake() => Instance = this;

    private void Start()
    {
    }

    public void SetState(States nextState)
    {
        currentState = nextState;
        Debug.Log($"상태 전이: {nextState}");

        switch (nextState)
        {
            case States.BattleStart:
                Debug.Log("전투 시작");
                Debug.Log("Battle Start: Initializing Areas");
                turnCount = 0; // 턴 수 초기화
                AreaManager.Instance.ClearAllAreas();
                AreaManager.Instance.InitializeArea();
                foreach(Unit unit in UnitManager.Instance.allUnits){
                    unit.BeginBattle();
                }

                break;
            case States.TurnStart:
                foreach (Unit unit in UnitManager.Instance.allUnits)
                {
                    unit.BeginTurn(); // 각 유닛의 턴 시작 처리
                }
                currentCommandPoints = maxCommandPointsPerTurn;
                break;
            case States.ActionSelection:
                ActionSelector.Instance.BeginSelection();
                break;
            case States.UnitSelection:
                UnitSelector.Instance.BeginSelection();
                break;
            case States.AreaSelection:
                AreaSelector.Instance.BeginSelection();
                break;
            case States.UnitMove:
                currentUnit.DoMove(currentArea); // 현재 유닛을 선택된 지역으로 이동
                break;
            case States.UnitSupport:
                Debug.Log("유닛 지원 행동 처리");
                currentSupport = currentUnit.GetSupport();
                currentSupport.DoSupport(currentArea);
                CheckVictoryCondition();
                break;
            case States.UnitDefend:
                Debug.Log("유닛 방어 행동 처리");
                currentUnit.Defense();
                break;
            case States.Combat:
                CombatManager.Instance.Combat();
                CheckVictoryCondition();
                break;
            case States.TurnEnd:
                foreach (Area area in AreaManager.Instance.allAreas)
                {
                    area.UpdateStatusEffects(); // 모든 영역의 상태 효과 업데이트
                }
                foreach (Unit unit in UnitManager.Instance.allUnits)
                {
                    unit.UpdateStatusEffects(); // 모든 유닛의 상태 효과 업데이트
                }
                CheckVictoryCondition();
                turnCount++; // 턴 수 증가
                if (turnCount > maxTurnCount)
                {
                    battleEndReason = BattleEndReason.TurnLimitReached;
                    SetState(States.BattleEnd);
                    return;
                }
                SetState(States.TurnStart);
                break;
            case States.BattleEnd:
                foreach (Unit unit in UnitManager.Instance.allUnits)
                {
                    unit.stats.currentHealth = unit.health; // 전투 종료 시 유닛의 현재 체력을 영구 정보에 저장
                }
                break;
        }
    }
    public void SetCurrentUnit(Unit unit)
    {
        currentUnit = unit;
        Debug.Log($"현재 유닛: {unit.unitName}");
    }
    public void SetCurrentArea(Area area)
    {
        currentArea = area;
        Debug.Log($"현재 지역: {area.areaIndexX}, {area.areaIndexY}");
    }
    public void SetCurrentAction(Action action)
    {
        currentAction = action;
        Debug.Log($"현재 행동: {action}");
    }
    public void ResetBattle()
    {
        UnitManager.Instance.ClearAllUnits();
        AreaManager.Instance.ClearAllAreas();
        currentState = States.BattleStart; // 전투 시작 상태로 초기화
        currentUnit = null; // 현재 유닛 초기화    
        currentArea = null; // 현재 지역 초기화
        currentAction = Action.None; // 현재 행동 초기화

        SetState(States.BattleStart);
        Debug.Log("전투 초기화");
    }
    public bool TrySpendCommandPoints(int amount)
    {
        if (currentCommandPoints >= amount)
        {
            return true;
        }
        Debug.LogWarning($"전령 부족! ({amount} 필요, {currentCommandPoints} 보유)");
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
            if (unit.health <= 0) continue;

            if (unit.faction == Unit.Faction.Friendly)
                anyFriendlyAlive = true;
            else if (unit.faction == Unit.Faction.Enemy)
                anyEnemyAlive = true;
        }

        if (!anyEnemyAlive)
        {
            battleEndReason = BattleEndReason.AllEnemiesDefeated;
            Debug.Log("적군 전멸 - 승리!");
            SetState(States.BattleEnd);
        }
        else if (!anyFriendlyAlive)
        {
            battleEndReason = BattleEndReason.AllFriendliesDefeated;
            Debug.Log("아군 전멸 - 패배...");
            SetState(States.BattleEnd);
        }
    }


}
