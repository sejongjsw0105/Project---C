
// UnitFactory.cs
using System;
using UnityEngine;
using System.Linq;

public class UnitFactory : MonoBehaviour
{
    public static GameObject baseUnitPrefab;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void LoadBasePrefab()
    {
        baseUnitPrefab = Resources.Load<GameObject>("BaseUnit");
        if (baseUnitPrefab == null)
        {
            Debug.LogError("[UnitFactory] BaseUnit 프리팹을 Resources/BaseUnit 경로에 배치해야 합니다.");
        }
    }

    public static Unit CreateUnitFromData(UnitData data)
    {
        if (baseUnitPrefab == null)
        {
            Debug.LogError("[UnitFactory] baseUnitPrefab is not loaded.");
            return null;
        }

        GameObject unitObj = GameObject.Instantiate(baseUnitPrefab);
        Unit unit = unitObj.GetComponent<Unit>();

        unit.unitId = data.id;
        unit.unitName = data.name;
        unit.faction = Enum.Parse<Unit.Faction>(data.faction);
        unit.unitType = Enum.Parse<Unit.UnitType>(data.type);

        unit.stats = new Unit.UnitStats
        {
            maxHealth = data.maxHealth,
            currentHealth = data.maxHealth,
            attackPower = data.attack,
            defensePower = data.defense,
            range = data.range
        };

        unit.health = data.maxHealth;
        unit.attackPower = data.attack;
        unit.defensePower = data.defense;
        unit.range = data.range;

        unit.unitTrait = new System.Collections.Generic.List<UnitTrait>();

        foreach (string traitName in data.traits)
        {
            UnitTrait trait = TraitFactory.CreateTrait(traitName);
            if (trait != null)
                unit.unitTrait.Add(trait);
        }

        UnitManager.Instance.RegisterUnit(unit);
        return unit;
    }
}
