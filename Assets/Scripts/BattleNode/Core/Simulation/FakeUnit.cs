using System.Collections.Generic;
using UnityEngine;

public class FakeUnit : Unit
{
    public FakeUnit(Unit original)
    {
        this.unitName = original.unitName;
        this.attackPower = original.attackPower;
        this.defensePower = original.defensePower;
        this.range = original.range;
        this.unitType = original.unitType;
        this.faction = original.faction;
        this.area = original.area;
        this.health = original.health;
        this.stats = new UnitStats
        {
            maxHealth = original.stats.maxHealth,
            
            attackPower = original.stats.attackPower,
            defensePower = original.stats.defensePower,
            range = original.stats.range
        };
        this.unitTrait = new List<UnitTrait>(original.unitTrait);
        this.statusEffects = new List<StatusEffect>(original.statusEffects);

        this.isAttackable = original.isAttackable;
        this.isMovable = original.isMovable;
        this.isSupportable = original.isSupportable;
        this.isDamagedable = original.isDamagedable;
    }

    // �������̹Ƿ� ���� ������Ʈ/����Ʈ ������ ����

    public override void AddStatusEffect(StatusEffect newEffect)
    {
        var copy = newEffect; // ���� ����� ��� (�ùķ��̼Ǹ� �ϹǷ�)
        var existing = statusEffects.Find(e => e.type == copy.type);
        if (existing != null)
        {
            HandleExistingStatusEffect(existing, copy);
        }
        else
        {
            statusEffects.Add(copy);
        }
    }

    public override void Die()
    {
        // �����̹Ƿ� �ƹ� �͵� ���� ����
    }

    public override void Defense()
    {
        AddStatusEffect(new Defending(1, this.defensePower));
    }

    public override Support GetSupport()
    {
        return base.GetSupport(); // ���� ���� ��� (������ support ���)
    }

}