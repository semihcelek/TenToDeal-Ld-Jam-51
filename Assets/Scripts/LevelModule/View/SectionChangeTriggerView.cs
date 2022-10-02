using SemihCelek.TenToDeal.Controller;
using SemihCelek.TenToDeal.Model;
using SemihCelek.TenToDeal.View;
using UnityEngine;

namespace SemihCelek.TenToDeal.LevelModule.View
{
    public class SectionChangeTriggerView : MonoBehaviour, IView
    {
        [SerializeField]
        private bool _isSectionStart = true;
        
        private GameController _gameController;
        
        private void Start()
        {
            InitializeDependencies();
        }

        private void InitializeDependencies()
        {
            _gameController = FindObjectOfType<GameController>();
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
                _gameController.EnterGameSection();
            }
            else
            {
                _gameController.CompleteGameSection();
            }
        }
    }
}