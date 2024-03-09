using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace JAM.Stats
{
    [System.Serializable]
    public class FloatStat : Stat<float, FloatStatModifier>, IAdditiveStat<float>
    {
        public FloatStat(float value, string name) : base(value, name) { }

        public float AddValues(float value1, float value2) => value1 + value2;

        protected override void CalculateValue()
        {
            _modifiedValue = _baseValue;
            if (_statModifiers.Count == 0) { return; }

            _statModifiers.Sort((x, y) => x.ModType.CompareTo(y.ModType));

            for (int i = 0; i < _statModifiers.Count; i++)
            {
                if (_statModifiers[i].ModType == ModifierOperationType.Additive)
                {
                    _modifiedValue += _statModifiers[i].Value;
                }
                else if (_statModifiers[i].ModType == ModifierOperationType.Multiplicative)
                {
                    _modifiedValue += (_modifiedValue * _statModifiers[i].Value) / 100;
                }
            }
        }
    }
}
//EOF.