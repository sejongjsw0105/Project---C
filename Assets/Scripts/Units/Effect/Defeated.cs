using System.Linq;
using UnityEngine;

public class Defeated : StatusEffect
{
    public Defeated()
        : base("Defeated", StackType.value, EffectType.Debuff, 1000, 1)
    {
        isPersistent = true; // 이 상태 효과는 지속적입니다.
    }

    public override void OnApply(IUnit target)
    {
        target.stats.attackPower /= 2;
        target.stats.defensePower /= 2;
        target.stats.maxHealth /= 2;
    }

    public override void OnLose(IUnit unit)
    {
        var defeated = unit.GetStatusEffect<Defeated>();
        if (defeated != null && defeated.value >= 2)
        {
            unit.Die();
        }
    }

    public override void OnWin(IUnit unit)
    {
        Expire(unit); 
    }
}
