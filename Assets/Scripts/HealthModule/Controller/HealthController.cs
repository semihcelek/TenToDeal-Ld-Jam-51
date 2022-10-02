using System;
using SemihCelek.TenToDeal.HealthModule.Model;
using SemihCelek.TenToDeal.Model;
using UnityEngine;

namespace SemihCelek.TenToDeal.HealthModule.Controller
{
    public class HealthController : MonoBehaviour, IController
    {
        public static event Action<IHealthEntity> OnHealthEntityDied;
        public static event Action<IHealthEntity, int> OnDealDamage;

        public void DealDamage(IHealthEntity healthEntity, int dealAmount)
        {
            HealthAssetData healthAssetData = healthEntity.HealthAssetData;

            int currentHealth = healthAssetData.currentHealth - dealAmount;

            healthAssetData.currentHealth = currentHealth;
            
            if (currentHealth <= 0)
            {
                OnHealthEntityDied?.Invoke(healthEntity);
            }
            
            OnDealDamage?.Invoke(healthEntity, dealAmount);
        }
    }
}