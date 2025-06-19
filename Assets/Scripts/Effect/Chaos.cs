using UnityEngine;

public class Chaos : StatusEffect
{
    public Chaos(int duration = 1) 
        : base(StatusEffectType.Chaos, StackType.duration, false, 0, duration, 1000)
    {

    }
    public override (int, bool) OnBeforeAttack(Unit attacker, Unit target, int damage)
    {
        return (0, false);
    }
    public override (int, bool) OnBeforeSupport(Unit supporter, Area area, int value)
    {
        return (0, false);
    }
    public override (int, bool) OnBeforeMove(Unit unit, Area target, int moveRange)
    {
        return (0, false);
    }
    

}
