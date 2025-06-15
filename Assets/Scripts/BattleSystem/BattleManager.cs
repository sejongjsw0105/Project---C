using UnityEngine;

public class BattleManager : MonoBehaviour
{

    public static BattleManager Instance;

    public enum States { BattleStart, TurnStart, ActionSelection, UnitSelection, UnitAction, AreaSelection, UnitMove, Combat, TurnEnd, BattleEnd }
    public States currentState = States.TurnStart;
    public Unit currentUnit; // 현재 선택된 유닛
    public Area currentArea; // 현재 선택된 지역
    public Action currentAction = Action.None; // 현재 선택된 행동

    private void Awake() => Instance = this;

    private void Start()
    {
        TurnManager.Instance.StartTurn();
    }

    public void SetState(States nextState)
    {
        currentState = nextState;
        Debug.Log($"상태 전이: {nextState}");

        switch (nextState)
        {
            case States.BattleStart:
                Debug.Log("전투 시작");
                break;
            case States.TurnStart:
                TurnManager.Instance.StartTurn();
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
                break;
            case States.UnitAction:
                break;
            case States.Combat:
                break;
            case States.TurnEnd:
                TurnManager.Instance.EndTurn();
                break;
            case States.BattleEnd:
                break;
        }
    }

}
