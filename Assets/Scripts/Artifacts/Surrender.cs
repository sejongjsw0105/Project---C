using UnityEngine;

public class Surrender : Artifact
{
    Unit unit;
    public Surrender()
    {
        artifactName = "Surrender";
    }
    public override void OnAfterDamaged(Unit from, Unit target, DamageType damageType, int damage)
    {
        if (target.faction == Faction.Enemy)
        {
            if (target.currentHealth <= 0)
            {
                target.currentHealth = (int)(target.stats.maxHealth / 2); // Prevents death
                target.faction = Faction.Friendly; // Changes faction to friendly
                target.AddStatusEffect(new Defeated());
            }
        }

    }
}
