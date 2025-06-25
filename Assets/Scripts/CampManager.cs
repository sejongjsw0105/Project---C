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
            unit.isUpgraded = true; // ���׷��̵� ���·� ����
        }

    }
    public void Rest()
    {
        foreach (UnitData unit in GameContext.Instance.myUnitDataList)
        {
            unit.currentHealth += (int)(unit.maxHealth* 1/ 5); // �޽� �� ü�� ȸ��
        }
    }
    public void FullRest(UnitData unit)
    {
        unit.currentHealth = unit.maxHealth; // ��ü ü�� ȸ��
    }
}
