using System.Collections.Generic;
// Step 1: Snapshot 구조 정의

public class UnitSnapshot
{
    public string unitName;
    public UnitType unitType;
    public Faction faction;
    public int currentHealth;
    public UnitStats stats;
    public List<UnitTrait> unitTrait = new();
    public List<StatusEffect> statusEffects = new();
    public Area area;

    public bool canAttack = true;
    public bool canMove = true;
    public bool canSupport = true;
    public bool canBeDamaged = true;
}
