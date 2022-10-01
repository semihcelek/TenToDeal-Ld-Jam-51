using SemihCelek.TenToDeal.Model;
using UnityEngine;

namespace SemihCelek.TenToDeal.Controller
{
    public class PlayerInputController : MonoBehaviour, ICharacterInput
    {
        public float HorizontalInput { get; }
        public float VerticalInput { get; }
        
        public bool Execute { get; }

        private IGameStateController _gameStateController;
        
        private void Start()
        {
            InitializeDependencies();
        }

        private void InitializeDependencies()
        {
            _gameStateController = FindObjectOfType<GameController>();
        }

        private void Update()
        {
            CheckPlayerInput();
        }

        private void CheckPlayerInput()
        {
            if (_gameStateController.GameState != GameState.Idle)
            {
                return;
            }
            
            
        }
    }
}