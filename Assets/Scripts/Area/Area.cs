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
    public virtual void AddStatusEffect(StatusEffect newEffect)
    {
        newEffect.OnApply(null,this); // ���� ȿ�� ����
        var existing = statusEffects.Find(e => e.type == newEffect.type);

        if (existing != null)
        {
            HandleExistingStatusEffect(existing, newEffect);
        }
        else
        {
            AddNewStatusEffect(newEffect);
        }
    }

    private void HandleExistingStatusEffect(StatusEffect existing, StatusEffect newEffect)
    {
        switch (newEffect.stackType)
        {
            case StackType.duration:
                existing.duration += newEffect.duration;
                statusEffects.Remove(existing); // ���� ȿ���� �����Ͽ� �ߺ� ����
                AddNewStatusEffect(existing); // ���ο� ȿ���� �߰��Ͽ� �α� ���                
                Debug.Log($"[{newEffect.type}] ���ӽð��� ��ø�Ǿ����ϴ�. �� {existing.duration}��");
                break;
            case StackType.value:
                existing.value += newEffect.value;
                statusEffects.Remove(existing); // ���� ȿ���� �����Ͽ� �ߺ� ����
                AddNewStatusEffect(existing); // ���ο� ȿ���� �߰��Ͽ� �α� ��� 
                Debug.Log($"[{newEffect.type}] ȿ������ ��ø�Ǿ����ϴ�. �� value: {existing.value}");
                break;
            case StackType.count:
                existing.count += newEffect.count;
                statusEffects.Remove(existing); // ���� ȿ���� �����Ͽ� �ߺ� ����
                AddNewStatusEffect(existing); // ���ο� ȿ���� �߰��Ͽ� �α� ��� 
                Debug.Log($"[{newEffect.type}] ��� Ƚ���� ��ø�Ǿ����ϴ�. �� count: {existing.count}");
                break;
            case StackType.none:
                break;
        }
    }

    private void AddNewStatusEffect(StatusEffect newEffect)
    {
        statusEffects.Add(newEffect);
    }
    //�����̻� �ϸ���, ���Ž�
    public void UpdateStatusEffects()
    {
        for (int i = statusEffects.Count - 1; i >= 0; i--)
        {
            var effect = statusEffects[i];
            effect.OnUpdate(null,this); // ���� ȿ�� ������Ʈ
            if (effect.duration > 0)
                effect.duration--;

            if (effect.duration <= 0)
                RemoveExpiredEffect(effect); // index ���� ����
        }
    }
    public void RemoveExpiredEffect(StatusEffect effect)
    {
        effect.OnExpire(null, this);
        statusEffects.Remove(effect);
    }

}
