using System;
using Cysharp.Threading.Tasks;
using SemihCelek.TenToDeal.CombatModule.Controller;
using SemihCelek.TenToDeal.CombatModule.Model;
using SemihCelek.TenToDeal.HealthModule.View;
using SemihCelek.TenToDeal.Model;
using SemihCelek.TenToDeal.Utilites;
using UnityEngine;

namespace SemihCelek.TenToDeal.CombatModule.View
{
    public class WeaponView : MonoBehaviour, IInteractable, IView
    {
        public int id => _weaponAssetData.id; 
        
        public bool IsInteractable { get; }

        private CombatController _combatController;

        private Collider _weaponDamagingCollider;
        
        [SerializeField] 
        private WeaponAssetData _weaponAssetData;

        private HealthView _targetHealthView;

        private void Start()
        {
            InitializeDependencies();
        }

        private void InitializeDependencies()
        {
            _combatController = FindObjectOfType<CombatController>();
            _weaponDamagingCollider = GetComponentInChildren<Collider>();
        }

        private void SetWeaponTakenView(GameObject takingObject)
        {
            Transform gameObjectTransform = gameObject.transform;
            
            gameObjectTransform.SetParent(takingObject.transform);
            gameObjectTransform.localPosition = _weaponAssetData.weaponPosition;
        }

        public void Interact(GameObject interactor)
        {
            SetWeaponTakenView(interactor);
        }

        public async UniTask UseAsync()
        {
            _targetHealthView = null;
            await _weaponAssetData.PlayWeaponAnimation(gameObject);
            TryAttackUsingWeaponCollision();
        }

        private void TryAttackUsingWeaponCollision()
        {
            if (_targetHealthView != null)
            {
                _combatController.Attack(20, _targetHealthView);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out HealthView healthView))
            {
                _targetHealthView = healthView;
                return;
            }

            DisposeHealthViewAsync().Forget();
        }

        private async UniTaskVoid DisposeHealthViewAsync()
        {
            await UniTaskHelper.Delay(0.2f);
        }
    }
}