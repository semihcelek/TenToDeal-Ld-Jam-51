using System;
using SemihCelek.TenToDeal.HealthModule.Controller;
using SemihCelek.TenToDeal.HealthModule.Model;
using SemihCelek.TenToDeal.HealthModule.View;
using SemihCelek.TenToDeal.Model;
using SemihCelek.TenToDeal.Utilites;
using UnityEngine;

namespace SemihCelek.TenToDeal.CombatModule.Controller
{
    public class CombatController : MonoBehaviour, IController
    {
        private HealthController _healthController;

        private void Start()
        {
            _healthController = FindObjectOfType<HealthController>();
        }

        public void Attack(int damageAmount, HealthView healthView)
        {
            _healthController.DealDamage(healthView.Cast<IHealthEntity>(), damageAmount);
            Debug.Log(damageAmount);
        }
    }
}