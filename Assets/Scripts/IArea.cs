using System.Collections.Generic;
using UnityEngine;
public interface IArea
{
    int areaIndexX { get; }
    int areaIndexY { get; }

    AreaType areaType { get; }
    AreaCondition areaCondition { get; }

    IUnit occupyingFriendlyUnit { get; set; }
    IUnit occupyingEnemyUnit { get; set; }
    IUnit firstAttacker { get; set; }
    IUnit secondAttacker { get; set; }
    float captureScore { get; }
    float combatScore { get; }

    List<StatusEffect> statusEffects { get; }

    IUnit GetAllyOccupant(IUnit reference);
    IUnit GetEnemyOccupant(IUnit reference);
    void RemoveOccupant(IUnit unit);
    void AddStatusEffect(StatusEffect effect);
    void UpdateAreaCondition();
}