using UnityEngine;
using UnityEngine.EventSystems; // UI 클릭 감지용

public class CampClickManager : MonoBehaviour
{
    public static CampClickManager Instance;
    public bool canRest = true;
    public bool canEnforce = true;
    public bool canFullRest = true; // 전체 휴식 가능 여부

    private void Awake() => Instance = this;

    void Update()
    {
    }

    public void HandleRestButtonClick()
    {
        if (canRest)
        {
            CampManager.Instance.Rest();
        }
    }
    public void HandleEnforceButtonClick()
    {
        if (canEnforce)
        {
            UnitSelectionUI.Instance.PopulateUnitList();
        }

    }
    public void HandleFullRestButtonClick()
    {

        if (canFullRest)
        {

            UnitSelectionUI.Instance.PopulateUnitList();
        }

    }
}