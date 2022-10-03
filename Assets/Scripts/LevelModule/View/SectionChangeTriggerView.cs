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

        private bool _isInteracted;

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

            if (!isPlayerGameObject || _isInteracted)
            {
                return;
            }

            if (_isSectionStart)
            {
                _sectionController.StartSection(sectionId);
                _isInteracted = true;
            }
            else
            {
                _sectionController.EndSection(sectionId);
            }
        }
    }
}