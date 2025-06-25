using UnityEngine;

public class FireFight : Artifact
{
    public FireFight()
    {
        artifactName = "FireFight";
    }
    public override (int, bool) OnBeforeAttack(Unit attacker, Unit target, int damage)
    {

        if (attacker.unitType == Unit.UnitType.Ranged)
        {
            if(attacker.faction ==Unit.Faction.Friendly)
            return (damage +5 , true);
        }
        return (damage, true);
    }
}