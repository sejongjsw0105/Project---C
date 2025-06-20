using UnityEngine;
using UnityEngine.EventSystems; // UI Ŭ�� ������

public class ClickManager : MonoBehaviour
{
    public static ClickManager Instance;

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
        if (BattleManager.Instance.currentState != BattleManager.States.ActionSelection)
        {
            Debug.LogWarning("������ �ൿ�� ������ �� �����ϴ�.");
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
            Debug.Log("�� ���� ��ư Ŭ����");
            BattleManager.Instance.SetState(BattleManager.States.TurnEnd);
        }
        else
        {
            Debug.LogWarning("���� ���¿����� ���� ������ �� �����ϴ�.");
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
                    CancelAllSelections(); // �߸��� Ÿ�̹� Ŭ�� �� ���
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
        BattleManager.Instance.CancelSelection();
        BattleManager.Instance.currentUnit = null;
        BattleManager.Instance.currentArea = null;
        BattleManager.Instance.SetCurrentAction(Action.None);
        Debug.Log("���� ��� ��ҵ�");
    }
}
