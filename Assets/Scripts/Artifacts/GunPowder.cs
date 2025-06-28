using UnityEngine;

public class GunPowder : Artifact
{
    public GunPowder()
    {
        artifactName = "GunPowder";
    }
    public override ResultContext<int> OnBeforeAttack(Unit attacker, Unit target, int damage)
    {
        var context = new ResultContext<int>(damage);

        if (attacker.unitName == "½ÂÀÚÃÑÅë¼ö"|| attacker.unitName == "È­Â÷")
        {
            if(attacker.faction == Faction.Friendly)
            {
                context.Modify((int)(damage * 1.5), "GunPowder");
                
            }
        }
        return context;
    }

}

