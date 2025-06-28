using UnityEngine;

public class UnitSelectionUI : MonoBehaviour
{
    public Transform contentRoot;
    public GameObject unitItemPrefab;
    public static UnitSelectionUI Instance;
    public UnitSelectionMode selectionMode;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public enum UnitSelectionMode
    {
        Enforce,
        FullRest
    }


    public void PopulateUnitList()
    {
        foreach (var data in GameContext.Instance.myUnitDataList)
        {
            var item = Instantiate(unitItemPrefab, contentRoot);
            //item.GetComponent<UnitDataItemUI>().SetUp(data, OnSelectUnit);
        }
    }
    public void OnSelectUnit(UnitData unit)
    {
        switch (selectionMode)
        {
            case UnitSelectionMode.Enforce:
                CampManager.Instance.Enforce(unit);
                break;
            case UnitSelectionMode.FullRest:
                CampManager.Instance.FullRest(unit);
                break;
            default:
                break;
        }
    }
}
