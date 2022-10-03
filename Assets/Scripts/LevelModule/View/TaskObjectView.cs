using SemihCelek.TenToDeal.Controller;
using SemihCelek.TenToDeal.Model;
using SemihCelek.TenToDeal.View;
using UnityEngine;

namespace SemihCelek.TenToDeal.LevelModule.View
{
    public class TaskObjectView : MonoBehaviour, IView
    {
        public int sectionId;
        public int id;

        public bool completedStatus;

        protected TaskController _taskController;
        
        protected SectionController _sectionController;

        protected virtual void OnTaskCompleted() { }
        
        private void Start()
        {
            InitializeDependencies();
        }

        protected virtual void InitializeDependencies()
        {
            _taskController = FindObjectOfType<TaskController>();
            _sectionController = FindObjectOfType<SectionController>();
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (sectionId != _sectionController.CurrentSection)
            {
                return;
            }
            
            if (completedStatus)
            {
                return;
            }

            bool isPlayer = other.TryGetComponent(out PlayerView playerView);

            if (!isPlayer)
            {
                return;
            }

            completedStatus = true;
            _taskController.CompleteTask(sectionId, id);
            
            OnTaskCompleted();
        }
    }
}