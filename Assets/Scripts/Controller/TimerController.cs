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

        private void Start()
        {
            ListenEvents();
        }

        private void ListenEvents()
        {
            TimerStartedEvent += OnTimerStarted;
        }

        private void OnTimerStarted(float duration)
        {
            StartTimerAsync(duration).Forget();
        }

        private async UniTaskVoid StartTimerAsync(float seconds)
        {
            for (int index = 0; index < seconds; index++)
            {
                await UnitaskHelper.Delay(1f);
                
                SecondElapsedEvent?.Invoke(seconds - index);
            }
            
            TimerEndedEvent?.Invoke(0);
        }
    }
}