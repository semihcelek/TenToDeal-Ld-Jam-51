using System;
using SemihCelek.TenToDeal.HealthModule.Controller;
using SemihCelek.TenToDeal.HealthModule.Model;
using SemihCelek.TenToDeal.Model;
using UnityEngine;

namespace SemihCelek.TenToDeal.HealthModule.View
{
    public class HealthView : MonoBehaviour, IHealthEntity ,IView
    {
        [SerializeField]
        public HealthAssetData HealthAssetData { get; }

        private HealthController _healthController;

        private void Start()
        {
            InitializeDependencies();
        }

        private void InitializeDependencies()
        {
            _healthController = FindObjectOfType<HealthController>();
        }

        public void GiveHealthDamage(int damageAmount)
        {
            _healthController.DealDamage(this, damageAmount);
            Debug.Log("damage is given");
        }
    }
}