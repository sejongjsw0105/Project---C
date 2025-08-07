using System.Collections.Generic;

public class UnitManager : MonoSingleton<UnitManager>
{
    // 예: 유닛 리스트
    private List<Unit> _units = new List<Unit>();

    public void RegisterUnit(Unit unit)
    {
        if (!_units.Contains(unit))
            _units.Add(unit);
    }

    public void RemoveUnit(Unit unit)
    {
        _units.Remove(unit);
    }

    public List<Unit> GetAllUnits() => _units;
}
