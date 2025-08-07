using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    public BattleStateMachine StateMachine { get; private set; }
    public CommandPointManager CommandPoints { get; private set; }
    public SelectionManager Selection { get; private set; }
    public VictoryChecker Victory { get; private set; }

    private void Awake()
    {
        Instance = this;
        StateMachine = new BattleStateMachine();
        CommandPoints = new CommandPointManager(3);
        Selection = new SelectionManager();
        Victory = new VictoryChecker();

        GameEvents.OnUnitDied.Subscribe(_ =>
        {
            Victory.CheckVictoryCondition();
        });
    }

    private void Start()
    {
        StateMachine.SetState(new BattleStartState());
    }

    public void ResetBattle()
    {
        UnitManager.Instance.ClearAllUnits();
        AreaManager.Instance.ClearAllAreas();
        Selection.Clear();
        StateMachine.SetState(new BattleStartState());
    }
    public enum BattleEndReason { None, TurnLimitReached, AllEnemiesDefeated, CommanderKilled, AllFriendliesDefeated }
    public BattleEndReason EndReason { get; private set; } = BattleEndReason.None;

    public void VictoryEnd(BattleEndReason reason)
    {
        EndReason = reason;
        StateMachine.SetState(new BattleEndState());
        GameEvents.OnBattleEnded.Invoke(reason);
    }
}