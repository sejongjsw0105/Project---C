// UnitData.cs
using System;
using System.Collections.Generic;

[Serializable]
public class UnitData
{
    public int id;
    public string name;
    public string faction; // "Friendly", "Enemy"
    public string type;    // "Melee", "Ranged", etc.
    public int maxHealth;
    public int currentHealth;
    public int attack;
    public int defense;
    public int range;
    public string[] traits; // e.g., ["Fire", "Pressure"]
    public bool isUpgraded; // true if upgraded, false otherwise
}