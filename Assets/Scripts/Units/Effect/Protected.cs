using UnityEngine;

public class Protected : StatusEffect
{
    public Protected(int count = 1)
        : base("Protected",StackType.value, 1000, count)
    {
    }
    public override ResultContext<int> OnBeforeDamaged(Unit from, Unit target, DamageType damageType, int damage)
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
