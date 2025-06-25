using UnityEngine;

public class Surrender : Artifact
{
    Unit unit;
    public Surrender()
    {
        artifactName = "Surrender";
    }
    public override void OnAfterDamaged(Unit from, Unit target, Unit.DamageType damageType, int damage)
    {
        if (target.faction == Unit.Faction.Enemy)
        {
            if (target.health <= 0)
            {
                target.health = (int)(target.stats.maxHealth / 2); // Prevents death
                target.faction = Unit.Faction.Friendly; // Changes faction to friendly
                target.AddStatusEffect(new Defeated());
            }
        }

    }
}
