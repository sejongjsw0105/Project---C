public interface IStatusEffect
{
    /// <summary>
    /// �����̻��� �ο��� �� 1ȸ ȣ��
    /// </summary>
    void OnApply();

    /// <summary>
    /// ������ ���� ���۵� �� ȣ��
    /// </summary>
    void OnTurnStart();

    /// <summary>
    /// ������ ���� ����� �� ȣ�� (���ӽð� ���� ��)
    /// </summary>
    void OnTurnEnd();

    /// <summary>
    /// ������ ���ظ� �ޱ� ���� ȣ�� (���ط� ����)
    /// </summary>
    /// <param name="damage">���ط� (���� ����)</param>
    void OnBeforeTakeDamage(ref int damage);

    /// <summary>
    /// ������ ���ظ� ���� �� ȣ��
    /// </summary>
    void OnAfterTakeDamage();

    /// <summary>
    /// �����̻��� ���ŵ� �� ȣ��
    /// </summary>
    void OnRemove();
}
