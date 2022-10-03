using System;
using System.Collections.Generic;
using System.Linq;
using SemihCelek.TenToDeal.LevelModule.View;
using SemihCelek.TenToDeal.Model;
using UnityEngine;

namespace SemihCelek.TenToDeal.Controller
{
    public class TaskController : MonoBehaviour, IController
    {
        public event Action<int> TaskCompletedEvent;

        private SectionController _sectionController;

        private TimerController _timerController;

        private GameController _gameController;

        private Dictionary<int, List<TaskObjectView>> _allSectionTasksDictionary = new ();

        private Queue<int> _completedTaskQueue = new (4);

        private void Start()
        {
            InitializeDependencies();
            ListenEvents();
        }


        private void InitializeDependencies()
        {
            _sectionController = FindObjectOfType<SectionController>();
            _timerController = FindObjectOfType<TimerController>();
            _gameController = FindObjectOfType<GameController>();

            FindAllTasks();
        }

        private void ListenEvents()
        {
            _gameController.OnGameStateChanged += OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState gameState)
        {
            if ((gameState & GameState.TimerCycleCompleted) == GameState.TimerCycleCompleted)
            {
                CheckCompletedTaskQueue();
            }
        }

        private void CheckCompletedTaskQueue()
        {
            if (_completedTaskQueue.Count <= 0)
            {
                _gameController.AddState(GameState.Failed);
                return;
            }

            int completedSection = _completedTaskQueue.Count;
            Debug.Log(completedSection);
            TaskCompletedEvent?.Invoke(completedSection);
            
            _gameController.AddState(GameState.Idle);
        }


        private void FindAllTasks()
        {
            TaskObjectView[] _allTasks =
                FindObjectsOfType<TaskObjectView>()
                    .Where(t => t.completedStatus == false)
                    .OrderBy(t => t.sectionId)
                    .ToArray();

            for (int index = 0; index < 5; index++)
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
            
            RemoveTaskFromTaskDictionary(sectionId, id);
            CheckWhetherAllTaskAreCompleted();
        }

        private void RemoveTaskFromTaskDictionary(int sectionId, int id)
        {
            List<TaskObjectView> currentTaskList = _allSectionTasksDictionary[sectionId];

            TaskObjectView taskObjectViewToRemove = currentTaskList.Find(t => t.id == id);

            currentTaskList.Remove(taskObjectViewToRemove);
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
                    _completedTaskQueue.Enqueue(completedSectionCount);
                }

                remainingTaskCount += taskObjectViews.Count;
            }
        }
    }
}