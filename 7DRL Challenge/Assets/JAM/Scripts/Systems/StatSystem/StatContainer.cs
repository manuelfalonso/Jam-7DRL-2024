using System.Collections.Generic;
using UnityEngine;

namespace JAM.Stats
{
    [System.Serializable]
    public class StatContainer
    {
        private Dictionary<string, IBaseStat> _stats = new Dictionary<string, IBaseStat>();
        private StatSheet _myStatSheet;

        public Dictionary<string, IBaseStat> Stats => _stats;

        public StatContainer(StatSheet statSheet)
        {
            Initialize(statSheet);
        }
        
        public void Initialize(StatSheet statSheet)
        {
            _myStatSheet = ScriptableObject.Instantiate(statSheet);
            foreach (var stat in _myStatSheet.Stats)
            {
                IBaseStat newStat = StatFactory.CreateStat(stat);
                _stats.Add(newStat.Name, newStat);
                //Debug.Log("Adding " + stat.Name);
            }
        }

        public bool TryGetValue(string statName, out dynamic value)
        {
            value = null;

            if (_stats.TryGetValue(statName, out var stat))
            {
                value = stat;
                return true;
            }

            return false;
        }

        //public void ListAllStats()
        //{
        //    Debug.Log("Dictionary elements: " + _stats.Count);
        //    foreach(KeyValuePair<string, IBaseStat> kvp in _stats)
        //    {
        //        Debug.Log($"{kvp.Key}, {kvp.Value.Name}");
        //        if (kvp.Value is not FloatStat) { continue; }
        //        var floatStat = (FloatStat)kvp.Value;
        //        Debug.Log($"Value: {floatStat.GetValue()}");
        //    }
        //}
    }
}
//EOF.