using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JAM.Stats
{
    [CreateAssetMenu(fileName = "New Stat Sheet", menuName = "JAM/Stat Sheet")]
    public class StatSheet : ScriptableObject
    {
        //[SerializeField] private Sprite _sprite;
        [SerializeField] private List<FloatStat> _floatStats = new List<FloatStat>();
        [SerializeField] private List<IntStat> _intStats = new List<IntStat>();
        [SerializeField] private List<BoolStat> _boolStats = new List<BoolStat>();

        //public DerivedStat<int, StatModifier<int>> MyDerivedStat;

        public List<IBaseStat> Stats => _floatStats.Cast<IBaseStat>()
            .Concat(_intStats.Cast<IBaseStat>())
            .Concat(_boolStats.Cast<IBaseStat>())
            .ToList();

        public static StatSheet Instance { get; private set; }
        private void OnEnable() => Instance = this;

        public void AddStat(IBaseStat stat)
        {
            switch (stat)
            {
                case FloatStat floatStat:
                    _floatStats.Add(floatStat);
                    break;
                case IntStat intStat:
                    _intStats.Add(intStat);
                    break;
                case BoolStat boolStat:
                    _boolStats.Add(boolStat);
                    break;
            }
        }

        public void RemoveStat(IBaseStat stat)
        {
            switch (stat)
            {
                case FloatStat floatStat:
                    _floatStats.Remove(floatStat);
                    break;
                case IntStat intStat:
                    _intStats.Remove(intStat);
                    break;
                case BoolStat boolStat:
                    _boolStats.Remove(boolStat);
                    break;
            }
        }
    }
}
//EOF.