using System.Collections.Generic;
using System.Linq;
public class SimWorldSnapshot
{
    public Dictionary<int, IUnit> units = new();  // unitId -> SimUnit
    public List<IArea> areas = new();

    public static SimWorldSnapshot Capture(List<IUnit> unitList, List<IArea> areaList)
    {
        var snapshot = new SimWorldSnapshot();
        snapshot.units = unitList.ToDictionary(u => u.unitId, u => u);
        snapshot.areas = areaList;
        return snapshot;
    }

    public List<IUnit> GetUnitsByFaction(Faction faction)
        => units.Values.Where(u => u.faction == faction && u.currentHealth > 0).ToList();
}