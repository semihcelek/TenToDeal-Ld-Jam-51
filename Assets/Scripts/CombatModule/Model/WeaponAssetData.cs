using SemihCelek.TenToDeal.Model;
using UnityEngine;

namespace SemihCelek.TenToDeal.CombatModule.Model
{
    [CreateAssetMenu(fileName = "New Weapon Asset Data", menuName = "SemihCelek/TenToDeal/WeaponAssetData", order = 0)]
    public class WeaponAssetData : ScriptableObject
    {
        public Sprite weaponIconSprite;
        
        public int damageAmount;

        public WeaponType weaponType;
        
        public void WeaponAnimation()
        {
            
        }

        public enum WeaponType
        {
            KnifeCommand,
            SwordCommand,
            ArrowCommand
        }
    }
}