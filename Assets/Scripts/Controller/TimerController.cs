using System;
using Cysharp.Threading.Tasks;
using SemihCelek.TenToDeal.Model;
using SemihCelek.TenToDeal.Utilites;
using UnityEngine;

namespace SemihCelek.TenToDeal.Controller
{
    public class TimerController : MonoBehaviour, IController
    {
        public static event Action<float> TimerEndedEvent;
        public static event Action<float> TimerStartedEvent;

        public event Action<float> SecondElapsedEvent;

        private IGameStateController _gameStateController;

        private void Start()
        {
            InitializeDependencies();
            ListenEvents();
        }

        private void InitializeDependencies()
        {
            _gameStateController = FindObjectOfType<GameController>();
        }

        private void ListenEvents()
        {
            TimerStartedEvent += OnTimerStarted;
            _gameStateController.OnGameStateChanged += OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState gameState)
        {
            if (gameState == GameState.SectionStarted)
            {
                TimerStartedEvent?.Invoke(10f);
            }
        }

        private void OnTimerStarted(float duration)
        {
            StartTimerAsync(duration).Forget();
        }

        private async UniTaskVoid StartTimerAsync(float seconds)
        {
            for (int index = 0; index < seconds; index++)
            {
                await UniTaskHelper.Delay(1f);
                
                SecondElapsedEvent?.Invoke(seconds - index);
            }
            
            TimerEndedEvent?.Invoke(0);
        }
    }
}