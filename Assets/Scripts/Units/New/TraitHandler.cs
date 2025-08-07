using UnityEngine;
using System.Collections.Generic;

public class TraitHandler : MonoBehaviour
{
    private List<IUnitTrait> _traits = new List<IUnitTrait>();

    private void Awake()
    {
        LoadTraits();
    }

    private void LoadTraits()
    {
        _traits.Clear();
        _traits.AddRange(GetComponents<IUnitTrait>());
    }

    public void OnAction(UnitActionType action)
    {
        foreach (var trait in _traits)
        {
            trait.OnAction(action);
        }
    }

    public void OnAfterDamaged()
    {
        foreach (var trait in _traits)
        {
            trait.OnAfterDamaged();
        }
    }

    public void AddTrait(IUnitTrait trait)
    {
        var traitComponent = trait as Component;
        if (traitComponent != null && !_traits.Contains(trait))
        {
            _traits.Add(trait);
        }
    }

    public void RemoveTrait(IUnitTrait trait)
    {
        var traitComponent = trait as Component;
        if (traitComponent != null && _traits.Contains(trait))
        {
            _traits.Remove(trait);
            Destroy(traitComponent);
        }
    }
    public void OnKill(Unit target)
    {
        foreach (var trait in _traits)
        {
            trait.OnKill(target);
        }
    }
    public bool HasTrait<T>() where T : IUnitTrait
    {
        foreach (var trait in _traits)
        {
            if (trait is T) return true;
        }
        return false;
    }
}
