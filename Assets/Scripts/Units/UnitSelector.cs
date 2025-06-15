using UnityEngine;

public class UnitSelector : MonoBehaviour
{
    public static UnitSelector Instance;
    private void Awake() => Instance = this;

    public void BeginSelection()
    {
        Debug.Log("���� ���� ��� ��");
        // UI ǥ�� �Ǵ� �Է� ��� ó��
    }

    public void OnUnitClicked(Unit unit)
    {
        switch(BattleManager.Instance.currentState)
        {
            case BattleManager.States.UnitSelection:
                // �̹� ������ ���õ� ���¿��� �ٽ� Ŭ���ϸ� ���� ����
                if (BattleManager.Instance.currentUnit == unit)
                {
                    CancelSelection();
                    return;
                }
                else
                {
                    BattleManager.Instance.currentUnit = unit;
                    Debug.Log($"{unit.unitName} ���õ�");
                    ProcessAction();
                }
                break;
            default:
                //UI�� ���� ���� ����
                Debug.LogWarning("���� ���¿����� ������ ������ �� �����ϴ�.");
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
        Debug.Log("���� ���� ����");
    }

}
