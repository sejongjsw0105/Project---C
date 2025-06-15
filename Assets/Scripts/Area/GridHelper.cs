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
            if (dx > 1) return false; // ���� �������� �� ĭ �̻� �̵� �Ұ�
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
        if (dx > support.supportRange || dy > support.supportRange) return false; // ���� �ʰ�
        if (support.areacond != to.areaCondition) return false;
        return true; // ���� ����



    }

    // Start is called before the first frame update
    void Start()
    {
        // �ʱ�ȭ ������ �ִٸ� ���⿡ �ۼ�
    }

    // Update is called once per frame
    void Update()
    {
        // �� ������ ������ �۾��� �ִٸ� ���⿡ �ۼ�
    }
}

