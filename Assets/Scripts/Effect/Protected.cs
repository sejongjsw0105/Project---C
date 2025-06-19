using UnityEngine;

public class Protected : StatusEffect
{
    public Protected(int count = 1)
        : base(StatusEffectType.Protected, StackType.count, true, 0, 1000, count)
    {
    }
    public override (int,bool) OnBeforeDamaged(Unit from, Unit target, Unit.DamageType damageType, int damage)
    {

        count--;
        if (count <= 0)
        {
            from.RemoveExpiredEffect(this); // 효과가 0이 되면 제거
        }
        return (0, false);
    }
}
