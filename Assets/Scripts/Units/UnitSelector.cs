using UnityEngine;

public class UnitSelector : MonoBehaviour
{
    public static UnitSelector Instance;
    private void Awake() => Instance = this;

    public void BeginSelection()
    {
        Debug.Log("유닛 선택 대기 중");
        // UI 표시 또는 입력 대기 처리
    }

    public void OnUnitClicked(Unit unit)
    {
        switch(BattleManager.Instance.currentState)
        {
            case BattleManager.States.UnitSelection:
                // 이미 유닛이 선택된 상태에서 다시 클릭하면 선택 해제
                if (BattleManager.Instance.currentUnit == unit)
                {
                    CancelSelection();
                    return;
                }
                else
                {
                    BattleManager.Instance.currentUnit = unit;
                    Debug.Log($"{unit.unitName} 선택됨");
                    ProcessAction();
                }
                break;
            default:
                //UI로 유닛 정보 띄우기
                Debug.LogWarning("현재 상태에서는 유닛을 선택할 수 없습니다.");
                return;
        }
    }
    public void ProcessAction()
    {
        switch (BattleManager.Instance.currentAction)
        {
            case Action.Move:
                BattleManager.Instance.SetState(BattleManager.States.AreaSelection);
                break;
            case Action.Support:
                BattleManager.Instance.SetState(BattleManager.States.AreaSelection);
                break;

            case Action.Defend:
                BattleManager.Instance.SetState(BattleManager.States.UnitAction);
                break;
        }
    }
    public void CancelSelection()
    {
        BattleManager.Instance.currentUnit = null;
        Debug.Log("유닛 선택 해제");
    }

}
