using UnityEngine;

public class OnFire : StatusEffect
{
    public OnFire(int value, int duration) : base("OnFire", StackType.duration, duration, value)
    {
    }
    public override void AreaOnApply(Area area)
    {
        area.occupyingEnemyUnit.AddStatusEffect(new OnFire(value, 1)); // 영역에 있는 적 유닛에게도 불 효과를 적용
        area.occupyingFriendlyUnit.AddStatusEffect(new OnFire(value, 1)); // 영역에 있는 아군 유닛에게도 불 효과를 적용
    }
    public override void OnTurnEnd(Unit target)
    {
        DamageAction.Execute(null, target, DamageType.Debuff, value); // 2. 대상에게 피해를 입힘
    }
    public override void AreaOnTurnEnd(Area area)
    {
        // 영역 내 모든 유닛에게 불 효과 적용
        if (area.occupyingEnemyUnit != null)
            area.occupyingEnemyUnit.AddStatusEffect(new OnFire(value, 2));
        if (area.occupyingFriendlyUnit != null)
            area.occupyingFriendlyUnit.AddStatusEffect(new OnFire(value, 2));
    }

}