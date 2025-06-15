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
        // 전투 로직 구현
        // 예: 유닛 간의 공격, 피해 계산 등
        AreaManager.Instance.allAreas.ForEach(area =>
        {
            if (area.areaCondition == AreaCondition.InCombat)
            {
                Unit friendlyUnit = area.occupyingFriendlyUnit;
                Unit enemyUnit = area.occupyingEnemyUnit;
                if (friendlyUnit != null && enemyUnit != null)
                {
                    // 공격력과 방어력 계산
                    int damageToEnemy = Mathf.Max(0, friendlyUnit.attackPower - enemyUnit.defensePower);
                    int damageToFriendly = Mathf.Max(0, enemyUnit.attackPower - friendlyUnit.defensePower);
                    // 피해 적용
                    enemyUnit.health -= damageToEnemy;
                    friendlyUnit.health -= damageToFriendly;
                    Debug.Log($"{friendlyUnit.unitName}이 {enemyUnit.unitName}에게 {damageToEnemy}의 피해를 입혔습니다.");
                    Debug.Log($"{enemyUnit.unitName}이 {friendlyUnit.unitName}에게 {damageToFriendly}의 피해를 입혔습니다.");
                    // 유닛 사망 처리
                    if (enemyUnit.health <= 0)
                    {
                        enemyUnit.Die();
                        area.occupyingEnemyUnit = null; // 영역에서 제거
                        Debug.Log($"{enemyUnit.unitName}이 사망했습니다.");
                    }
                    if (friendlyUnit.health <= 0)
                    {
                        friendlyUnit.Die();
                        area.occupyingFriendlyUnit = null; // 영역에서 제거
                        Debug.Log($"{friendlyUnit.unitName}이 사망했습니다.");
                    }
                }
            }
        });
        Debug.Log("전투가 시작되었습니다.");
    }
}
