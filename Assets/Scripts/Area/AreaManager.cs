using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    public static AreaManager Instance { get; private set; }
    public List<Area> allAreas = new List<Area>();
    [SerializeField] private Transform areaParent; // ��ü ������ �θ�

    private Area[,] areaGrid; // 2���� �迭�� ����
    private const int width = 3;
    private const int height = 5;
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
            Debug.LogWarning($"[GetArea] ��ǥ ({x},{y})�� ��ȿ ������ ���");
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
                    // AreaType �ڵ� ����
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
                    allAreas.Add(area); // ��� Area ����
                    Debug.Log($"[SetArea] {child.name} ���: ��ǥ [{x},{y}], Ÿ��: {area.areaType}");
                }
                else
                {
                    Debug.LogWarning($"[SetArea] {child.name} ��ǥ�� ��ȿ ������ ���: ({x},{y})");
                }
            }
        }
    }

}
