using UnityEngine;

public class Protected : StatusEffect
{
    public Protected(int count = 1)
        : base("Protected",StackType.value, EffectType.Buff, 1000, count)
    {
    }
    public override ResultContext<int> OnBeforeDamaged(IUnit from, IUnit target, DamageType damageType, int damage)
    { 
        var context = new ResultContext<int>(damage);
        context.Block();
        value--;
        if (value <= 0)
        {
            Expire(target);
        }
        return context;
    }
}
