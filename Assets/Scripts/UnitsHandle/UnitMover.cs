using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class UnitMover : MonoBehaviour
{
    public static UnitMover Instance;

    private void Awake() => Instance = this;

    public void PrepareMove()
    {
        BattleManager.Instance.SpendCommandPoints(BattleManager.Instance.GetActionCost(Action.Move));
        Debug.Log("�̵� ��� ���� ���� ��� ��");
        // UI�� �̵� ���� ���� ǥ��
    }

    public void MoveFriendlyUnitToArea(Unit unit, Area targetArea)
    {
        var previousArea = unit.area;
        if (!unit.isMovable||!GridHelper.Instance.IsFriendlyMoveAllowed(previousArea, targetArea, unit.ApplyBeforeMove(unit, targetArea))) return;

        previousArea.occupyingFriendlyUnit = null;
        unit.transform.position = targetArea.transform.position;
        unit.area = targetArea;

        if (targetArea.occupyingEnemyUnit != null)
        {
            targetArea.firstAttacker = unit;
            targetArea.secondAttacker = targetArea.occupyingEnemyUnit;
        }
        targetArea.occupyingFriendlyUnit = unit;
        targetArea.SetAreaCondition();
        UnitSelector.Instance.CancelSelection();
        BattleManager.Instance.SetState(BattleManager.States.UnitSelection);
    }
    public void MoveEnemyUnitToArea(Unit unit, Area targetArea)
    {
        var previousArea = unit.area;
        if (!unit.isMovable||!GridHelper.Instance.IsEnemyMoveAllowed(unit.area, targetArea, unit.ApplyBeforeMove(unit, targetArea)))
        {
            return;
        };
        unit.area.occupyingEnemyUnit = null;
        unit.transform.position = targetArea.transform.position;
        unit.area = targetArea;
        if (targetArea.occupyingFriendlyUnit != null)
        {
            targetArea.firstAttacker = targetArea.occupyingFriendlyUnit;
            targetArea.secondAttacker = unit;
        }
        targetArea.occupyingEnemyUnit = unit;
        targetArea.SetAreaCondition(); // �̵� �� ���� ���¸� �������� ����
        UnitSelector.Instance.CancelSelection();
        BattleManager.Instance.SetState(BattleManager.States.UnitSelection);
    }


}
