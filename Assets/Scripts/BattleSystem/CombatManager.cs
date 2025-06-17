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
        Debug.Log("������ ���۵Ǿ����ϴ�.");

        foreach (var area in AreaManager.Instance.allAreas)
        {
            if (area.areaCondition != AreaCondition.InCombat) continue;
            Unit firstAttacker = area.firstAttacker;
            Unit secondAttacker = area.secondAttacker;
            int damageToSecond = 0;
            int damageToFirst = 0;

            if (secondAttacker == null) continue;

            // �� ���� �� ��
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
                
            
            
            // �� ��� ó�� �� Die() ���ο��� UnitManager, UI, ����Ʈ ó����
            if (firstAttacker.health <= 0)
            {
               firstAttacker.Die(); // �� �̰ɷ�!
            }

            if (secondAttacker.health <= 0)
            {
                secondAttacker.Die();
            }
        }
    }
}
