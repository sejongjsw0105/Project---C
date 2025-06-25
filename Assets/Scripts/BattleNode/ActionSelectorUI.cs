using UnityEngine;
using UnityEngine.UI;

public class ActionSelectorUI : MonoBehaviour
{
    public Button moveButton;
    public Button supportButton;
    public Button defendButton;
    public Button turnEndButton;

    private void Start()
    {
        moveButton.onClick.AddListener(() => OnActionClicked(Action.Move));
        supportButton.onClick.AddListener(() => OnActionClicked(Action.Support));
        defendButton.onClick.AddListener(() => OnActionClicked(Action.Defend));
        turnEndButton.onClick.AddListener(OnTurnEndClicked);
    }

    private void OnActionClicked(Action action)
    {
        BattleClickManager.Instance.HandleActionButtonClick(action);
    }

    private void OnTurnEndClicked()
    {
        BattleClickManager.Instance.HandleTurnEndButtonClick();
    }
}
