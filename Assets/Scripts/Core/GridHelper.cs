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
    public bool isSide(Area from, Area to)
    {
        int dx = Mathf.Abs(from.areaIndexX - to.areaIndexX);
        if (dx != 0) return true; // x���� ������ ����
        else return false;
    }
    public bool IsFriendlyMoveAllowed(Area from, Area to, int moveRange = 1)
    {
        if (from == to) return false;
        // ��� ĭ�� �̹� ���ɵ� �� �Ұ�
        if (to.occupyingFriendlyUnit != null)
            return false;

        int dx = Mathf.Abs(from.areaIndexX - to.areaIndexX);
        int dy = Mathf.Abs(from.areaIndexY - to.areaIndexY);

        // ���Ĺ� �� �Ĺ� �̵��� ���� (��, moveRange ����)
        if (from.areaType == AreaType.FriendlyFinal)
        {
            return to.areaType == AreaType.FriendlyRear && dx <= moveRange;
        }

        // �Ĺ� �Ǵ� ���� �� ��/�Ʒ� �� ĭ �̵��� ���
        if (from.areaType == AreaType.FriendlyRear || from.areaType == AreaType.Frontline)
        {
            return dx == 0 && dy == 1;
        }

        return false;
    }
    public bool IsEnemyMoveAllowed(Area from, Area to, int moveRange = 1)
    {
        if (from == to) return false;
        // ��� ĭ�� �̹� ���ɵ� �� �Ұ�
        if (to.occupyingEnemyUnit != null)
            return false;
        int dx = Mathf.Abs(from.areaIndexX - to.areaIndexX);
        int dy = Mathf.Abs(from.areaIndexY - to.areaIndexY);
        // ���Ĺ� �� �Ĺ� �̵��� ���� (��, moveRange ����)
        if (from.areaType == AreaType.EnemyFinal)
        {
            return to.areaType == AreaType.EnemyRear && dx <= moveRange;
        }
        // �Ĺ� �Ǵ� ���� �� ��/�Ʒ� �� ĭ �̵��� ���
        if (from.areaType == AreaType.EnemyRear || from.areaType == AreaType.Frontline)
        {
            return dx == 0 && dy == 1;
        }
        return false;
    }

    public bool IsInRange(Area from, Area to, int range)
    {
        int dx = Mathf.Abs(from.areaIndexX - to.areaIndexX);
        int dy = Mathf.Abs(from.areaIndexY - to.areaIndexY);
        return Mathf.Max(dx + dy) <= range;
    }
    public List<Unit> GetAdjacentUnits(Unit centerUnit)
    {
        List<Unit> adjacentUnits = new List<Unit>();
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

            Area neighbor = AreaManager.Instance.GetArea(nx, ny);
            if (neighbor == null) continue;

            Unit u = neighbor.occupyingFriendlyUnit ?? neighbor.occupyingEnemyUnit;
            if (u != null)
            {
                adjacentUnits.Add(u);
            }
        }

        return adjacentUnits;
    }


}

