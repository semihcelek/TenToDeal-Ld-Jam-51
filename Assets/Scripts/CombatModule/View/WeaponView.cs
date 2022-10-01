using SemihCelek.TenToDeal.CombatModule.Model;
using SemihCelek.TenToDeal.Model;
using UnityEngine;

namespace SemihCelek.TenToDeal.CombatModule.View
{
    public class WeaponView : MonoBehaviour, IInteractable, IView
    {
        public bool IsInteractable { get; }
        
        [SerializeField] 
        private WeaponAssetData _weaponAssetData;
        
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

        public void Use()
        {
            _weaponAssetData.PlayWeaponAnimation(gameObject);
        }
    }
}