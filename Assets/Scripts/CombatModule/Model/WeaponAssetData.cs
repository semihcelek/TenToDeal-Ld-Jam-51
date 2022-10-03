using Cysharp.Threading.Tasks;
using SemihCelek.TenToDeal.Model;
using UnityEngine;

namespace SemihCelek.TenToDeal.CombatModule.Model
{
    [CreateAssetMenu(fileName = "New Weapon Asset Data", menuName = "SemihCelek/TenToDeal/WeaponAssetData", order = 0)]
    public abstract class WeaponAssetData : ScriptableObject
    {
        public int id;

        public int damageAmount;
        
        public Sprite weaponIconSprite;
        
        public Vector3 weaponPosition;

        public abstract UniTask PlayWeaponAnimation(GameObject weaponGameObject);
    }
}