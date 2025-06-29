using UnityEngine;

using System.Collections.Generic;
public class Healer : UnitTrait
{
    public int healValue = 10; // ġ���� �ɷ�ġ ��
    public Healer()
    {
        traitName = "Healer";
        unitTypes = new List<UnitType>
        {
            UnitType.Melee,
            UnitType.Cavalry
        };
    }
    public override void OnTurnEnd(IUnit from)
    {
        List<IUnit> units = GridHelper.Instance.GetAdjacentUnits(from);
        foreach (IUnit unit in units)
        {
            unit.currentHealth += healValue; 
        }
    }

}
