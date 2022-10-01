using System;
using System.Reflection;
using SemihCelek.TenToDeal.CombatModule.Model;
using SemihCelek.TenToDeal.Model;
using UnityEngine;

namespace SemihCelek.TenToDeal.CombatModule.View
{
    public class WeaponView : MonoBehaviour, ICommandAcquirable, IView
    {
        [SerializeField] 
        private WeaponAssetData _weaponAssetData;

        public Command GetCommand()
        {
            Command command = _weaponAssetData.weaponType switch
            {
                WeaponAssetData.WeaponType.SwordCommand => new SwordCommand()
            };

            return command;
        }
    }
}