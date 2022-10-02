using SemihCelek.TenToDeal.HealthModule.Controller;
using SemihCelek.TenToDeal.HealthModule.Model;
using SemihCelek.TenToDeal.Model;
using SemihCelek.TenToDeal.UI;
using UnityEngine;

namespace SemihCelek.TenToDeal.HealthModule.View
{
    public class HealthView : MonoBehaviour, IHealthEntity ,IView
    {
        [SerializeField] 
        private HealthAssetData _healthAssetData;

        public HealthAssetData HealthAssetData => _healthAssetData;

        private HealthController _healthController;

        private ProgressBar _healthProgressBar;

        private void Start()
        {
            InitializeDependencies();

            SetupHealthProgressBar();
        }

        private void SetupHealthProgressBar()
        {
            ProgressBarData progressBarData =
                new ProgressBarData(0, HealthAssetData.maxHealth, HealthAssetData.currentHealth);
            
            _healthProgressBar.SetupProgressBar(progressBarData);
        }

        private void InitializeDependencies()
        {
            _healthController = FindObjectOfType<HealthController>();
            _healthProgressBar = GetComponentInChildren<ProgressBar>();
        }

        public void GiveHealthDamage(int damageAmount)
        {
            _healthController.DealDamage(this, damageAmount);
            Debug.Log("damage is given");
        }
    }
}