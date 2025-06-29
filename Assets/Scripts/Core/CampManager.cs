using UnityEngine;

public class CampManager : MonoBehaviour
{
    public static CampManager Instance;

    private void Awake() => Instance = this;

    public void Enforce(UnitData unit)
    {
        if (unit.isUpgraded)
            return;

        unit.isUpgraded = true; // 상태만 전환
    }

    public void Rest()
    {
        foreach (UnitData unit in GameContext.Instance.myUnitDataList)
        {
            var maxHp = unit.isUpgraded ? unit.upgradedStats.maxHealth : unit.baseStats.maxHealth;
            unit.currentHealth += (int)(maxHp * 0.2f);
            if (unit.currentHealth > maxHp)
                unit.currentHealth = maxHp;
        }
    }

    public void FullRest(UnitData unit)
    {
        var maxHp = unit.isUpgraded ? unit.upgradedStats.maxHealth : unit.baseStats.maxHealth;
        unit.currentHealth = maxHp;
    }
}
