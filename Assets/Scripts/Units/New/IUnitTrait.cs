public interface IUnitTrait
{
    void OnAction(UnitActionType action);          // 이동, 지원, 방어 등
    void OnAfterDamaged();                         // 피해를 입은 직후
    void OnTurnStart();                            // 유닛의 턴 시작
    void OnTurnEnd();                              // 유닛의 턴 종료
    void OnKill(Unit target);                      // 적 처치 시
    void OnSupport(Unit target);                   // 아군 지원 시
}
