using UnityEngine;

public class Chaos : StatusEffect
{
    public Chaos(int duration = 1) 
        : base("Chaos", StackType.duration, EffectType.Debuff, duration, 0)
    {

    }
    public override ResultContext<int> OnBeforeAttack(IUnit attacker, IUnit target, int damage)
    {
        var result = new ResultContext<int>(damage);
        result.Block("Chaos");
        return result;
    }
    public override ResultContext<int> OnBeforeSupport(IUnit supporter, IArea area, int value)
    {
        var result = new ResultContext<int>(value);
        result.Block("Chaos");
        return result;
    }
    public override ResultContext<int> OnBeforeMove(IUnit unit, IArea target, int moveRange)
    {
        var result = new ResultContext<int>(value);
        result.Block("Chaos");
        return result;
    }
    

}
