using UnityEngine;
using UnityEngine.EventSystems; // UI 클릭 감지용

public class ClickManager : MonoBehaviour
{
    public static ClickManager Instance;

    private void Awake() => Instance = this;

    void Update()
    {
        // UI 클릭이면 무시
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButtonDown(0)) // 좌클릭
        {
            HandleLeftClick();
        }
        else if (Input.GetMouseButtonDown(1)) // 우클릭
        {
            CancelAllSelections();
        }
    }
    public void HandleActionButtonClick(Action action)
    {
        if (BattleManager.Instance.currentState != BattleManager.States.ActionSelection)
        {
            Debug.LogWarning("지금은 행동을 선택할 수 없습니다.");
            return;
        }
        int cost = BattleManager.Instance.GetActionCost(action);
        if (!BattleManager.Instance.CanAffordCommandPoints(cost))
            return;
        BattleManager.Instance.HandleActionClick(action);
    }

    public void HandleTurnEndButtonClick()
    {
        if (BattleManager.Instance.currentState == BattleManager.States.ActionSelection )
        {
            Debug.Log("턴 종료 버튼 클릭됨");
            BattleManager.Instance.SetState(BattleManager.States.TurnEnd);
        }
        else
        {
            Debug.LogWarning("현재 상태에서는 턴을 종료할 수 없습니다.");
        }
    }
    private void HandleLeftClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            GameObject clicked = hit.collider.gameObject;

            if (clicked.TryGetComponent<Unit>(out var unit))
            {
                if (BattleManager.Instance.currentState == BattleManager.States.UnitSelection)
                    BattleManager.Instance.HandleUnitClick(unit);
                else
                    CancelAllSelections(); // 잘못된 타이밍 클릭 → 취소
            }
            else if (clicked.TryGetComponent<Area>(out var area))
            {
                if (BattleManager.Instance.currentState == BattleManager.States.AreaSelection)
                    BattleManager.Instance.HandleAreaClick(area);
                else
                    CancelAllSelections();
            }
            else
            {
                // 다른 오브젝트 클릭 → 취소
                CancelAllSelections();
            }
        }
        else
        {
            // 아무 것도 클릭되지 않음 → 취소
            CancelAllSelections();
        }
    }

    public void CancelAllSelections()
    {
        BattleManager.Instance.CancelSelection();
        BattleManager.Instance.currentUnit = null;
        BattleManager.Instance.currentArea = null;
        BattleManager.Instance.SetCurrentAction(Action.None);
        Debug.Log("선택 모두 취소됨");
    }
}
