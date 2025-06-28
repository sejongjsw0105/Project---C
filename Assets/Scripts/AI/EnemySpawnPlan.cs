using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySpawnPlan
{
    public class EnemyUnitEntry
    {
        public GameObject unitPrefab;
        public Vector2Int spawnPosition; // areaIndexX, areaIndexY
    }

    public List<EnemyUnitEntry> units = new();

    public void Spawn()
    {
        foreach (var entry in units)
        {
            Area area = AreaManager.Instance.GetArea(entry.spawnPosition.x, entry.spawnPosition.y);
            if (area == null || area.occupyingEnemyUnit != null) continue;

            var obj = GameObject.Instantiate(entry.unitPrefab, area.transform.position, Quaternion.identity);
            Unit unit = obj.GetComponent<Unit>();
            unit.area = area;
            area.occupyingEnemyUnit = unit;
            area.UpdateAreaCondition();
        }
    }
}