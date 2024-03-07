using System;

namespace JAM.Stats
{
    public static class StatFactory
    {
        public static IBaseStat CreateStat(IBaseStat statData)
        {
            return statData.GetType().Name switch
            {
                nameof(FloatStat) => new FloatStat((statData as IStat<float>)?.GetBaseValue() ?? default, statData.Name),
                nameof(IntStat) => new IntStat((statData as IStat<int>)?.GetBaseValue() ?? default, statData.Name),
                nameof(BoolStat) => new BoolStat((statData as IStat<bool>)?.GetBaseValue() ?? default, statData.Name),
                _ => throw new InvalidOperationException($"Could not create a stat instance for type {statData.GetType().Name}"),
            };
        }
    }
}
//EOF.