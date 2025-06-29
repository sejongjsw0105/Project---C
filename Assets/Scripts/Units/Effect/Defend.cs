using UnityEngine;
public class Defending : StatusEffect
{
    public Defending(int duration, int value)
        : base("Defend", StackType.value, EffectType.Buff, duration, value)
    {
    }

    public override ResultContext<int> OnBeforeDamaged(IUnit from, IUnit target, DamageType damageType, int incomingDamage)
    {
        int absorbed = Mathf.Min(value, incomingDamage);
        value -= absorbed;
        int result = incomingDamage - absorbed;

        if (value <= 0)
        {
            Expire(target);
        }

        var context = new ResultContext<int>(result);

        return context;
    }
}
