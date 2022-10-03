using SemihCelek.TenToDeal.EnemyModule.Model;
using SemihCelek.TenToDeal.EnemyModule.View;
using SemihCelek.TenToDeal.HealthModule.Controller;
using SemihCelek.TenToDeal.HealthModule.Model;
using SemihCelek.TenToDeal.HealthModule.View;
using UnityEngine;

namespace SemihCelek.TenToDeal.LevelModule.View
{
    public class EnemyTaskObjectView : TaskObjectView
    {
        private EnemyView _enemyView;
        private HealthView _enemyHealthView;
        
        protected override void InitializeDependencies()
        {
            base.InitializeDependencies();

            _enemyView = GetComponent<EnemyView>();
            _enemyHealthView = GetComponent<HealthView>();

            ListenEvents();
        }

        private void ListenEvents()
        {
            HealthController.HealthEntityDiedEvent += OnHealthEntityDiedEvent;
        }

        private void OnHealthEntityDiedEvent(HealthView healthView)
        {
            CheckForDeath(healthView);
        }

        private void CheckForDeath(HealthView healthView)
        {
            if (sectionId != _sectionController.CurrentSection)
            {
                return;
            }
            
            if (completedStatus)
            {
                return;
            }
            
            if (healthView.taskObjectView.id == _enemyHealthView.taskObjectView.id)
            {
                _taskController.CompleteTask(sectionId, id);
            }
        }

        protected override void OnTriggerEnter(Collider other)
        {
            // ignored
        }
    }
}