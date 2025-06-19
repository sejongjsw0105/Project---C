using UnityEngine;

public class Covered: StatusEffect
{
    private Unit from;
    public Covered(Unit from, int duration = 1, int value = 0) : base(StatusEffectType.Covered, StackType.duration, true, value, duration, 1000)
    {
        this.from = from;

    }
    public override (int,bool) OnBeforeAttack(Unit attacker, Unit target, int damage)
    {
        return (damage+ from.attackPower,true);
    }

}
