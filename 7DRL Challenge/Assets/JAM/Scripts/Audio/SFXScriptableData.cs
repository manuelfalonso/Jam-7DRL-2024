using System;
using UnityEngine;

namespace BossRushJam2024.Audio
{
    [Serializable]
    [CreateAssetMenu(fileName = "New SFX Scriptable Data", menuName = "BossRushJam2024/Audio/SFX Scriptable Data")]
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
