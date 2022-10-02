using DG.Tweening;
using SemihCelek.TenToDeal.HealthModule.Controller;
using SemihCelek.TenToDeal.HealthModule.Model;
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
        private int _healthEntityId;
        
        private readonly Quaternion _transformRotation = Quaternion.Euler(30f, 45f, 0f);

        public void SetupProgressBar(ProgressBarData progressBarData)
        {
            _minValue = progressBarData.minValue;
            _maxValue = progressBarData.maxValue;
            _currentValue = progressBarData.currentValue;
            _healthEntityId = progressBarData.healthEntityId;
            
            AdjustProgressBar(_currentValue, false);

            ListenEvents();
        }

        private void ListenEvents()
        {
            HealthController.OnDealDamage += OnDealDamage;
        }

        private void OnDealDamage(IHealthEntity healthEntity, int damageAmount)
        {
            if (healthEntity.HealthAssetData.id == _healthEntityId)
            {
                AdjustProgressBar(_currentValue - damageAmount);
            }
        }

        private void LateUpdate()
        {
            KeepProgressBarRotation();
        }

        private void KeepProgressBarRotation()
        {
            transform.rotation = _transformRotation;
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
        public readonly int healthEntityId;
        public readonly float minValue;
        public readonly float maxValue;
        public readonly float currentValue;

        public ProgressBarData(float minValue, float maxValue, float currentValue, int healthEntityId)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.currentValue = currentValue;
            this.healthEntityId = healthEntityId;
        }
    }
}