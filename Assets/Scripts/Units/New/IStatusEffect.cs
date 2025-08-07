public interface IStatusEffect
{
    /// <summary>
    /// 상태이상이 부여될 때 1회 호출
    /// </summary>
    void OnApply();

    /// <summary>
    /// 유닛의 턴이 시작될 때 호출
    /// </summary>
    void OnTurnStart();

    /// <summary>
    /// 유닛의 턴이 종료될 때 호출 (지속시간 감소 등)
    /// </summary>
    void OnTurnEnd();

    /// <summary>
    /// 유닛이 피해를 받기 전에 호출 (피해량 조정)
    /// </summary>
    /// <param name="damage">피해량 (참조 전달)</param>
    void OnBeforeTakeDamage(ref int damage);

    /// <summary>
    /// 유닛이 피해를 받은 후 호출
    /// </summary>
    void OnAfterTakeDamage();

    /// <summary>
    /// 상태이상이 제거될 때 호출
    /// </summary>
    void OnRemove();
}
