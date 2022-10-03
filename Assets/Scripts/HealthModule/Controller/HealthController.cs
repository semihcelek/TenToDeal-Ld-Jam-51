using System;
using SemihCelek.TenToDeal.HealthModule.Model;
using SemihCelek.TenToDeal.HealthModule.View;
using SemihCelek.TenToDeal.Model;
using UnityEngine;

namespace SemihCelek.TenToDeal.HealthModule.Controller
{
    public class HealthController : MonoBehaviour, IController
    {
        public static event Action<HealthView> HealthEntityDiedEvent;
        public static event Action<HealthView, int> OnDealDamage;

        public void DealDamage(HealthView healthView, int dealAmount)
        {
            int currentHealth = healthView.CurrentHealth - dealAmount;

            healthView.CurrentHealth = currentHealth;
            
            if (currentHealth <= 0)
            {
                HealthEntityDiedEvent?.Invoke(healthView);
            }
            
            OnDealDamage?.Invoke(healthView, dealAmount);
        }
    }
}