using UnityEngine;

public class UnitStats : MonoBehaviour
{
    public int MaxHealth { get; private set; }
    public int CurrentHealth { get; set; } // 수시로 변화
    public int AttackPower { get; private set; }
    public int DefensePower { get; private set; }
    public int Range { get; private set; }

    public void Initialize(int maxHealth, int attack, int defense, int range)
    {
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
        AttackPower = attack;
        DefensePower = defense;
        Range = range;
    }

    public void SetUpgradeStats(int maxHealth, int attack, int defense, int range)
    {
        MaxHealth = maxHealth;
        CurrentHealth = Mathf.Min(CurrentHealth, maxHealth);
        AttackPower = attack;
        DefensePower = defense;
        Range = range;
    }

    public void ResetHealth()
    {
        CurrentHealth = MaxHealth;
    }
}
