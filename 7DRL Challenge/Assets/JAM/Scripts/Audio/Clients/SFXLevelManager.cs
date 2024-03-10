using System;
using NaughtyAttributes;
using UnityEngine;
using Utils.Singleton;

namespace JAM.Audio
{
    public class SFXLevelManager : MonoBehaviourSingleton<SFXLevelManager>
    {
        #region Private Variables
        [SerializeField] private bool _useGeneralParameters;
        [SerializeField] private SFXScriptableData _levelData;
        [Header("General Parameters")]
        [SerializeField, Range(0, 1)] private float _volume = 1f;
        [SerializeField, Range(0, 1)] private float _dialogueVolume = 1f;
        [SerializeField] private bool _muteOnStart;
        [SerializeField] private bool _setVolumesOnStart;
        #endregion


        #region Monobehaviour
        private void Start()
        {
            InitializeRackInstances();

            if (_setVolumesOnStart)
            {
                SetRackVolume(_volume);
                SetDialogueVolume(_dialogueVolume);
            }

            if (_muteOnStart)
            {
                SetRackMute(_muteOnStart);
            }

            //PlayOneShotAudio("GamePlay", Vector3.zero);
        }
        #endregion

        
        #region Public
        public void PlayOneShotAudio(string audioEvent, Vector3 pos)
        {
            AudioSource.PlayClipAtPoint(_levelData.SFXRack[audioEvent].Data.Clip, pos);
        }
        #endregion

        
        #region Private Methods
        private void SetRackVolume(float volume)
        {
            if(!_useGeneralParameters) { return; }
            
            foreach (var rack in _levelData.SFXRack)
            {
                rack.Value.Instance.volume = volume;
            }
        }

        private void SetDialogueVolume(float volume)
        {
            if(!_useGeneralParameters) { return; }
            
            foreach (var rack in _levelData.VoiceRack)
            {
                rack.Value.Instance.volume = volume;
            }
        }

        private void SetRackMute(bool muteOnStart)
        {
            if(!_useGeneralParameters) { return; }
            
            foreach (var rack in _levelData.SFXRack)
            {
                rack.Value.Instance.mute = muteOnStart;
            }
        }

        private void InitializeRackInstances()
        {
            if (_levelData == null) { return; }
            
            _levelData.SFXRack.InstantiateAll(transform);
            _levelData.VoiceRack.InstantiateAll(transform);
        }
        #endregion


        #region Buttons
        [Button]
        public void SetVolume() => SetRackVolume(_volume);

        public void MuteVolume() => SetRackMute(true);

        [Button]
        public void UnmuteVolume() => SetRackMute(false);
        #endregion
    }
}
//EOF