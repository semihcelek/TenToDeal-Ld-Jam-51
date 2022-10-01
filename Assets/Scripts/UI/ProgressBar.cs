using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SemihCelek.TenToDeal.UI
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField]
        private Image _fillImage;

        [SerializeField] 
        private TMP_Text _percentageText; 

        private float _minValue;
        private float _maxValue;
        private float _currentValue;

        public void SetupProgressBar(ProgressBarData progressBarData)
        {
            _minValue = progressBarData.minValue;
            _maxValue = progressBarData.maxValue;
            _currentValue = progressBarData.currentValue;
            
            AdjustProgressBar(_currentValue, false);
        }

        private void AdjustProgressBar(float currentValue, bool animate = true)
        {
            float animationDuration = animate ? 0.75f : 0f;

            float percentage = currentValue / (_maxValue - _minValue);
            _fillImage.DOFillAmount(percentage, animationDuration).SetEase(Ease.InOutSine);

            _percentageText.text = currentValue.ToString();
        }
    }

    public readonly struct ProgressBarData
    {
        public readonly float minValue;
        public readonly float maxValue;
        public readonly float currentValue;

        public ProgressBarData(float minValue, float maxValue, float currentValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.currentValue = currentValue;
        }
    }
}