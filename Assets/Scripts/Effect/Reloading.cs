using UnityEngine;
public class Reloading : StatusEffect
{
    public Reloading(int duration) : base(StatusEffectType.Reloading, StackType.duration, true, 0, duration, 0)
    {
    }
    public override int OnBeforeSupport(Unit supporter, Area area)
    {
        supporter.isSupportable = false; // 지원 불가 상태로 설정
        return 0;
    }

}