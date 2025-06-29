using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GridHelper : MonoBehaviour
{
    public static GridHelper Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // �̹� �ν��Ͻ��� �����ϸ� �ڱ� �ڽ� ����
            return;
        }
        Instance = this;
    }
    public bool isSide(IArea from, IArea to)
    {
        int dx = Mathf.Abs(from.areaIndexX - to.areaIndexX);
        if (dx != 0) return true; // x���� ������ ����
        else return false;
    }

    public bool IsMoveAllowed(IUnit unit, IArea to, int moveRange = 1)
    {
        if (unit == null || unit.area == null || to == null) return false;
        if (unit.area == to) return false;

        var from = unit.area;
        int dx = Mathf.Abs(from.areaIndexX - to.areaIndexX);
        int dy = Mathf.Abs(from.areaIndexY - to.areaIndexY);

        // ���� ���� ������ �ִٸ� �̵� �Ұ�
        var occupying = (unit.faction == Faction.Friendly) ? to.occupyingFriendlyUnit : to.occupyingEnemyUnit;
        if (occupying != null)
            return false;

        // ������ ���� �̵� ��Ģ
        bool isFriendly = unit.faction == Faction.Friendly;
        AreaType fromType = from.areaType;
        AreaType toType = to.areaType;

        if (isFriendly)
        {
            if (fromType == AreaType.FriendlyFinal)
                return toType == AreaType.FriendlyRear && dx <= moveRange;
            if (fromType == AreaType.FriendlyRear || fromType == AreaType.Frontline)
                return dx == 0 && dy == 1;
        }
        else
        {
            if (fromType == AreaType.EnemyFinal)
                return toType == AreaType.EnemyRear && dx <= moveRange;
            if (fromType == AreaType.EnemyRear || fromType == AreaType.Frontline)
                return dx == 0 && dy == 1;
        }

        return false;
    }

    public bool IsInRange(IArea from, IArea to, int range)
    {
        int dx = Mathf.Abs(from.areaIndexX - to.areaIndexX);
        int dy = Mathf.Abs(from.areaIndexY - to.areaIndexY);
        return Mathf.Max(dx + dy) <= range;
    }
    public List<IUnit> GetAdjacentUnits(IUnit centerUnit)
    {
        List<IUnit> adjacentUnits = new List<IUnit>();
        var area = centerUnit.area;
        if (area == null) return adjacentUnits;

        int x = area.areaIndexX;
        int y = area.areaIndexY;

        int[,] directions = new int[,] {
        { 0, 1 },  // ��
        { 0, -1 }, // �Ʒ�
        { 1, 0 },  // ������
        { -1, 0 }  // ����
    };

        for (int i = 0; i < 4; i++)
        {
            int nx = x + directions[i, 0];
            int ny = y + directions[i, 1];

            IArea neighbor = AreaManager.Instance.GetArea(nx, ny);
            if (neighbor == null) continue;

            IUnit u = neighbor.occupyingFriendlyUnit ?? neighbor.occupyingEnemyUnit;
            if (u != null)
            {
                adjacentUnits.Add(u);
            }
        }

        return adjacentUnits;
    }


}

