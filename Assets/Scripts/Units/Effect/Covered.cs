using UnityEngine;

public class Covered: StatusEffect
{
    private IUnit from;
    public Covered(IUnit from, int duration = 1) : base("Covered", StackType.none, EffectType.Buff, duration,0)
    {
        this.from = from;
    }
    public override ResultContext<int> OnBeforeAttack(IUnit attacker, IUnit target, int damage)
    {
        var context = new ResultContext<int>(damage);
        context.Modify((int)(damage + from.stats.attackPower), "Covered");
        Expire(attacker);
        return context;
    }

}
