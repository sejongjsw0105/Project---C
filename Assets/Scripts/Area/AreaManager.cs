using System;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    [SerializeField] private Transform areaParent; // 전체 영역의 부모

    private IArea[,] areaGrid; // 2차원 배열로 관리
    private const int width = 3;
    private const int height = 5;

    private void Awake()
    {
        areaGrid = new Area[width, height];
        InitializeArea();
    }

    private void InitializeArea()
    {
        SetArea(areaParent);
    }
    private void SetAreaCondition(Area area)
    {
        if (area.isOccupied)
        {
            switch (area.occupyingUnit.faction)
            {
                case Unit.Faction.Friendly:
                    area.areaCondition = AreaCondition.FriendlyOccupied;
                    break;
                case Unit.Faction.Enemy:
                    area.areaCondition = AreaCondition.EnemyOccupied;
                    break;
                default:
                    area.areaCondition = AreaCondition.NeutralOccupied;
                    break;
            }
            
        }
        if (!area.isOccupied)
        {
            area.areaCondition = AreaCondition.Empty;
        }
    }
    private void SetArea(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Area area = child.GetComponent<Area>();
            if (area != null)
            {
                var (x, y) = area.GetPosition();

                if (x >= 0 && x < width && y >= 0 && y < height)
                {
                    // AreaType 자동 설정
                    switch (y)
                    {
                        case 0:
                            area.areaType = AreaType.FriendlyFinal;
                            break;
                        case 1:
                            area.areaType = AreaType.FriendlyRear;
                            break;
                        case 2:
                            area.areaType = AreaType.Frontline;
                            break;
                        case 3:
                            area.areaType = AreaType.EnemyRear;
                            break;
                        case 4:
                            area.areaType = AreaType.EnemyFinal;
                            break;
                    }

                    areaGrid[x, y] = area;

                    Debug.Log($"[SetArea] {child.name} 등록: 좌표 [{x},{y}], 타입: {area.areaType}");
                }
                else
                {
                    Debug.LogWarning($"[SetArea] {child.name} 좌표가 유효 범위를 벗어남: ({x},{y})");
                }
            }
        }
    }
}