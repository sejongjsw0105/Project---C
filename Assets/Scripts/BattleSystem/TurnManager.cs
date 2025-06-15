using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;
    public int turnCount = 0;

    private void Awake() => Instance = this;

    public void StartTurn()
    {
        turnCount++;
        Debug.Log($"�� {turnCount} ����");
        BattleManager.Instance.SetState(BattleManager.States.UnitSelection);
    }

    public void EndTurn()
    {
        Debug.Log($"�� {turnCount} ����");
        BattleManager.Instance.SetState(BattleManager.States.TurnStart);
    }
}
