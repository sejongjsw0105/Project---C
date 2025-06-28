using System;
using UnityEngine;

public class AreaFactory : MonoBehaviour
{
    public static AreaFactory Instance { get; private set; }

    public static void CreateAllAreaFromGrid(int x, int y)
    {
        GameObject prefab = Resources.Load<GameObject>("BaseArea");
        if (prefab == null)
        {
            Debug.LogError("BaseArea prefab not found in Resources.");
            return;
        }

        // AreaParent GameObject ���� (�Ǵ� ���� ��ü ã�Ƽ� ����)
        GameObject areaParent = GameObject.Find("AreaParent");
        if (areaParent == null)
        {
            areaParent = new GameObject("AreaParent");
        }

        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                GameObject areaObj = GameObject.Instantiate(prefab, areaParent.transform); // �θ� ����
                areaObj.name = $"Area_{i}_{j}";

                Area area = areaObj.GetComponent<Area>();
                if (area == null)
                {
                    Debug.LogError($"[AreaFactory] Area ������Ʈ�� ����: {areaObj.name}");
                    continue;
                }

                area.areaIndexX = i;
                area.areaIndexY = j;
                AreaManager.Instance.RegisterArea(area);
            }
        }
    }
}
