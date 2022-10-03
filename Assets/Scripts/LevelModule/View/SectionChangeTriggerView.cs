using SemihCelek.TenToDeal.Controller;
using SemihCelek.TenToDeal.Model;
using SemihCelek.TenToDeal.View;
using UnityEngine;

namespace SemihCelek.TenToDeal.LevelModule.View
{
    public class SectionChangeTriggerView : MonoBehaviour, IView
    {
        [SerializeField] 
        private int sectionId;

        [SerializeField]
        private bool _isSectionStart = true;

        private SectionController _sectionController;
        private void Start()
        {
            InitializeDependencies();
        }

        private void InitializeDependencies()
        {
            _sectionController = FindObjectOfType<SectionController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            bool isPlayerGameObject = other.gameObject.TryGetComponent(out PlayerView playerView);

            if (!isPlayerGameObject)
            {
                return;
            }

            if (_isSectionStart)
            {
                _sectionController.StartSection(sectionId);
            }
            else
            {
                _sectionController.EndSection(sectionId);
            }
        }
    }
}