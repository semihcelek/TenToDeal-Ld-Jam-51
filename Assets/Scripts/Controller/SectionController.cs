using System;
using SemihCelek.TenToDeal.Model;
using UnityEngine;

namespace SemihCelek.TenToDeal.Controller
{
    public class SectionController : MonoBehaviour, IController
    {
        public event Action<int> SectionStartedEvent;
        public event Action<int> SectionEndedEvent;

        private IGameStateController _gameStateController;
        
        private int _currentSection;
        public int CurrentSection => _currentSection;

        private void Start()
        {
            InitializeDependencies();
            
            PlayerPrefs.DeleteAll();
        }

        private void InitializeDependencies()
        {
            _gameStateController = FindObjectOfType<GameController>();
        }

        public void StartSection(int sectionId)
        {
            int savedSection = TryGetSavedSection(sectionId);

            if (savedSection != - 1)
            {
                return;
            }

            _currentSection = sectionId;
            _gameStateController.AddState(GameState.SectionStarted);
            SectionStartedEvent?.Invoke(sectionId);
        }

        public void EndSection(int sectionId)
        {
            int savedSection = TryGetSavedSection(sectionId);

            if (savedSection == sectionId)
            {
                return;
            }

            _gameStateController.AddState(GameState.SectionCompleted);
            SectionEndedEvent?.Invoke(sectionId);
            SaveCompletedSection(sectionId);
        }

        private void SaveCompletedSection(int sectionId)
        {
            PlayerPrefs.SetInt($"CompletedSection_{sectionId}", sectionId);
        }

        private int TryGetSavedSection(int sectionId)
        {
            int section = PlayerPrefs.GetInt($"CompletedSection_{sectionId}", -1);
            return section;
        }
    }
}