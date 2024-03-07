using System.Collections.Generic;
using UnityEngine;

namespace JAM.Stats
{
    //[System.Serializable]
    //public class DerivedStat<T, TModifier> : Stat<T, TModifier> where TModifier : StatModifier<T>
    //{
    //    [SerializeField]
    //    public List<Stat<T, StatModifier<T>>> _baseStats = new List<Stat<T, StatModifier<T>>>();

    //    [SerializeField]
    //    public List<IBaseStat> BaseStatsTest = new List<IBaseStat>();

    //    public DerivedStat(string name, List<Stat<T, StatModifier<T>>> baseStats) : base()
    //    {
    //        _baseStats = baseStats;
    //        CalculateValue();
    //    }

    //    protected override void CalculateValue()
    //    {
    //        // Calculate the derived value based on base stats
    //        T modifiedValue = _baseValue;
    //        foreach (var baseStat in _baseStats)
    //        {
    //            if (baseStat is not IAdditiveStat<T> additiveStat) { continue; }

    //            // Safely cast and add the value if it's an IAdditiveStat<T>
    //            modifiedValue = AdditiveStatExtensions.Add<T>(additiveStat, modifiedValue);
    //        }

    //        _modifiedValue = modifiedValue;
    //    }
    //}

}
//EOF.