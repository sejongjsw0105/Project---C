using UnityEngine;

public class GunPowder : Artifact
{
    public GunPowder()
    {
        artifactName = "GunPowder";
    }
    public override (int, bool) OnBeforeAttack(Unit attacker, Unit target, int damage)
    {
        if (attacker.unitName == "승자총통수"|| attacker.unitName == "화차")
        {
            if(attacker.faction == Unit.Faction.Friendly)
            {
                return ((int)(damage *1.5), true);
            }
        }
        // 공격 전에 데미지를 2배로 증가시킴
        return (damage, true);
    }

}

