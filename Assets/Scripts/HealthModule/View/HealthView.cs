using SemihCelek.TenToDeal.HealthModule.Controller;
using SemihCelek.TenToDeal.HealthModule.Model;
using SemihCelek.TenToDeal.LevelModule.View;
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

        public TaskObjectView taskObjectView;

        public int CurrentHealth { get; set; }
        
        private ProgressBar _healthProgressBar;

        private void Start()
        {
            InitializeDependencies();

            CurrentHealth = _healthAssetData.maxHealth;
            SetupHealthProgressBar();
        }

        private void InitializeDependencies()
        {
            _healthProgressBar = GetComponentInChildren<ProgressBar>();
            taskObjectView = GetComponent<TaskObjectView>();
        }
        
        private void SetupHealthProgressBar()
        {
            ProgressBarData progressBarData =
                new ProgressBarData(0, HealthAssetData.maxHealth, CurrentHealth, taskObjectView.id);
            
            _healthProgressBar.SetupProgressBar(progressBarData);
        }
    }
}