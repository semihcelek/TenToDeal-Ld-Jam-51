using Cysharp.Threading.Tasks;
using DG.Tweening;
using SemihCelek.TenToDeal.Controller;
using SemihCelek.TenToDeal.Utilites;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SemihCelek.TenToDeal.UI
{
    public class SectionCompletedPopUp : UIPanel
    {
        [SerializeField] 
        private TMP_Text _bannerText;

        [SerializeField] 
        private Image _backgroundImage;
        
        private TaskController _taskController;

        private void Start()
        {
            InitializeDependencies();
            ListenEvents();
            TogglePanel(false);
        }

        private void InitializeDependencies()
        {
            _taskController = FindObjectOfType<TaskController>();
        }
        
        private void ListenEvents()
        {
            _taskController.TaskCompletedEvent += OnTaskCompleted;
        }

        private void OnTaskCompleted(int sectionId)
        {
            TogglePanel(true);
            DisplayPopUpAsync(sectionId).Forget();
        }

        private async UniTaskVoid DisplayPopUpAsync(int sectionId)
        {
            _bannerText.text = $"Dungeon {sectionId} is completed. Go to {sectionId + 1} dungeon.";
            
            _backgroundImage.DOFade(1f, 2f);
            _bannerText.DOFade(1f, 2f);

            await UniTaskHelper.Delay(4f);

            _backgroundImage.DOFade(0f, 2f);
            _bannerText.DOFade(0f, 2f).OnComplete(() => TogglePanel(false));
        }
    }
}