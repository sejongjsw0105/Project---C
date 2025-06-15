using UnityEngine;

public class CombatManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Combat()
    {
        // ���� ���� ����
        // ��: ���� ���� ����, ���� ��� ��
        AreaManager.Instance.allAreas.ForEach(area =>
        {
            if (area.areaCondition == AreaCondition.InCombat)
            {
                Unit friendlyUnit = area.occupyingFriendlyUnit;
                Unit enemyUnit = area.occupyingEnemyUnit;
                if (friendlyUnit != null && enemyUnit != null)
                {
                    // ���ݷ°� ���� ���
                    int damageToEnemy = Mathf.Max(0, friendlyUnit.attackPower - enemyUnit.defensePower);
                    int damageToFriendly = Mathf.Max(0, enemyUnit.attackPower - friendlyUnit.defensePower);
                    // ���� ����
                    enemyUnit.health -= damageToEnemy;
                    friendlyUnit.health -= damageToFriendly;
                    Debug.Log($"{friendlyUnit.unitName}�� {enemyUnit.unitName}���� {damageToEnemy}�� ���ظ� �������ϴ�.");
                    Debug.Log($"{enemyUnit.unitName}�� {friendlyUnit.unitName}���� {damageToFriendly}�� ���ظ� �������ϴ�.");
                    // ���� ��� ó��
                    if (enemyUnit.health <= 0)
                    {
                        enemyUnit.Die();
                        area.occupyingEnemyUnit = null; // �������� ����
                        Debug.Log($"{enemyUnit.unitName}�� ����߽��ϴ�.");
                    }
                    if (friendlyUnit.health <= 0)
                    {
                        friendlyUnit.Die();
                        area.occupyingFriendlyUnit = null; // �������� ����
                        Debug.Log($"{friendlyUnit.unitName}�� ����߽��ϴ�.");
                    }
                }
            }
        });
        Debug.Log("������ ���۵Ǿ����ϴ�.");
    }
}
