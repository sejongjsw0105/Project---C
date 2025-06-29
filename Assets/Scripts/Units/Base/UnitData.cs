using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UnitData
{
    public int id;
    public string name;
    public Faction faction;
    public UnitType type;
    public int currentHealth;
    public UnitStats baseStats;
    public UnitStats upgradedStats;
    public bool isUpgraded;
    public List<UnitTrait> traits; // MonoBehaviour 기반
    public List<StatusEffect> statusEffects; // MonoBehaviour 기반
}
