using UnityEngine;
using JAM.Audio;

namespace JAM.Utils
{
    //TODO: Review calling methods with string parameters, it's not the best way to do it.
    public class SFXEntityHelper : MonoBehaviour
    {
        #region Private Variables
        [SerializeField] private SFXScriptableData _audioData;
        #endregion
        

        #region MonoBehaviour Callbacks
        private void Awake()
        {
            if (_audioData == null) { return; }

            // Creating a runtime version so it wont be overwritten for each entity.
            _audioData = Instantiate(_audioData);
        }

        private void Start()
        {
            if (_audioData == null) return;

            _audioData.SFXRack.InstantiateAll(transform);
            _audioData.VoiceRack.InstantiateAll(transform);
        }
        #endregion


        #region Public Methods
        public void SFX_PlayAudio(AnimationEvent animationEvent)
        {
            PlayAudio(animationEvent.stringParameter);
        }

        public void SFX_PlayAudio(string key)
        {
            PlayAudio(key);
        }

        public void SFX_PlayVoice(AnimationEvent animationEvent)
        {
            PlayVoice(animationEvent.stringParameter);
        }

        public void SFX_PlayVoice(string key)
        {
            PlayVoice(key);
        }

        public void SFX_PlayLoopAudio(string key)
        {
            PlayLoopAudio(key);
        }

        public void SFX_StopLoopAudio(string key)
        {
            StopLoopAudio(key);
        }
        #endregion


        #region Private Methods
        private void PlayAudio(string audio)
        {
            if (_audioData == null) return;

            _audioData.SFXRack.Play(audio);
        }

        private void PlayVoice(string voice)
        {
            if (_audioData == null) return;

            _audioData.VoiceRack.Play(voice);
        }

        private void PlayLoopAudio(string audio)
        {
            if (_audioData == null) return;
            
            _audioData.SFXRack[audio].Instance.loop = true;
            _audioData.SFXRack.Play(audio);
        }

        private void StopLoopAudio(string audio)
        {
            if (_audioData == null) return;

            _audioData.SFXRack.Stop(audio);
        }
        #endregion
    }
}
//EOF.