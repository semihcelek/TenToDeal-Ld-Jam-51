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

        public int CurrentHealth { get; set; }
        
        private HealthController _healthController;

        private ProgressBar _healthProgressBar;

        private void Start()
        {
            InitializeDependencies();

            CurrentHealth = _healthAssetData.maxHealth;
            SetupHealthProgressBar();
        }

        private void InitializeDependencies()
        {
            _healthController = FindObjectOfType<HealthController>();
            _healthProgressBar = GetComponentInChildren<ProgressBar>();
        }
        
        private void SetupHealthProgressBar()
        {
            ProgressBarData progressBarData =
                new ProgressBarData(0, HealthAssetData.maxHealth, CurrentHealth, _healthAssetData.id);
            
            _healthProgressBar.SetupProgressBar(progressBarData);
        }

        public void GiveHealthDamage(int damageAmount)
        {
            _healthController.DealDamage(this, damageAmount);
            Debug.Log("damage is given");
        }
    }
}