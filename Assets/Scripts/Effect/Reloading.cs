using UnityEngine;
public class Reloading : StatusEffect
{
    public Reloading(int duration) : base(StatusEffectType.Reloading, StackType.duration, true, 0, duration, 0)
    {
    }
    public override (int,bool) OnBeforeSupport(Unit supporter, Area area, int value)
    {
        return (0, false); // 지원 효과를 적용하지 않음
    }

}