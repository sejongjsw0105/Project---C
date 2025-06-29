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
    Faction supportableFaction { get; set; } // 지원 가능한 진영 (예: Friendly 유닛만 지원 가능)
    UnitStats stats { get; }
    UnitStats upgradedStats { get; }

    IArea area { get; set; }

    List<UnitTrait> unitTrait { get; }
    List<StatusEffect> statusEffects { get; }

    UnitState state { get; }


    // ===== 상태이상 및 사망 처리 관련 =====
    void AddStatusEffect(StatusEffect effect);
    T GetStatusEffect<T>() where T : StatusEffect;
    StatusEffect GetStatusEffectByName(string name);

    void Die();  // 유닛 사망 처리
    void Win();
    void Lose(); // 유닛 패배 처리
}