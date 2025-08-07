using UnityEngine;

public class BattleStateMachine : MonoBehaviour
{
    private IBattleState _currentState;

    public void ChangeState(IBattleState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter();
    }

    private void Update()
    {
        _currentState?.Execute();
    }
}
