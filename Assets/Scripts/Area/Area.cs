using System;
using System.Collections.Generic;
using UnityEngine;

public enum AreaCondition
{
    InCombat,
    Empty,
    FriendlyOccupied,
    EnemyOccupied,
    NeutralOccupied
}

public enum AreaType
{
    FriendlyFinal,
    FriendlyRear,
    Frontline,
    EnemyRear,
    EnemyFinal
}

public class Area : MonoBehaviour
{
    public Unit firstAttacker;
    public Unit secondAttacker;
    public int areaIndexX;
    public int areaIndexY;
    public AreaType areaType;
    public Unit occupyingFriendlyUnit;
    public Unit occupyingEnemyUnit;
    public AreaCondition areaCondition;

    public List<StatusEffect> statusEffects = new List<StatusEffect>();

    private void Start()
    {
        if (AreaManager.Instance != null)
        {
            AreaManager.Instance.RegisterArea(this);
        }

        switch (areaIndexY)
        {
            case 0: areaType = AreaType.FriendlyFinal; break;
            case 1: areaType = AreaType.FriendlyRear; break;
            case 2: areaType = AreaType.Frontline; break;
            case 3: areaType = AreaType.EnemyRear; break;
            case 4: areaType = AreaType.EnemyFinal; break;
        }

        transform.position = new Vector3(areaIndexX * 12, 0, areaIndexY * 12);
    }

    public Tuple<int, int> GetPosition()
    {
        return Tuple.Create(areaIndexX, areaIndexY);
    }

    public void UpdateAreaCondition()
    {
        if (occupyingFriendlyUnit != null && occupyingEnemyUnit != null)
            areaCondition = AreaCondition.InCombat;
        else if (occupyingFriendlyUnit != null)
            areaCondition = AreaCondition.FriendlyOccupied;
        else if (occupyingEnemyUnit != null)
            areaCondition = AreaCondition.EnemyOccupied;
        else
            areaCondition = AreaCondition.Empty;
    }

    public void AddStatusEffect(StatusEffect newEffect)
    {
        newEffect.AreaOnApply(this);
        var existing = statusEffects.Find(e => e.effectName == newEffect.effectName);

        if (existing != null)
        {
            HandleExistingStatusEffect(existing, newEffect);
        }
        else
        {
            statusEffects.Add(newEffect);
        }
    }

    private void HandleExistingStatusEffect(StatusEffect existing, StatusEffect newEffect)
    {
        switch (newEffect.stackType)
        {
            case StackType.duration:
                existing.duration += newEffect.duration;
                Debug.Log($"[{existing.effectName}] Áö¼Ó½Ã°£ ÁßÃ¸µÊ: ÃÑ {existing.duration}ÅÏ");
                break;
            case StackType.value:
                existing.value += newEffect.value;
                Debug.Log($"[{existing.effectName}] °ª ÁßÃ¸µÊ: ÃÑ value = {existing.value}");
                break;
            case StackType.none:
                break;
        }
    }

    public void AfterTurnEnd()
    {
        UpdateStatusEffects();

        foreach (var artifact in GameContext.Instance.myArtifacts)
        {
            artifact.AreaOnTurnEnd(this);
        }
    }

    private void UpdateStatusEffects()
    {
        for (int i = statusEffects.Count - 1; i >= 0; i--)
        {
            var effect = statusEffects[i];
            effect.AreaOnTurnEnd(this);

            if (effect.duration > 0)
                effect.duration--;

            if (effect.duration <= 0)
            {
                effect.AreaOnExpire(this);
                statusEffects.RemoveAt(i);
            }
        }
    }

    public Unit GetAllyOccupant(Unit reference)
    {
        return reference.faction switch
        {
            Faction.Friendly => occupyingFriendlyUnit,
            Faction.Enemy => occupyingEnemyUnit,
            _ => null
        };
    }

    public Unit GetEnemyOccupant(Unit reference)
    {
        return reference.faction switch
        {
            Faction.Friendly => occupyingEnemyUnit,
            Faction.Enemy => occupyingFriendlyUnit,
            _ => null
        };
    }
    public void RemoveOccupant(Unit unit)
    {
        if (unit == occupyingFriendlyUnit)
        {
            occupyingFriendlyUnit = null;
        }
        else if (unit == occupyingEnemyUnit)
        {
            occupyingEnemyUnit = null;
        }

        UpdateAreaCondition();
    }
}
