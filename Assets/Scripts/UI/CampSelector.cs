using UnityEngine;
using UnityEngine.UI;
public class CampSelector : MonoBehaviour
{
    public Button restButton;
    public Button enforceButton;

    private void Start()
    {
        restButton.onClick.AddListener(OnRestClicked);
        enforceButton.onClick.AddListener(OnEnforceClicked);
    }
    private void OnRestClicked()
    {
        CampClickManager.Instance.HandleRestButtonClick();
    }
    private void OnEnforceClicked()
    {
        CampClickManager.Instance.HandleEnforceButtonClick();
    }
    private void Update()
    {
        
    }
}
