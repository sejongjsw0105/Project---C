using System.Collections.Generic;
public static class UnitConverter
{
    public static UnitSnapshot FromUnit(Unit unit)
    {
        return new UnitSnapshot
        {
            unitName = unit.unitName,
            unitType = unit.unitType,
            faction = unit.faction,
            currentHealth = unit.currentHealth,
            stats = new UnitStats
            {
                maxHealth = unit.stats.maxHealth,
                attackPower = unit.stats.attackPower,
                defensePower = unit.stats.defensePower,
                range = unit.stats.range
            },
            unitTrait = new List<UnitTrait>(unit.unitTrait),
            statusEffects = new List<StatusEffect>(unit.statusEffects),
            area = unit.area,
            canAttack = unit.state.CanAttack,
            canMove = unit.state.CanMove,
            canSupport = unit.state.CanSupport,
            canBeDamaged = unit.state.CanDamaged
        };
    }

    public static Dictionary<Unit, UnitSnapshot> CloneSnapshotDictionary(List<Unit> units)
    {
        var result = new Dictionary<Unit, UnitSnapshot>();
        foreach (var unit in units)
            result[unit] = FromUnit(unit);
        return result;
    }
}
