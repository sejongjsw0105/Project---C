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
    private void MoveUnitToArea(Unit unit, Area targetArea)
    {
        unit.area.occupyingUnit = null;
        unit.transform.position = targetArea.transform.position;
        unit.area = targetArea;
        targetArea.occupyingUnit = unit;

        UnitSelector.Instance.CancelSelection();
        BattleManager.Instance.SetState(BattleManager.States.UnitSelection);
    }


}
