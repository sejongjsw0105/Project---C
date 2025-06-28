using UnityEngine;

public class FireFight : Artifact
{
    public FireFight()
    {
        artifactName = "FireFight";
    }
    public override ResultContext<int> OnBeforeAttack(Unit attacker, Unit target, int damage)
    {
        var context = new ResultContext<int>(damage);
        if (attacker.unitType == UnitType.Ranged)
        {
            
            if(attacker.faction == Faction.Friendly)
            {
                context.Modify((int)((damage +5)));
            }
        }
        return context;
    }
}