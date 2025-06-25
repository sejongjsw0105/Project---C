using UnityEngine;

public class CampManager : MonoBehaviour
{
    public static CampManager Instance;
    private void Awake() => Instance = this;

    public void Enforce(UnitData unit)
    {
        if(unit.isUpgraded)
        {
            return;
        }
        else
        {
            unit.isUpgraded = true; // 업그레이드 상태로 변경
        }

    }
    public void Rest()
    {
        foreach (UnitData unit in GameContext.Instance.myUnitDataList)
        {
            unit.currentHealth += (int)(unit.maxHealth* 1/ 5); // 휴식 시 체력 회복
        }
    }
    public void FullRest(UnitData unit)
    {
        unit.currentHealth = unit.maxHealth; // 전체 체력 회복
    }
}
