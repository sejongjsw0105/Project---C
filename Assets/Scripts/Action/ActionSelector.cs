using System;
using UnityEngine;

public class ActionSelector : MonoBehaviour
{
    public static ActionSelector Instance;
    private void Awake() => Instance = this;

    public void BeginSelection()
    {
        Debug.Log("���� ���� ��� ��");
        // UI ǥ�� �Ǵ� �Է� ��� ó��
    }

    public void OnActionClicked(Action action)
    {
        switch (BattleManager.Instance.currentState)
        {
            case BattleManager.States.ActionSelection:
                // �̹� ������ ���õ� ���¿��� �ٽ� Ŭ���ϸ� ���� ����
                if (BattleManager.Instance.currentAction == action)
                {
                    CancelSelection();
                    return;
                }
                else
                {
                    BattleManager.Instance.currentAction = action;
                    Debug.Log($"{action} ���õ�");
                    ProcessAction();
                }
                break;
            default:
                //UI�� �׼� ���� ����
                return;
        }
    }
    public void ProcessAction()
    {
        BattleManager.Instance.SetState(BattleManager.States.UnitSelection);
    }

    public void CancelSelection()
    {
        BattleManager.Instance.currentAction = Action.None;
        Debug.Log("���� ���� ����");
    }

}