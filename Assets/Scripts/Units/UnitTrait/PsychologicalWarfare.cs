using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PsychologicalWarfare : UnitTrait
{
    public PsychologicalWarfare()
    {
        traitName = "Psychological Warfare";
        unitTypes = new List<UnitType> { UnitType.Melee, UnitType.Cavalry };
    }

    public override void OnAfterAttack(IUnit attacker, IUnit target, int damage)
    {
        if (target == null) return;

        if (target.currentHealth <= 0)
        {
            ReviveAsDefector(attacker, target);
        }
    }

    private void ReviveAsDefector(IUnit attacker, IUnit target)
    {

        // ü�� ȸ�� (����), ���� ����
        target.currentHealth = target.stats.maxHealth / 2;
        target.faction = attacker.faction;

        // Defeated ���� �ο�
        target.AddStatusEffect(new Defeated());

        if (target is Unit t)
        {
            // ������ ��ϵǾ� ���� �ʴٸ� �ٽ� ���
            if (!UnitManager.Instance.allUnits.Contains(t))
            {
                UnitManager.Instance.RegisterUnit(t);
            }
        }
        // ������ �Ҽӵ� �������� ���� ������ ������Ʈ
        if (target.area != null)
        {
            target.area.UpdateAreaCondition();
        }
    }
}
