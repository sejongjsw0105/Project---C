using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;
    public int turnCount = 0;

    private void Awake() => Instance = this;

    public void StartTurn()
    {
        turnCount++;
        Debug.Log($"턴 {turnCount} 시작");
        BattleManager.Instance.SetState(BattleManager.States.UnitSelection);
    }

    public void EndTurn()
    {
        Debug.Log($"턴 {turnCount} 종료");
        BattleManager.Instance.SetState(BattleManager.States.TurnStart);
    }
}
