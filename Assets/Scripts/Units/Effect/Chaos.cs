using UnityEngine;

public class Chaos : StatusEffect
{
    public Chaos(int duration = 1) 
        : base("Chaos", StackType.duration, duration, 0)
    {

    }
    public override ResultContext<int> OnBeforeAttack(Unit attacker, Unit target, int damage)
    {
        var result = new ResultContext<int>(damage);
        result.Block("Chaos");
        return result;
    }
    public override ResultContext<int> OnBeforeSupport(Unit supporter, Area area, int value)
    {
        var result = new ResultContext<int>(value);
        result.Block("Chaos");
        return result;
    }
    public override ResultContext<int> OnBeforeMove(Unit unit, Area target, int moveRange)
    {
        var result = new ResultContext<int>(value);
        result.Block("Chaos");
        return result;
    }
    

}
