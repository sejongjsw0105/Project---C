using UnityEngine;

public class VisionLess : StatusEffect
{
    public VisionLess( int duration =1000, int value=0 ) 
        : base("VisionLess", StackType.duration, duration, value)
    {
    }
    public override void OnBattleStart(Unit target)
    {
        target.stats.range -=value; // �þ� ���� ȿ�� ����
    }
    public override void OnExpire(Unit target)
    {
        target.stats.range += value; // �þ� ���� ȿ�� ����

    }

}
