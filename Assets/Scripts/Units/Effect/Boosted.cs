using JetBrains.Annotations;
using UnityEngine;

public class Boosted:StatusEffect
{
    public Boosted(int duration = 1, int value =150 )
        : base("Boosted",StackType.value,duration, value)
    {
    }
    public override ResultContext<int> OnBeforeAttack(Unit attacker, Unit target, int damage)
    {
        var context = new ResultContext<int>(damage);
        context.Modify((int)((damage * value) / 100));
        return context;
    }  
}
