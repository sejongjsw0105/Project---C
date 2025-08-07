public interface IBattleState
{
    void Enter();
    void Execute();  // 매 프레임마다 실행 (선택)
    void Exit();
}
