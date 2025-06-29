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
            return;
        }

        // AreaParent GameObject 생성 (또는 기존 객체 찾아서 재사용)
        GameObject areaParent = GameObject.Find("AreaParent");
        if (areaParent == null)
        {
            areaParent = new GameObject("AreaParent");
        }

        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                GameObject areaObj = GameObject.Instantiate(prefab, areaParent.transform); // 부모 지정
                areaObj.name = $"Area_{i}_{j}";

                Area area = areaObj.GetComponent<Area>();
                if (area == null)
                {
                    Debug.LogError($"[AreaFactory] Area 컴포넌트가 없음: {areaObj.name}");
                    continue;
                }

                area.areaIndexX = i;
                area.areaIndexY = j;
                AreaManager.Instance.RegisterArea(area);
            }
        }
    }
}
