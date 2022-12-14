using System;
using UnityEngine;

namespace SemihCelek.TenToDeal.HealthModule.Model
{
    [CreateAssetMenu(fileName = "New Health Asset Data", menuName = "SemihCelek/TenToDeal/HealthAssetData", order = 0)]
    public class HealthAssetData : ScriptableObject
    {
        public int id;
        public int maxHealth;
    }
}