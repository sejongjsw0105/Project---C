public interface IUnitTrait
{
    void OnAction(UnitActionType action);          // �̵�, ����, ��� ��
    void OnAfterDamaged();                         // ���ظ� ���� ����
    void OnTurnStart();                            // ������ �� ����
    void OnTurnEnd();                              // ������ �� ����
    void OnKill(Unit target);                      // �� óġ ��
    void OnSupport(Unit target);                   // �Ʊ� ���� ��
}
