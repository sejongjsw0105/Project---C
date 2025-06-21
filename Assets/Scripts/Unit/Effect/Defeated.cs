using UnityEngine;

public class Defeated : StatusEffect
{
    public Defeated() 
        : base(StatusEffectType.Defeated, StackType.duration, false, 0, 1000, 1000)
    {
    }
    public override void OnApply(Unit target, Area area)
    {
        target.health /= 2;
        target.attackPower /= 2;
    }


}
