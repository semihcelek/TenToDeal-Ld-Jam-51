using System;
using System.Collections.Generic;
using System.Linq;
using SemihCelek.TenToDeal.EnemyModule.View;
using SemihCelek.TenToDeal.LevelModule.View;
using SemihCelek.TenToDeal.Model;
using UnityEngine;

namespace SemihCelek.TenToDeal.Controller
{
    public class TaskController : MonoBehaviour, IController
    {
        private event Action<int, int> TaskCompletedEvent;

        private SectionController _sectionController;

        private Dictionary<int, List<TaskObjectView>> _allSectionTasksDictionary = new ();

        private void Start()
        {
            InitializeDependencies();
        }

        private void InitializeDependencies()
        {
            _sectionController = FindObjectOfType<SectionController>();

            FindAllTasks();
        }

        private void FindAllTasks()
        {
            TaskObjectView[] _allTasks =
                FindObjectsOfType<TaskObjectView>().Where(t => t.completedStatus == false).ToArray();

            for (int index = 0; index < 4; index++)
            {
                _allSectionTasksDictionary.Add(index, new List<TaskObjectView>());
            }

            for (int index = 0; index < _allTasks.Length; index++)
            {
                int sectionId = _allTasks[index].sectionId;

                if (_allSectionTasksDictionary.ContainsKey(sectionId))
                {
                    List<TaskObjectView> sectionTasks = _allSectionTasksDictionary[sectionId];
                    
                    sectionTasks.Add(_allTasks[index]);
                    Debug.Log("added");
                }
            }
        }

        public void CompleteTask(int sectionId, int id)
        {
            int currentSection = _sectionController.CurrentSection;

            if (sectionId != currentSection)
            {
                return;
            }

            if (!_allSectionTasksDictionary.ContainsKey(sectionId))
            {
                return;
            }

            // List<TaskObjectView> taskObjectViews = _allSectionTasksDictionary[sectionId];

            TaskCompletedEvent?.Invoke(sectionId, id);

            RemoveTaskFromTaskDictionary(sectionId, id);

            CheckWhetherAllTaskAreCompleted();
        }

        private void RemoveTaskFromTaskDictionary(int sectionId, int id)
        {
            List<TaskObjectView> currentTaskList = _allSectionTasksDictionary[sectionId];

            TaskObjectView taskObjectViewToRemove = currentTaskList.Find(t => t.id == id);

            currentTaskList.Remove(taskObjectViewToRemove);
            Debug.Log("removed");
        }

        private void CheckWhetherAllTaskAreCompleted()
        {
            int remainingTaskCount = 0;
            int completedSectionCount = 0;

            for (int index = 0; index < _allSectionTasksDictionary.Count; index++)
            {
                List<TaskObjectView> taskObjectViews = _allSectionTasksDictionary[index];

                if (taskObjectViews.Count <= 0)
                {
                    completedSectionCount++;
                    Debug.Log("section Completed" + index);
                }

                remainingTaskCount += taskObjectViews.Count;
                Debug.Log(remainingTaskCount);
            }
        }
    }
}