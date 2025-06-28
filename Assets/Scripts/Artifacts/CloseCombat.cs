using UnityEngine;

public class CloseCombat : Artifact
{
    public CloseCombat()
    {
        artifactName = "CloseCombat";
    }
    public override ResultContext<int> OnBeforeAttack(Unit attacker, Unit target, int damage)
    {
        var context = new ResultContext<int>(damage);
        if (attacker.unitType == UnitType.Melee || attacker.unitType == UnitType.Cavalry)
        {
            if(attacker.faction ==Faction.Friendly)
            context.Modify((int)((damage + 5)), "CloseCombat");
        }
        return context;
    }
}