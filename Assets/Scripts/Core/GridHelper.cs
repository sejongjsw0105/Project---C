using UnityEngine;
using UnityEngine.Rendering;

public class GridHelper : MonoBehaviour
{
    public static GridHelper Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // 이미 인스턴스가 존재하면 자기 자신 제거
            return;
        }
        Instance = this;
    }
    public bool isSide(Area from, Area to)
    {
        int dx = Mathf.Abs(from.areaIndexX - to.areaIndexX);
        if (dx != 0) return true; // x축이 있으면 측면
        else return false;
    }
    public bool IsFriendlyMoveAllowed(Area from, Area to, int moveRange = 1)
    {
        if (from == to) return false;
        // 대상 칸이 이미 점령됨 → 불가
        if (to.occupyingFriendlyUnit != null)
            return false;

        int dx = Mathf.Abs(from.areaIndexX - to.areaIndexX);
        int dy = Mathf.Abs(from.areaIndexY - to.areaIndexY);

        // 최후방 → 후방 이동은 자유 (단, moveRange 이하)
        if (from.areaType == AreaType.FriendlyFinal)
        {
            return to.areaType == AreaType.FriendlyRear && dx <= moveRange;
        }

        // 후방 또는 전방 → 위/아래 한 칸 이동만 허용
        if (from.areaType == AreaType.FriendlyRear || from.areaType == AreaType.Frontline)
        {
            return dx == 0 && dy == 1;
        }

        return false;
    }
    public bool IsEnemyMoveAllowed(Area from, Area to, int moveRange = 1)
    {
        if (from == to) return false;
        // 대상 칸이 이미 점령됨 → 불가
        if (to.occupyingEnemyUnit != null)
            return false;
        int dx = Mathf.Abs(from.areaIndexX - to.areaIndexX);
        int dy = Mathf.Abs(from.areaIndexY - to.areaIndexY);
        // 최후방 → 후방 이동은 자유 (단, moveRange 이하)
        if (from.areaType == AreaType.EnemyFinal)
        {
            return to.areaType == AreaType.EnemyRear && dx <= moveRange;
        }
        // 후방 또는 전방 → 위/아래 한 칸 이동만 허용
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

}

