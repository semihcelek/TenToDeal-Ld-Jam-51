using DG.Tweening;
using UnityEngine;

namespace SemihCelek.TenToDeal.UI
{
    public class MenuUIPanel : UIPanel
    {
        private CanvasGroup _canvasGroup;

        private void Start()
        {
            InitializeDependencies();
        }

        private void InitializeDependencies()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void PlayButton()
        {
            _canvasGroup.DOFade(0f, 1f).OnComplete(() => gameObject.SetActive(false));
        }

        public void ExitButton()
        {
            Application.Quit();
        }
    }
}