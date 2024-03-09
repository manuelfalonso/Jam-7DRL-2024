using System;
using UnityEngine;

namespace JAM.Audio
{
    [Serializable]
    public class SFXRack
    {
        #region Private Variables
        [SerializeField] private RackData _data;
        private AudioSource _instance;
        #endregion
        
        #region Properties
        public AudioSource Instance => _instance;
        public RackData Data => _data;
        #endregion

        #region Constructor

        public SFXRack(RackData dataRef)
        {
            _data = dataRef;
        }

        #endregion
        
        #region Public Methods

        public void InstantiateEventInstance(Transform transform)
        {
            _instance = transform.gameObject.AddComponent<AudioSource>();
            _instance.clip = _data.Clip;
            _instance.volume = _data.Volume;
            _instance.playOnAwake = _data.PlayOnAwake;
            _instance.loop = _data.Loop;
            _instance.spatialBlend = _data.SpatialBlend;
            _instance.pitch = _data.Pitch;
        }

        #endregion
    }

    [Serializable]
    public struct RackData
    {
        public AudioClip Clip;
        [Range(0,1)] public float Volume;
        [Range(-3,3)] public float Pitch;
        [Range(0,1)] public float SpatialBlend;
        public bool PlayOnAwake;
        public bool Loop;
    }
}