using System.Collections.Generic;
using UnityEngine;

public interface IUnit
{
    int unitId { get; set; }
    int currentHealth { get; set; }
    float unitPriority { get; }
    string unitName { get; }
    Faction faction { get; set; }
    UnitType unitType { get; }
    Faction supportableFaction { get; set; } // ���� ������ ���� (��: Friendly ���ָ� ���� ����)
    UnitStats stats { get; }
    UnitStats upgradedStats { get; }

    IArea area { get; set; }

    List<UnitTrait> unitTrait { get; }
    List<StatusEffect> statusEffects { get; }

    UnitState state { get; }


    // ===== �����̻� �� ��� ó�� ���� =====
    void AddStatusEffect(StatusEffect effect);
    T GetStatusEffect<T>() where T : StatusEffect;
    StatusEffect GetStatusEffectByName(string name);

    void Die();  // ���� ��� ó��
    void Win();
    void Lose(); // ���� �й� ó��
}