using UnityEngine;

public class UnitMover : MonoBehaviour
{
    public static UnitMover Instance;

    private void Awake() => Instance = this;

    public void PrepareMove()
    {
        Debug.Log("�̵� ��� ���� ���� ��� ��");
        // UI�� �̵� ���� ���� ǥ��
    }
    private void MoveFriendlyUnitToArea(Unit unit, Area targetArea)
    {
        unit.area.occupyingFriendlyUnit = null;
        unit.transform.position = targetArea.transform.position;
        unit.area = targetArea;
        targetArea.occupyingFriendlyUnit = unit;

        UnitSelector.Instance.CancelSelection();
        BattleManager.Instance.SetState(BattleManager.States.UnitSelection);
    }


}
