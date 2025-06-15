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

    public bool IsMoveAllowed(Area from, Area to)
    {
        int dx = Mathf.Abs(from.areaIndexX - to.areaIndexX);
        int dy = Mathf.Abs(from.areaIndexY - to.areaIndexY);
        if (from.areaType != AreaType.FriendlyFinal && from.areaType != AreaType.EnemyFinal)
        {
            if (dx > 0) return false;
        }
        else
        {
            if (dx > 1) return false; // 최종 지역에서 한 칸 이상 이동 불가
        }

        if (from.areaType == AreaType.FriendlyFinal && to.areaType == AreaType.FriendlyRear)
            return !to.isOccupied;

        if ((from.areaType == AreaType.FriendlyRear || from.areaType == AreaType.Frontline) &&
            dy == 1 && dx == 0)
            return !to.isOccupied;

        return false;
    }
    public bool IsSupportAllowed(Area from, Area to, Unit.Support support)
    {
        int dx = Mathf.Abs(from.areaIndexX - to.areaIndexX);
        int dy = Mathf.Abs(from.areaIndexY - to.areaIndexY);
        if (dx > support.supportRange || dy > support.supportRange) return false; // 범위 초과
        if (support.areacond != to.areaCondition) return false;
        return true; // 지원 가능



    }

    // Start is called before the first frame update
    void Start()
    {
        // 초기화 로직이 있다면 여기에 작성
    }

    // Update is called once per frame
    void Update()
    {
        // 매 프레임 수행할 작업이 있다면 여기에 작성
    }
}

