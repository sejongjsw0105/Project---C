// TraitFactory.cs
using System;
using System.Collections.Generic;
using UnityEngine;

public static class TraitFactory
{
    private static Dictionary<string, Type> traitMap = new Dictionary<string, Type>
    {
        { "SuccesiveFiring", typeof(SuccesiveFiring) },
        { "Pressure", typeof(Pressure) },
        { "Reload", typeof(Reload) },
        { "Fire", typeof(Fire) },
        { "Fear", typeof(Fear) },
        { "Cover", typeof(Cover) },
        { "CounterAttack", typeof(CounterAttack) },
        { "CloseRangeShooting", typeof(CloseRangeShooting) },
        { "Sniping", typeof(Sniping) },
        { "PsychologicalWarfare", typeof(PsychologicalWarfare) },
        { "Surprise", typeof(Surprise) },
        { "Shielded", typeof(Shielded) },
        { "Armored", typeof(Armored) },
        { "Heavy", typeof(Heavy) },
    };

    public static UnitTrait CreateTrait(string name)
    {
        if (traitMap.TryGetValue(name, out Type traitType))
        {
            GameObject temp = new GameObject("Trait_" + name);
            return (UnitTrait)temp.AddComponent(traitType);
        }
        Debug.LogWarning($"[TraitFactory] Unknown trait: {name}");
        return null;
    }
}
