using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    public static AreaManager Instance { get; private set; }
    public List<Area> allAreas = new List<Area>();
    [SerializeField] private Transform areaParent; // 전체 영역의 부모

    private Area[,] areaGrid; // 2차원 배열로 관리
    private const int width = 3;
    private const int height = 5;
    public void ClearAllAreas()
    {
        foreach (var area in allAreas)
        {
            if (area != null)
            {
                area.occupyingFriendlyUnit = null; // 아군 유닛 초기화
                area.occupyingEnemyUnit = null; // 적군 유닛 초기화
                area.areaCondition = AreaCondition.Empty; // 영역 상태 초기화
            }
        }
        areaGrid = new Area[width, height];
        Debug.Log("[ClearAllAreas] 모든 영역을 초기화했습니다.");
    }
    private void Awake()
    {
        AreaManager instance = this;
        areaGrid = new Area[width, height];
        InitializeArea();
    }

    public void InitializeArea()
    {
        SetArea(areaParent);
    }
    public Area GetArea(int x, int y)
    {
        if (x < 0 || x >= width || y < 0 || y >= height)
        {
            Debug.LogWarning($"[GetArea] 좌표 ({x},{y})가 유효 범위를 벗어남");
            return null;
        }
        return  areaGrid[x, y];
    }

    public void SetArea(Transform parent)
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
                    allAreas.Add(area); // 모든 Area 수집
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
