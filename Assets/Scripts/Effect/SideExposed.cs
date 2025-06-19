using UnityEngine;

public class SideExposed : StatusEffect
{
    public SideExposed( int count)
        : base(StatusEffectType.SideExposed, StackType.duration, false, 0, 0, count)
    {
        // �ʿ��ϸ� �߰� �ʱ�ȭ
    }
    public override (int,bool) OnBeforeDamaged(Unit from, Unit target, Unit.DamageType damageType, int damage)
    {
        return ((int)(damage * 1.5), true);
    }
}
