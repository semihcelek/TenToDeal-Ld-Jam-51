using System.Linq;
using SemihCelek.TenToDeal.Controller;
using SemihCelek.TenToDeal.View.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SemihCelek.TenToDeal.UI
{
    public class TimerCanvas : UIPanel
    {
        [SerializeField] 
        private Image _backgroundImage;

        [SerializeField]
        private TMP_Text _timerText;

        [SerializeField]
        private TimerBackgroundSpriteAssetData[] _spriteAssetDatas;

        private TimerController _timerController;

        private float _totalTimerSeconds;

        private void Start()
        {
            InitializeDependencies();
            ListenEvents();
        }

        private void InitializeDependencies()
        {
            _timerController = FindObjectOfType<TimerController>();
        }

        private void ListenEvents()
        {
            TimerController.TimerStartedEvent += OnTimerStarted;
            TimerController.TimerEndedEvent += OnTimerEnded;

            _timerController.SecondElapsedEvent += OnSecondElapsed;
        }

        private void OnTimerStarted(float totalSeconds)
        {
            TogglePanel(true);
            _totalTimerSeconds = totalSeconds;
            Debug.Log("timer start");
        }

        private void OnTimerEnded(float obj)
        {
            TogglePanel(false);
            _totalTimerSeconds = 0f;
            Debug.Log("timer end");

        }

        private void OnSecondElapsed(float elapsedSeconds)
        {
            Debug.Log("timer elapse");

            float remainingTimerPercentage = (100f * elapsedSeconds) / _totalTimerSeconds;

            Debug.Log(remainingTimerPercentage);
            
            if (remainingTimerPercentage <= (float)TimerColors.Red)
            {
                UpdateTimerView(elapsedSeconds, TimerColors.Red);
                return;
            }

            if (remainingTimerPercentage <= (float)TimerColors.Yellow)
            {
                UpdateTimerView(elapsedSeconds, TimerColors.Yellow);
                return;
            }

            UpdateTimerView(elapsedSeconds, TimerColors.Green);
        }

        private void UpdateTimerView(float elapsedSeconds, TimerColors color)
        {
            Debug.Log("update text " + elapsedSeconds + color );

            _timerText.text = elapsedSeconds.ToString();
            _backgroundImage.sprite = _spriteAssetDatas.Where(s => s.color == color).First().sprite;
        }

        private void UnsubscribeEvents()
        {
            TimerController.TimerStartedEvent -= OnTimerStarted;
            TimerController.TimerEndedEvent -= OnTimerEnded;

            _timerController.SecondElapsedEvent -= OnSecondElapsed;
        }
        
        private void OnDestroy()
        {
            UnsubscribeEvents();
        }
    }

    public enum TimerColors
    {
        Green = 100,
        Yellow = 50,
        Red = 20
    }
}