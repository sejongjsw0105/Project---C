using System.Linq;
using UnityEngine;

public class Defeated : StatusEffect
{
    public Defeated() 
        :base("Defeated", StackType.value, 1000, 1)
    {
        
    }
    public override void OnApply(Unit target)
    {
        target.stats.attackPower /= 2;
        target.stats.defensePower /= 2;
        target.stats.maxHealth /= 2;    
    }
    public override void OnLose(Unit unit)
    {
        var defeated = unit.GetStatusEffect<Defeated>();
        if (defeated != null && defeated.value >= 2)
        {
            unit.Die();
        }
    }
    public override void OnWin(Unit unit)
    {
        Expire(unit, null);
    }

}
