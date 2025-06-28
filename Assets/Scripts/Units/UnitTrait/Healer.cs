using UnityEngine;

using System.Collections.Generic;
public class Healer : UnitTrait
{
    public int healValue = 10; // 치유할 능력치 값
    public Healer()
    {
        traitName = "Healer";
        unitTypes = new List<UnitType>
        {
            UnitType.Melee,
            UnitType.Cavalry
        };
    }
    public override void OnTurnEnd(Unit from)
    {
        List<Unit> units = GridHelper.Instance.GetAdjacentUnits(from);
        foreach (Unit unit in units)
        {
            unit.currentHealth += healValue; 
        }
    }

}
