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
            area.occupyingEnemyUnit.AddStatusEffect(new OnFire(value, 1)); // ������ �ִ� �� ���ֿ��Ե� �� ȿ���� ����
            area.occupyingFriendlyUnit.AddStatusEffect(new OnFire(value, 1)); // ������ �ִ� �Ʊ� ���ֿ��Ե� �� ȿ���� ����
        }
    }
    public override void OnUpdate(Unit target, Area area)
    {
        if (target != null)
        {
            target.Damaged(null, Unit.DamageType.Debuff, value); // �� �ϸ��� 5�� ���ظ� ����
        }
        else if (area != null)
        {
            if (area.occupyingEnemyUnit != null)
                area.occupyingEnemyUnit.AddStatusEffect(new OnFire(value, 1));
            if (area.occupyingFriendlyUnit != null)
                area.occupyingFriendlyUnit.AddStatusEffect(new OnFire(value, 1));
        }
        else {
            Debug.LogWarning("OnFire effect applied without a target or area. This should not happen.");
        }


    }

}