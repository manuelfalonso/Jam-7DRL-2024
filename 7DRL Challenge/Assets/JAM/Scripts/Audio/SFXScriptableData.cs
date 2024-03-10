using System;
using UnityEngine;

namespace JAM.Audio
{
    [Serializable]
    [CreateAssetMenu(fileName = "New SFX Scriptable Data", menuName = "JAM/Audio/SFX Scriptable Data")]
    public class SFXScriptableData : ScriptableObject
    {
        #region Private Variables
        [SerializeField] private SFXStringDictionary _sfxRack = new();
        [SerializeField] private SFXInterruptableDictionary _voiceRack = new();
        #endregion


        #region Properties
        public SFXStringDictionary SFXRack => _sfxRack;
        public SFXInterruptableDictionary VoiceRack => _voiceRack;
        #endregion
    }
}
