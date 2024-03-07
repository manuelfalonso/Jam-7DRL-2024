using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace JAM.Shared.Systems.Resource
{
    public class ResourceSystemView : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Slider _healthBarSlider;
        [SerializeField] private Image _healthBarFill;

        [Header("Config")]
        [SerializeField] private float _sliderAnimationTime;
        [SerializeField] private Ease _sliderAnimationEase;

        [Header("Debug")]
        [SerializeField] private bool _showLogs;

        public float MinLife { get; set; }
        public float MaxLife { get; set; }


        public void UpdateHealthBar(float value)
        {
            if (_healthBarSlider == null) { return; }

            //if (value < 0f || value > 1f) { Debug.LogError($"Health new value is not normalized", this); }
            var normalizedValue = Mathf.InverseLerp(MinLife, MaxLife, value);

            if (_showLogs) { Debug.Log($"value: {value} - normalizedValue: {normalizedValue}"); }

            //_healthBarSlider.value = normalizedValue;
            // Animate the slider value
            DOTween.To(
                () => _healthBarSlider.value, x => _healthBarSlider.value = x, normalizedValue, _sliderAnimationTime)
                .SetEase(_sliderAnimationEase);
        }

        public void HideFillImageOnDeath(float value)
        {
            if (value <= 0f)
            {
                _healthBarFill.enabled = false;
            }
            else
            {
                _healthBarFill.enabled = true;
            }
        }
    }
}
