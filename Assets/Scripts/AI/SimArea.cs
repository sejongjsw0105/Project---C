using System;
using System.Collections.Generic;

public class SimArea : IArea
{
    public int areaIndexX { get; set; }
    public int areaIndexY { get; set; }
    public AreaType areaType { get; set; }
    public AreaCondition areaCondition { get; set; }
    public IUnit occupyingFriendlyUnit { get; set; }
    public IUnit occupyingEnemyUnit { get; set; }
    public IUnit firstAttacker { get; set; }
    public IUnit secondAttacker { get; set; }
    public float captureScore { get; set; } = 50f;
    public float combatScore { get; set; } = 30f;
    public List<StatusEffect> statusEffects { get; set; } = new();

    public SimArea(IArea origin)
    {
        areaIndexX = origin.areaIndexX;
        areaIndexY = origin.areaIndexY;
        areaType = origin.areaType;
        areaCondition = origin.areaCondition;
        occupyingFriendlyUnit = null;
        occupyingEnemyUnit = null;
        captureScore = origin.captureScore;
        combatScore = origin.combatScore;
        statusEffects = new List<StatusEffect>(origin.statusEffects); // ¾èÀº º¹»ç
    }

    public void AddStatusEffect(StatusEffect effect)
    {
        statusEffects.Add(effect);
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

    public void RemoveOccupant(IUnit unit)
    {
        if (occupyingFriendlyUnit == unit) occupyingFriendlyUnit = null;
        if (occupyingEnemyUnit == unit) occupyingEnemyUnit = null;
        UpdateAreaCondition();
    }

    public IUnit GetAllyOccupant(IUnit reference)
        => reference.faction == Faction.Friendly ? occupyingFriendlyUnit : occupyingEnemyUnit;

    public IUnit GetEnemyOccupant(IUnit reference)
        => reference.faction == Faction.Friendly ? occupyingEnemyUnit : occupyingFriendlyUnit;
}
public class SimAreaManager
{
    public SimArea[,] areaGrid;
    public List<SimArea> allAreas = new();

    public SimAreaManager(IArea[,] originGrid, int width, int height)
    {
        areaGrid = new SimArea[width, height];

        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                SimArea sim = new SimArea(originGrid[x, y]);
                areaGrid[x, y] = sim;
                allAreas.Add(sim);
            }
    }

    public SimArea GetArea(int x, int y)
    {
        if (x < 0 || y < 0 || x >= areaGrid.GetLength(0) || y >= areaGrid.GetLength(1))
            return null;

        return areaGrid[x, y];
    }
}
