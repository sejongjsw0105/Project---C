using System.Collections.Generic;
using System.Linq;

public class SimUnit : IUnit
{
    public int unitId { get; set; }
    public int currentHealth { get; set; }
    public float unitPriority { get; set; }
    public string unitName { get; set; }

    public Faction supportableFaction { get; set; } = Faction.Other; // 예: Friendly 유닛만 지원 가능
    public Faction faction { get; set; }
    public UnitType unitType { get; set; }
    public UnitStats stats { get; set; }
    public UnitStats upgradedStats { get; set; }
    public IArea area { get; set; }
    public List<UnitTrait> unitTrait { get; set; } = new();
    public List<StatusEffect> statusEffects { get; set; } = new();
    public UnitState state { get; set; } = new();

    public SimUnit(IUnit origin)
    {
        unitId = origin.unitId;
        currentHealth = origin.currentHealth;
        unitPriority = origin.unitPriority;
        unitName = origin.unitName;
        faction = origin.faction;
        unitType = origin.unitType;
        stats = CloneStats(origin.stats);
        upgradedStats = CloneStats(origin.upgradedStats);
        unitTrait = new List<UnitTrait>(origin.unitTrait); // 얕은 복사
        statusEffects = new List<StatusEffect>(origin.statusEffects); // 얕은 복사
        state = new UnitState
        {
            CanAttack = origin.state.CanAttack,
            CanMove = origin.state.CanMove,
            CanSupport = origin.state.CanSupport,
            CanDamaged = origin.state.CanDamaged,
            CanDefend = origin.state.CanDefend
        };
    }

    private UnitStats CloneStats(UnitStats source)
    {
        return new UnitStats
        {
            maxHealth = source.maxHealth,
            attackPower = source.attackPower,
            defensePower = source.defensePower,
            range = source.range
        };
    }

    public void AddStatusEffect(StatusEffect effect)
    {
        statusEffects.Add(effect);
    }

    public T GetStatusEffect<T>() where T : StatusEffect
    {
        return statusEffects.OfType<T>().FirstOrDefault();
    }

    public StatusEffect GetStatusEffectByName(string name)
    {
        return statusEffects.FirstOrDefault(s => s.effectName == name);
    }

    public void Die() => currentHealth = 0;
    public void Win() { }
    public void Lose() { }
}
public class SimUnitManager
{
    public List<SimUnit> allUnits = new();

    public SimUnitManager(IEnumerable<IUnit> units)
    {
        allUnits = units.Select(u => new SimUnit(u)).ToList();
    }

    public List<SimUnit> GetUnitsByFaction(Faction faction)
        => allUnits.Where(u => u.faction == faction && u.currentHealth > 0).ToList();
}
