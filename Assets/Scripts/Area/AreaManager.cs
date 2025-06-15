using System;
using System.Collections.Generic;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    public static AreaManager Instance { get; private set; }
    public List<Area> allAreas = new List<Area>();
    [SerializeField] private Transform areaParent; // ��ü ������ �θ�

    private IArea[,] areaGrid; // 2���� �迭�� ����
    private const int width = 3;
    private const int height = 5;

    private void Awake()
    {
        AreaManager instance = this;
        areaGrid = new Area[width, height];
        InitializeArea();
    }

    private void InitializeArea()
    {
        SetArea(areaParent);
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