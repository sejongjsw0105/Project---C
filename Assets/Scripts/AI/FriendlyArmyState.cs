using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FriendlyArmyState
{
    public class UnitRecord
    {
        public string unitID; // 유닛 이름 or 고유 ID
        public Vector2Int gridPosition; // areaIndexX, areaIndexY
        public int currentHealth; // 사기

        public UnitType unitType;
        public List<UnitTrait> traits = new();
        public List<StatusEffect> statusEffects = new();
    }

    public List<UnitRecord> units = new();

    public void CaptureFromScene()
    {
        units.Clear();
        foreach (Unit unit in UnitManager.Instance.GetUnitsByFaction(Faction.Friendly))
        {
            var record = new UnitRecord
            {
                unitID = unit.unitName,
                gridPosition = new Vector2Int(unit.area.areaIndexX, unit.area.areaIndexY),
                currentHealth = unit.currentHealth,
                unitType = unit.unitType,
                traits = unit.unitTrait,
                statusEffects = unit.statusEffects
            };
            units.Add(record);
        }
    }
}
