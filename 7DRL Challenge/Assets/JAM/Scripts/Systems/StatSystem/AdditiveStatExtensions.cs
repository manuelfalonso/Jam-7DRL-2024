namespace JAM.Stats
{
    public static class AdditiveStatExtensions
    {
        public static T Add<T>(this IAdditiveStat<T> stat, T other) => stat.Add(other);
        public static float Add(this IAdditiveStat<float> stat, int other) => stat.Add((float)other);
        public static float Add(this IAdditiveStat<float> stat, float other) => stat.Add(other);
        public static int Add(this IAdditiveStat<int> stat, float other) => stat.Add((int)other);
    }
}
//EOF.