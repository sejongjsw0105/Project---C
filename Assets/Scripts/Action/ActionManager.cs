using UnityEngine;

public class ActionManager : MonoBehaviour
{
    public static ActionManager Instance;

    private void Awake() => Instance = this;

    public void Start()
    {
        // 초기 상태 설정
        BattleManager.Instance.SetCurrentAction(Action.None);
    }

    public void ProcessAction(Unit selectedUnit)
    {
        switch (BattleManager.Instance.currentAction)
        {
            case Action.Move:
                BattleManager.Instance.SetState(BattleManager.States.UnitSelection);
                break;
            case Action.Support:
                BattleManager.Instance.SetState(BattleManager.States.UnitSelection);
                break;

            case Action.Defend:
                BattleManager.Instance.SetState(BattleManager.States.UnitSelection);
                break;
        }
    }

    public void CancelAction()
    {
        BattleManager.Instance.currentAction = Action.None;
        Debug.Log("행동 선택 취소");
        BattleManager.Instance.SetState(BattleManager.States.ActionSelection);
    }
}
