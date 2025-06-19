using UnityEngine;
using System.Collections.Generic;

public class Fear : UnitTrait
{
    public Fear()
    {
        type = UnitTraitType.Fear;
        unitTypes = new List<Unit.UnitType> { Unit.UnitType.Melee, Unit.UnitType.Cavalry };

    }

    public override void OnAfterAttack(Unit attacker, Unit target, int damage)
    {
        if (target == null) return;

        // ü���� 1/4 ������ ��� ���� �ߵ�
        if (target.health <= target.stats.maxHealth / 4 )
        {
            Debug.Log($"[Fear] {target.unitName}�� ������ ���� ����Ĩ�ϴ�!");
            target.Die();
        }
    }
}