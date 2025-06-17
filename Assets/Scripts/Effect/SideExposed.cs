using UnityEngine;

public class SideExposed : StatusEffect
{
    public SideExposed( int count)
        : base(StatusEffectType.SideExposed, StackType.duration, false, 0, 0, count)
    {
        // 필요하면 추가 초기화
    }
    public override int OnBeforeDamage(Unit from, Unit target, Unit.DamageType damageType, int damage)
    {
        return (int)(damage * 1.5);
    }
}
