using JetBrains.Annotations;
using UnityEngine;

public class Boosted:StatusEffect
{
    public Boosted(int duration, int value)
        : base(StatusEffectType.Boosted, StackType.duration, true, value, duration, 1000)
    {
    }
    public override (int, bool) OnBeforeAttack(Unit attacker, Unit target, int damage)
    {
        return (damage * value, true);
    }  
}
