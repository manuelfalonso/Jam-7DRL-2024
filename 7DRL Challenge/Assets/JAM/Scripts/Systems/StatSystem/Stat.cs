using System;
using System.Collections.Generic;
using UnityEngine;

namespace JAM.Stats
{
    [System.Serializable]
    public abstract class Stat<T, TModifier> : IStat<T> where TModifier : StatModifier<T>
    {
        [SerializeField] protected string _name;
        [SerializeField] protected T _baseValue;
        [SerializeField] protected List<TModifier> _statModifiers;

        protected T _modifiedValue;
        public Action<T> StatModified;

        protected Stat(T value, string name)
        {
            _name = name;
            _baseValue = value;
            _statModifiers = new List<TModifier>();
        }

        public virtual string Name => _name;
        public virtual void AddModifier(TModifier modifier) 
        {
            //Debug.Log($"Adding modifier value: {modifier.Value} to {this.ToString()}");
            _statModifiers.Add(modifier);
            CalculateValue();
            StatModified?.Invoke(_modifiedValue);
        }

        public virtual void RemoveModifier(TModifier modifier)
        {
            if (!_statModifiers.Contains(modifier)) { return; }

            //Debug.Log($"Removing modifier value: {modifier.Value} to {this.ToString()}");
            _statModifiers.Remove(modifier);
            CalculateValue();
            StatModified?.Invoke(_modifiedValue);
        }

        public virtual T GetBaseValue() => _baseValue;

        public virtual T GetValue()
        {
            return _statModifiers.Count == 0 || _statModifiers == null ? _baseValue : _modifiedValue;
        }
        protected abstract void CalculateValue();
    }

    public interface IBaseStat
    {
        string Name { get; }
    }
    
    public enum StatType
    {
        Float,
        Int,
        Bool,
        DerivedStat
    }

    public interface IStat<T> : IBaseStat
    {
        T GetValue();
        T GetBaseValue();
    }

    public interface IAdditiveStat<T>
    {
        T AddValues(T value1, T value2);
    }
}
//EOF.