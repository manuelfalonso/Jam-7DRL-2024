using System.Collections.Generic;
using System.Linq;

namespace JAM.Stats
{
    [System.Serializable]
    public class BoolStat : Stat<bool, BoolStatModifier>
    {
        public BoolStat(bool value, string name) : base(value, name) { }

        protected override void CalculateValue()
        {
            _modifiedValue = _statModifiers.Count > 0 ? _statModifiers.Last().Value : _baseValue;
        }
    }
}
//EOF.