using UnityEngine;

public class Covered: StatusEffect
{
    private Unit from;
    public Covered(Unit from, int duration = 1) : base("Covered", StackType.none, duration,0)
    {
        this.from = from;
    }
    public override ResultContext<int> OnBeforeAttack(Unit attacker, Unit target, int damage)
    {
        var context = new ResultContext<int>(damage);
        context.Modify((int)(damage + from.stats.attackPower), "Covered");
        return context;
    }

}
