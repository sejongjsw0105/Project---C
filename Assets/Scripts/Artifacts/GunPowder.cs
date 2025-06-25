using UnityEngine;

public class GunPowder : Artifact
{
    public GunPowder()
    {
        artifactName = "GunPowder";
    }
    public override (int, bool) OnBeforeAttack(Unit attacker, Unit target, int damage)
    {
        if (attacker.unitName == "���������"|| attacker.unitName == "ȭ��")
        {
            if(attacker.faction == Unit.Faction.Friendly)
            {
                return ((int)(damage *1.5), true);
            }
        }
        // ���� ���� �������� 2��� ������Ŵ
        return (damage, true);
    }

}

