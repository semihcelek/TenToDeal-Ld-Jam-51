using UnityEngine;

namespace SemihCelek.TenToDeal.Model
{
    [CreateAssetMenu(fileName = "New Player Settings", menuName = "SemihCelek/TenToDeal/PlayerSettings", order = 0)]
    public class PlayerSettings : ScriptableObject
    {
        public float speed;
    }
}