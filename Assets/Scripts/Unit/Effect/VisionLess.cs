using UnityEngine;

public class VisionLess : StatusEffect
{
    public VisionLess(int duration = 1, int value = 0) 
        : base(StatusEffectType.VisionLess, StackType.duration, false, value, duration, 1000)
    {
    }
    public override void OnApply(Unit target, Area area)
    {
        target.range -=value; // 시야 감소 효과 적용
    }
    public override void OnExpire(Unit target, Area area)
    {
        target.range += value; // 시야 감소 효과 해제

    }

}
