using UnityEngine;

public class Defending : StatusEffect
{
    public int Shield = 0;

    public Defending(int duration, int value)
        : base("Defend", StackType.value, 1, value)
    {
    }

    public override void OnApply(Unit target)
    {
        Shield += value; // ���� ����
    }

    public override ResultContext<int> OnBeforeDamaged(Unit from, Unit target, DamageType damageType, int incomingDamage)
    {
        int absorbed = Mathf.Min(Shield, incomingDamage);
        Shield -= absorbed;
        int result = incomingDamage - absorbed;

        if (Shield <= 0)
        {
            Expire(target);
        }
        return new ResultContext<int>(result)
        {
            IsBlocked = true,
            Source = "Defend" // ��� ȿ���� ���� ���� ����
        };
    }

    public override void OnExpire(Unit target)
    {
        base.OnExpire(target);
        Shield =0; // ���� ���󺹱�
    }
}
