using UnityEngine;

using System.Collections.Generic;
public class Healer : UnitTrait
{
    public int healValue = 10; // ġ���� �ɷ�ġ ��
    public Healer()
    {
        type = UnitTraitType.Healer;
        unitTypes = new List<Unit.UnitType>
        {
            Unit.UnitType.Melee,
            Unit.UnitType.Cavalry
        };
    }
    public override void OnUpdate(Unit from)
    {
        List<Unit> units = GridHelper.Instance.GetAdjacentUnits(from);
        foreach (Unit unit in units)
        {
            unit.health += healValue; 
        }
    }

}
