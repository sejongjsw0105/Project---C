using System;
using System.Collections.Generic;
using UnityEngine;

public enum AreaCondition
{
    InCombat, // ���� ��
    Empty, // ��� ����
    FriendlyOccupied, // �Ʊ� ����
    EnemyOccupied, // ���� ����
    NeutralOccupied // �߸� ����
}
public enum AreaType
{
    FriendlyFinal,   // �츮 ���Ĺ� (1ĭ)
    FriendlyRear,    // �츮 �Ĺ� (3ĭ)
    Frontline,       // ���� (3ĭ, ���� �߻� ����)
    EnemyRear,       // �� �Ĺ� (3ĭ)
    EnemyFinal       // �� ���Ĺ� (1ĭ)
}

public class Area : MonoBehaviour
{
    public Unit firstAttacker;  // ���� �̵��ؿ� ����
    public Unit secondAttacker;
    public int areaIndexX;
    public int areaIndexY;
    public AreaType areaType;          // ������ Ÿ��
    public Unit occupyingFriendlyUnit;         // ���� ����
    public Unit occupyingEnemyUnit;         // ���� ������ �����ϰ� �ִ� ����
    public AreaCondition areaCondition; // ������ ����
    public List<StatusEffect> statusEffects = new List<StatusEffect>();
    public Tuple<int, int> GetPosition()
    {
        return Tuple.Create(areaIndexX, areaIndexY);
    }
    public void SetAreaCondition()
    {
        Unit unit1 = occupyingFriendlyUnit;
        Unit unit2 = occupyingEnemyUnit;
        if (unit1 != null && unit2 != null)
        {
            areaCondition = AreaCondition.InCombat; // �� ������ ��� �����ϸ� ���� ��
        }
        else if (unit1 != null)
        {
            areaCondition = AreaCondition.FriendlyOccupied; // �Ʊ� ���ָ� ����
        }
        else if (unit2 != null)
        {
            areaCondition = AreaCondition.EnemyOccupied; // ���� ���ָ� ����
        }
        else
        {
            areaCondition = AreaCondition.Empty; // ��� ����
        }

    }
    public void AddStatusEffect(StatusEffect effect)
    {
        statusEffects.Add(effect);
        effect.OnApply(null, this); // ���� ȿ�� ����
    }
    //�����̻� �ϸ���, ���Ž�
    public void UpdateStatusEffects()
    {
        for (int i = statusEffects.Count - 1; i >= 0; i--)
        {
            StatusEffect effect = statusEffects[i];

            effect.OnUpdate(null, this);       //  1. �� �� ȿ�� ����
            effect.duration--;           //  2. �� ���� �� duration ����

            if (effect.duration <= 0)
            {
                effect.OnExpire(null, this);   //  3. ���� �� ����
                statusEffects.RemoveAt(i);
            }
        }
    }

}
