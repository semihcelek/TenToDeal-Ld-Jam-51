using SemihCelek.TenToDeal.UI;
using UnityEngine;

namespace SemihCelek.TenToDeal.View.Model
{
    [CreateAssetMenu(fileName = "New Timer Sprite Data", menuName = "SemihCelek/TenToDeal/TimerSpriteAssetData", order = 0)]
    public class TimerBackgroundSpriteAssetData : ScriptableObject
    {
        public Sprite sprite;

        public TimerColors color;
    }
}