using UnityEngine;

public class OnFire : StatusEffect
{
    public OnFire(int value, int duration) : base(StatusEffectType.OnFire, StackType.duration, false, value, duration, 1000)
    {
    }
    public override void OnApply(Unit target, Area area)
    {
        if(area != null)
        {
            area.occupyingEnemyUnit.AddStatusEffect(new OnFire(value, 1)); // 영역에 있는 적 유닛에게도 불 효과를 적용
            area.occupyingFriendlyUnit.AddStatusEffect(new OnFire(value, 1)); // 영역에 있는 아군 유닛에게도 불 효과를 적용
        }
    }
    public override void OnUpdate(Unit target, Area area)
    {
        if (target != null)
        {
            target.Damaged(null, Unit.DamageType.Debuff, value); // 매 턴마다 5의 피해를 입힘
        }
        else if (area != null)
        {
            if (area.occupyingEnemyUnit != null)
                area.occupyingEnemyUnit.AddStatusEffect(new OnFire(value, 2));
            if (area.occupyingFriendlyUnit != null)
                area.occupyingFriendlyUnit.AddStatusEffect(new OnFire(value, 2));
        }
        else {
            Debug.LogWarning("OnFire effect applied without a target or area. This should not happen.");
        }


    }

}