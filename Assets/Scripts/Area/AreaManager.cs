using System;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    public static AreaManager Instance { get; private set; }
    public List<Area> allAreas = new List<Area>();
    private Area[,] areaGrid; // 2차원 배열로 관리
    public const int width = 3;
    public const int height = 5;
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
    public void BeginBattle()
    {
        Instance = this;
        allAreas.Clear(); 
        areaGrid = new Area[width, height];
        AreaFactory.CreateAllAreaFromGrid(width, height);
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
    public void RegisterArea(Area area)
    {
        var (x, y) = area.GetPosition();
        if (x >= 0 && x < width && y >= 0 && y < height)
        {
            areaGrid[x, y] = area;
            if (!allAreas.Contains(area))
            {
                allAreas.Add(area);
            }
            Debug.Log($"[RegisterArea] ({x},{y}) 영역 등록됨");
        }
        else
        {
            Debug.LogWarning($"[RegisterArea] 잘못된 좌표: ({x},{y})");
        }
    }

}


