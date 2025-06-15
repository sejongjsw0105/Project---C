using UnityEngine;

public class AreaSelector : MonoBehaviour
{
    public static AreaSelector Instance;
    private void Awake() => Instance = this;

    public void BeginSelection()
    {
        Debug.Log("유닛 선택 대기 중");
        // UI 표시 또는 입력 대기 처리
    }

    public void OnAreaClicked(Area area)
    {
        switch (BattleManager.Instance.currentState)
        {
            case BattleManager.States.AreaSelection:
                // 이미 지역이 선택된 상태에서 다시 클릭하면 선택 해제
                if (BattleManager.Instance.currentArea == area)
                {
                    CancelSelection();
                    return;
                }
                else
                {
                    BattleManager.Instance.currentArea = area;
                    Debug.Log($"{area.areaType} 선택됨");
                    ProcessAction();
                }
                break;
            default:
                //UI로 지역 정보 띄우기
                return;
        }
    }
    public void ProcessAction()
    {
        switch (BattleManager.Instance.currentAction)
        {
            case Action.Move:
                BattleManager.Instance.SetState(BattleManager.States.UnitMove);
                break;
            case Action.Support:
                BattleManager.Instance.SetState(BattleManager.States.UnitAction);
                break;
        }
    }

    public void CancelSelection()
    {
        BattleManager.Instance.currentArea = null;
        Debug.Log("지역 선택 해제");
    }

}