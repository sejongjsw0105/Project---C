using UnityEngine;


public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Combat()
    {
        Debug.Log("전투가 시작되었습니다.");

        foreach (var area in AreaManager.Instance.allAreas)
        {
            if (area.areaCondition != AreaCondition.InCombat) continue;
            Unit firstAttacker = area.firstAttacker;
            Unit secondAttacker = area.secondAttacker;
            int damageToSecond = 0;
            int damageToFirst = 0;

            if (secondAttacker == null) continue;

            // ① 공격 전 훅
            if (firstAttacker.isAttackable){
                
                damageToSecond = firstAttacker.PrepareAttack(secondAttacker, firstAttacker.attackPower);
                firstAttacker.Damaged(secondAttacker, Unit.DamageType.Damage, damageToSecond);
                firstAttacker.AfterAttack(secondAttacker, damageToSecond);

            }
            if (secondAttacker.isAttackable)
            {
                damageToFirst = secondAttacker.PrepareAttack(firstAttacker, secondAttacker.attackPower);
                firstAttacker.Damaged(secondAttacker, Unit.DamageType.Damage, damageToFirst);
                secondAttacker.AfterAttack(firstAttacker, damageToFirst);
            }
                
            
            
            // ④ 사망 처리 → Die() 내부에서 UnitManager, UI, 이펙트 처리됨
            if (firstAttacker.health <= 0)
            {
               firstAttacker.Die(); // 꼭 이걸로!
            }

            if (secondAttacker.health <= 0)
            {
                secondAttacker.Die();
            }
        }
    }
}
