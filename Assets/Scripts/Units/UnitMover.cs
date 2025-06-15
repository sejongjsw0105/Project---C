using UnityEngine;

public class UnitMover : MonoBehaviour
{
    public static UnitMover Instance;

    private void Awake() => Instance = this;

    public void PrepareMove()
    {
        Debug.Log("이동 대상 지역 선택 대기 중");
        // UI에 이동 가능 영역 표시
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
