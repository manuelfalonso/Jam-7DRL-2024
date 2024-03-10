using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

namespace JAM.Audio
{
    [System.Serializable]
    public class SFXStringDictionary : SerializableDictionaryBase<string, SFXRack>
    {
        #region Public Methods
        public virtual void InstantiateAll(Transform t)
        {
            foreach (var rack in this)
            {
                rack.Value.InstantiateEventInstance(t);
                if (rack.Value.Instance.playOnAwake)
                {
                    rack.Value.Instance.Play();
                }
            }
        }

        public virtual void Play(string audio)
        {
            if (TryGetValue(audio, out var rack))
            {
                rack.Instance.Play();
            }
        }

        public virtual void Stop(string audio)
        {
            if (TryGetValue(audio, out var rack))
            {
                rack.Instance.Stop();
            }
        }

        public virtual void StopAllInstances()
        {
            foreach (var rack in this)
            {
                rack.Value.Instance.Stop();
            }
        }
        #endregion
    }
}