using UnityEngine;

public class SideExposed : StatusEffect
{
    public SideExposed(int duration,int value =1)
        : base("SideExposed", StackType.value, EffectType.Debuff,duration, value)
    {
        // 필요하면 추가 초기화
    }
    public override ResultContext<int> OnBeforeDamaged(IUnit from, IUnit target, DamageType damageType, int damage)
    {
        var context = new ResultContext<int>(damage);
        context.Modify((int)(damage * 1.5f)); // 피해량을 50% 증가
        value--;
        if (value <= 0)
        {
            Expire(target);
        }
        return context;
    }
}
