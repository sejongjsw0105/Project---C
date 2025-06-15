using UnityEngine;

public class AreaSelector : MonoBehaviour
{
    public static AreaSelector Instance;
    private void Awake() => Instance = this;

    public void BeginSelection()
    {
        Debug.Log("���� ���� ��� ��");
        // UI ǥ�� �Ǵ� �Է� ��� ó��
    }

    public void OnAreaClicked(Area area)
    {
        switch (BattleManager.Instance.currentState)
        {
            case BattleManager.States.AreaSelection:
                // �̹� ������ ���õ� ���¿��� �ٽ� Ŭ���ϸ� ���� ����
                if (BattleManager.Instance.currentArea == area)
                {
                    CancelSelection();
                    return;
                }
                else
                {
                    BattleManager.Instance.currentArea = area;
                    Debug.Log($"{area.areaType} ���õ�");
                    ProcessAction();
                }
                break;
            default:
                //UI�� ���� ���� ����
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
        Debug.Log("���� ���� ����");
    }

}