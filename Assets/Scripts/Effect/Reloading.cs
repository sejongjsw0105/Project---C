using UnityEngine;
public class Reloading : StatusEffect
{
    public Reloading(int duration) : base(StatusEffectType.Reloading, StackType.duration, true, 0, duration, 0)
    {
    }
    public override int OnBeforeSupport(Unit supporter, Area area)
    {
        supporter.isSupportable = false; // ���� �Ұ� ���·� ����
        return 0;
    }

}