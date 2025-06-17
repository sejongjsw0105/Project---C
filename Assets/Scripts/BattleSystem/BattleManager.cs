using UnityEngine;

public class BattleManager : MonoBehaviour
{

    public static BattleManager Instance;

    public enum States { BattleStart, TurnStart, ActionSelection, UnitSelection, UnitSupport,UnitDefend, AreaSelection, UnitMove, Combat, TurnEnd, BattleEnd,  }    
    public enum BattleEndReason { None, TurnLimitReached, AllEnemiesDefeated, CommanderKilled , AllFriendliesDefeated }
    public BattleEndReason battleEndReason = BattleEndReason.None;
    public States currentState = States.TurnStart;
    public Unit currentUnit; // ���� ���õ� ����
    public Area currentArea; // ���� ���õ� ����
    public Action currentAction = Action.None; // ���� ���õ� �ൿ
    public Unit.Support currentSupport; // ���� ���õ� ���� �ൿ
    public int turnCount = 0; // ���� �� ��
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
        Debug.Log($"���� ����: {nextState}");

        switch (nextState)
        {
            case States.BattleStart:
                Debug.Log("���� ����");
                Debug.Log("Battle Start: Initializing Areas");
                turnCount = 0; // �� �� �ʱ�ȭ
                AreaManager.Instance.ClearAllAreas();
                AreaManager.Instance.InitializeArea();

                break;
            case States.TurnStart:

                turnCount++; // �� �� ����
                if (turnCount > maxTurnCount)
                {
                    battleEndReason = BattleEndReason.TurnLimitReached;
                    SetState(States.BattleEnd);
                    return;
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
                UnitMover.Instance.PrepareMove();
                UnitMover.Instance.MoveFriendlyUnitToArea(currentUnit, currentArea);
                break;
            case States.UnitSupport:
                Debug.Log("���� ���� �ൿ ó��");
                currentSupport = currentUnit.GetSupport();
                currentSupport.DoSupport(currentArea);
                CheckVictoryCondition();
                break;
            case States.UnitDefend:
                Debug.Log("���� ��� �ൿ ó��");
                currentUnit.Defense();
                break;
            case States.Combat:
                CombatManager.Instance.Combat();
                CheckVictoryCondition();
                break;
            case States.TurnEnd:
                foreach (Area area in AreaManager.Instance.allAreas)
                {
                    area.UpdateStatusEffects(); // ��� ������ ���� ȿ�� ������Ʈ
                }
                foreach (Unit unit in UnitManager.Instance.allUnits)
                {
                    unit.UpdateStatusEffects(); // ��� ������ ���� ȿ�� ������Ʈ
                }
                CheckVictoryCondition();
                SetState(States.TurnStart);
                break;
            case States.BattleEnd:
                break;
        }
    }
    public void SetCurrentUnit(Unit unit)
    {
        currentUnit = unit;
        Debug.Log($"���� ����: {unit.unitName}");
    }
    public void SetCurrentArea(Area area)
    {
        currentArea = area;
        Debug.Log($"���� ����: {area.areaIndexX}, {area.areaIndexY}");
    }
    public void SetCurrentAction(Action action)
    {
        currentAction = action;
        Debug.Log($"���� �ൿ: {action}");
    }
    public void ResetBattle()
    {
        UnitManager.Instance.ClearAllUnits();
        AreaManager.Instance.ClearAllAreas();
        currentState = States.BattleStart; // ���� ���� ���·� �ʱ�ȭ
        currentUnit = null; // ���� ���� �ʱ�ȭ    
        currentArea = null; // ���� ���� �ʱ�ȭ
        currentAction = Action.None; // ���� �ൿ �ʱ�ȭ

        SetState(States.BattleStart);
        Debug.Log("���� �ʱ�ȭ");
    }
    public bool TrySpendCommandPoints(int amount)
    {
        if (currentCommandPoints >= amount)
        {
            return true;
        }
        Debug.LogWarning($"���� ����! ({amount} �ʿ�, {currentCommandPoints} ����)");
        return false;
    }
    public void SpendCommandPoints(int amount)
    {
        currentCommandPoints -= amount;
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
            Debug.Log("���� ���� - �¸�!");
            SetState(States.BattleEnd);
        }
        else if (!anyFriendlyAlive)
        {
            battleEndReason = BattleEndReason.AllFriendliesDefeated;
            Debug.Log("�Ʊ� ���� - �й�...");
            SetState(States.BattleEnd);
        }
    }


}
