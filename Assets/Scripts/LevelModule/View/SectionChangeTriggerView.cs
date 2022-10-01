using SemihCelek.TenToDeal.Model;
using SemihCelek.TenToDeal.View;
using UnityEngine;

namespace SemihCelek.TenToDeal.LevelModule.View
{
    public class SectionChangeTriggerView : MonoBehaviour, IView
    {
        private void OnTriggerEnter(Collider other)
        {
            bool isPlayerGameObject = other.gameObject.TryGetComponent(out PlayerView playerView);
            
            if (isPlayerGameObject)
            {
            }
        }
    }
}