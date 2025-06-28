using System;
using System.Linq;
using UnityEngine;

public static class TraitFactory
{
    public static UnitTrait CreateTrait(string traitName, GameObject owner)
    {
        // 유효한 타입 이름 (네임스페이스 없다고 가정)
        Type type = Type.GetType(traitName);
        if (type == null)
        {
            // 네임스페이스가 있을 경우 처리 (선택)
            type = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .FirstOrDefault(t => t.Name == traitName && typeof(UnitTrait).IsAssignableFrom(t));
        }

        if (type != null && typeof(UnitTrait).IsAssignableFrom(type))
        {
            return owner.AddComponent(type) as UnitTrait;
        }

        Debug.LogWarning($"[TraitFactory] '{traitName}' 트레잇 클래스를 찾을 수 없습니다.");
        return null;
    }
}
