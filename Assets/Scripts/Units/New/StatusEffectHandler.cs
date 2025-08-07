using UnityEngine;
using System.Collections.Generic;

public class StatusEffectHandler : MonoBehaviour
{
    private List<IStatusEffect> _statusEffects = new List<IStatusEffect>();

    private void Awake()
    {
        LoadStatusEffects();
    }

    private void LoadStatusEffects()
    {
        _statusEffects.Clear();
        _statusEffects.AddRange(GetComponents<IStatusEffect>());
    }

    public void OnTurnStart()
    {
        foreach (var effect in _statusEffects)
            effect.OnTurnStart();
    }

    public void OnTurnEnd()
    {
        foreach (var effect in _statusEffects)
            effect.OnTurnEnd();
    }

    public void OnBeforeTakeDamage(ref int damage)
    {
        foreach (var effect in _statusEffects)
            effect.OnBeforeTakeDamage(ref damage);
    }

    public void OnAfterTakeDamage()
    {
        foreach (var effect in _statusEffects)
            effect.OnAfterTakeDamage();
    }

    public void AddStatus(IStatusEffect effect)
    {
        var component = effect as Component;
        if (component != null && !_statusEffects.Contains(effect))
        {
            _statusEffects.Add(effect);
            effect.OnApply();
        }
    }

    public void RemoveStatus(IStatusEffect effect)
    {
        var component = effect as Component;
        if (component != null && _statusEffects.Contains(effect))
        {
            _statusEffects.Remove(effect);
            effect.OnRemove();
            Destroy(component);
        }
    }

    public bool HasStatus<T>() where T : IStatusEffect
    {
        foreach (var effect in _statusEffects)
        {
            if (effect is T) return true;
        }
        return false;
    }
}
