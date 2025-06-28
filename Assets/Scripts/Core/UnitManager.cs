using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;

    public List<Unit> allUnits = new List<Unit>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void RegisterUnit(Unit unit)
    {
        if (!allUnits.Contains(unit))
        {
            allUnits.Add(unit);
        }
    }

    public void UnregisterUnit(Unit unit)
    {
        if (allUnits.Contains(unit))
        {
            allUnits.Remove(unit);
        }
    }

    public List<Unit> GetUnitsByFaction(Faction faction)
    {
        return allUnits.FindAll(u => u.faction == faction);
    }

    public void ClearAllUnits()
    {
        allUnits.Clear();
    }
}