using UnityEngine;

public class VisionLess : StatusEffect
{
    public VisionLess( int duration =1000, int value=0 ) 
        : base("VisionLess", StackType.duration, EffectType.Debuff, duration, value)
    {
    }
    public override void OnBattleStart(IUnit target)
    {
        target.stats.range -=value; // 시야 감소 효과 적용
    }
    public override void OnExpire(IUnit target)
    {
        target.stats.range += value; // 시야 감소 효과 해제

    }

}
