using System;
using UnityEngine;

public class ActionSelector : MonoBehaviour
{
    public static ActionSelector Instance;
    private void Awake() => Instance = this;

    public void BeginSelection()
    {
        Debug.Log("유닛 선택 대기 중");
        // UI 표시 또는 입력 대기 처리
    }

    public void OnActionClicked(Action action)
    {
        switch (BattleManager.Instance.currentState)
        {
            case BattleManager.States.ActionSelection:
                // 이미 지역이 선택된 상태에서 다시 클릭하면 선택 해제
                if (BattleManager.Instance.currentAction == action)
                {
                    CancelSelection();
                    return;
                }
                else
                {
                    BattleManager.Instance.SetCurrentAction(action);
                    Debug.Log($"{action} 선택됨");
                    ProcessAction();
                }
                break;
            default:
                //UI로 액션 정보 띄우기
                return;
        }
    }
    public void ProcessAction()
    {
        BattleManager.Instance.SetState(BattleManager.States.UnitSelection);
    }

    public void CancelSelection()
    {
        BattleManager.Instance.SetCurrentAction(Action.None);
        Debug.Log("지역 선택 해제");
    }

}