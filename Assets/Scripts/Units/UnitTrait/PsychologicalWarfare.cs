using UnityEngine;
using System.Collections.Generic;

public class PsychologicalWarfare : UnitTrait
{
    public PsychologicalWarfare()
    {
        traitName = "Psychological Warfare";
        unitTypes = new List<UnitType> { UnitType.Melee, UnitType.Cavalry };
    }

    public override void OnAfterAttack(Unit attacker, Unit target, int damage)
    {
        if (target == null) return;

        if (target.currentHealth <= 0)
        {
            ReviveAsDefector(attacker, target);
        }
    }

    private void ReviveAsDefector(Unit attacker, Unit target)
    {

        // ü�� ȸ�� (����), ���� ����
        target.currentHealth = target.stats.maxHealth / 2;
        target.faction = attacker.faction;

        // Defeated ���� �ο�
        target.AddStatusEffect(new Defeated());

        // ������ ��ϵǾ� ���� �ʴٸ� �ٽ� ���
        if (!UnitManager.Instance.allUnits.Contains(target))
        {
            UnitManager.Instance.RegisterUnit(target);
        }

        // ������ �Ҽӵ� �������� ���� ������ ������Ʈ
        if (target.area != null)
        {
            target.area.UpdateAreaCondition();
        }
    }
}
