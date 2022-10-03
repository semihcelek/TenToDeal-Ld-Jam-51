using System;
using SemihCelek.TenToDeal.Model;
using UnityEngine;

namespace SemihCelek.TenToDeal.Controller
{
    public class GameController : MonoBehaviour, IGameStateController
    {
        public event Action<GameState> OnGameStateChanged;
        public GameState GameState { get; private set; }

        private SectionController _sectionController;

        private void Awake()
        {
            GameState = GameState.Idle;
        }

        private void Start()
        {
            InitializeDependencies();
        }

        private void InitializeDependencies()
        {
            _sectionController = FindObjectOfType<SectionController>();
        }

        public void AddState(GameState gameState)
        {
            if (GameState == gameState)
            {
                return;
            }

            GameState = gameState;
            OnGameStateChanged?.Invoke(gameState);
        }

        public void RemoveState(GameState gameState)
        {
            if (GameState != gameState)
            {
                return;
            }

            OnGameStateChanged?.Invoke(gameState);
        }
    }
}