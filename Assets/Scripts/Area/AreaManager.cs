using System;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    public static AreaManager Instance { get; private set; }
    public List<Area> allAreas = new List<Area>();
    private Area[,] areaGrid; // 2���� �迭�� ����
    public const int width = 3;
    public const int height = 5;
    public void ClearAllAreas()
    {
        foreach (var area in allAreas)
        {
            if (area != null)
            {
                area.occupyingFriendlyUnit = null; // �Ʊ� ���� �ʱ�ȭ
                area.occupyingEnemyUnit = null; // ���� ���� �ʱ�ȭ
                area.areaCondition = AreaCondition.Empty; // ���� ���� �ʱ�ȭ
            }
        }
        areaGrid = new Area[width, height];
        Debug.Log("[ClearAllAreas] ��� ������ �ʱ�ȭ�߽��ϴ�.");
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
            Debug.LogWarning($"[GetArea] ��ǥ ({x},{y})�� ��ȿ ������ ���");
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
            Debug.Log($"[RegisterArea] ({x},{y}) ���� ��ϵ�");
        }
        else
        {
            Debug.LogWarning($"[RegisterArea] �߸��� ��ǥ: ({x},{y})");
        }
    }

}


