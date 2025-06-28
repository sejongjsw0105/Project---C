using UnityEngine;
using UnityEngine.EventSystems; // UI Ŭ�� ������

public class CampClickManager : MonoBehaviour
{
    public static CampClickManager Instance;
    public bool canRest = true;
    public bool canEnforce = true;
    public bool canFullRest = true; // ��ü �޽� ���� ����

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