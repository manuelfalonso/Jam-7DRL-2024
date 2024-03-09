using UnityEngine;

namespace JAM.Utilities
{
    /// <summary>
    /// Limit FPS to optimize performance to the maximum refresh rate of the monitor
    /// This script don't need to and can't be attached to any Game Object
    /// </summary>
    public static class FrameRateLimiter
    {
        [RuntimeInitializeOnLoadMethod]
        public static void LimitFrameRate()
        {
            Application.targetFrameRate = 60;
        }
    }
}
