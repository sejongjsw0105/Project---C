using UnityEngine;

public class CloseCombat : Artifact
{
    public CloseCombat()
    {
        artifactName = "CloseCombat";
    }
    public override (int, bool) OnBeforeAttack(Unit attacker, Unit target, int damage)
    {

        if (attacker.unitType == Unit.UnitType.Melee || attacker.unitType == Unit.UnitType.Cavalry)
        {
            if(attacker.faction ==Unit.Faction.Friendly)
            return (damage +10 , true);
        }
        return (damage, true);
    }
}