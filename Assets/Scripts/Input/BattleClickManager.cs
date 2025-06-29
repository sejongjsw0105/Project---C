using UnityEngine;
using UnityEngine.EventSystems; // UI Ŭ�� ������

public class BattleClickManager : MonoBehaviour
{
    public static BattleClickManager Instance;

    private void Awake() => Instance = this;

    void Update()
    {
        // UI Ŭ���̸� ����
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButtonDown(0)) // ��Ŭ��
        {
            HandleLeftClick();
        }
        else if (Input.GetMouseButtonDown(1)) // ��Ŭ��
        {
            CancelAllSelections();
        }
    }

    public void HandleActionButtonClick(Action action)
    {
        if (!(BattleManager.Instance.stateMachine.CurrentState is ActionSelectionState))
        {
            return;
        }

        int cost = BattleManager.Instance.GetActionCost(action);
        if (!BattleManager.Instance.CanAffordCommandPoints(cost))
            return;

        BattleManager.Instance.stateMachine.CurrentState.HandleActionClick(action);
    }

    public void HandleTurnEndButtonClick()
    {
        if (BattleManager.Instance.stateMachine.CurrentState is ActionSelectionState)
        {
            BattleManager.Instance.stateMachine.SetState(new TurnEndState());
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
                if (BattleManager.Instance.stateMachine.CurrentState is UnitSelectionState)
                    BattleManager.Instance.stateMachine.CurrentState.HandleUnitClick(unit);
                else
                    CancelAllSelections(); // �߸��� Ÿ�̹� Ŭ�� �� ���
            }
            else if (clicked.TryGetComponent<Area>(out var area))
            {
                if (BattleManager.Instance.stateMachine.CurrentState is AreaSelectionState)
                    BattleManager.Instance.stateMachine.CurrentState.HandleAreaClick(area);
                else
                    CancelAllSelections();
            }
            else
            {
                // �ٸ� ������Ʈ Ŭ�� �� ���
                CancelAllSelections();
            }
        }
        else
        {
            // �ƹ� �͵� Ŭ������ ���� �� ���
            CancelAllSelections();
        }
    }

    public void CancelAllSelections()
    {
        BattleManager.Instance.currentUnit = null;
        BattleManager.Instance.currentArea = null;
        BattleManager.Instance.SetCurrentAction(Action.None);
    }
}
