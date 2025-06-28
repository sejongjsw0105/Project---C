using System;
using System.Linq;
using UnityEngine;

public static class TraitFactory
{
    public static UnitTrait CreateTrait(string traitName, GameObject owner)
    {
        // ��ȿ�� Ÿ�� �̸� (���ӽ����̽� ���ٰ� ����)
        Type type = Type.GetType(traitName);
        if (type == null)
        {
            // ���ӽ����̽��� ���� ��� ó�� (����)
            type = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .FirstOrDefault(t => t.Name == traitName && typeof(UnitTrait).IsAssignableFrom(t));
        }

        if (type != null && typeof(UnitTrait).IsAssignableFrom(type))
        {
            return owner.AddComponent(type) as UnitTrait;
        }

        Debug.LogWarning($"[TraitFactory] '{traitName}' Ʈ���� Ŭ������ ã�� �� �����ϴ�.");
        return null;
    }
}
