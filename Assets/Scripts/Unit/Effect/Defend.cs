using UnityEngine;

public class Defending : StatusEffect
{
    public int Shield = 0;

    public Defending(int duration, int value)
        : base(StatusEffectType.Defend, StackType.value, true, value,duration,1000)
    {
    }

    public override void OnApply(Unit target, Area area)
    {
        base.OnApply(target, area);
        Shield += value; // 방어력 증가
        Debug.Log($"[Defend] {target.unitName}의 방어력이 {value} 증가했습니다. (현재 방어력: {target.defensePower})");
    }

    public override (int,bool) OnBeforeDamaged(Unit from, Unit target, Unit.DamageType damageType, int incomingDamage)
    {
        int absorbed = Mathf.Min(Shield, incomingDamage);
        Shield -= absorbed;
        int result = incomingDamage - absorbed;

        Debug.Log($"[Defend] {target.unitName}의 방어막이 {absorbed} 피해를 흡수했습니다. (남은 방어막: {Shield})");
        if (Shield <= 0)
        {
            target.RemoveExpiredEffect(this); // 방어막이 0 이하가 되면 효과 제거

        }
        return (result,true);
    }

    public override void OnExpire(Unit target, Area area)
    {
        base.OnExpire(target, area);
        Shield =0; // 방어력 원상복구
        Debug.Log($"[Defend] {target.unitName}의 방어력이 {value} 감소했습니다. (현재 방어력: {target.defensePower})");
    }
}
