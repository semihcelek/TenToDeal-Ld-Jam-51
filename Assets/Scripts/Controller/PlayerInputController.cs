using SemihCelek.TenToDeal.Model;
using UnityEngine;

namespace SemihCelek.TenToDeal.Controller
{
    public class PlayerInputController : MonoBehaviour, ICharacterInput
    {
        public float HorizontalInput { get; private set; }
        public float VerticalInput { get; private set; }
        
        public bool PrimaryExecute { get; private set; }
        public bool SecondaryExecute { get; private set; }

        
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

            HorizontalInput = Input.GetAxisRaw("Horizontal");
            VerticalInput = Input.GetAxisRaw("Vertical");

            PrimaryExecute = Input.GetAxisRaw("Fire1") >= 1f;
            SecondaryExecute = Input.GetAxisRaw("Fire2") >= 1f;
        }

    }
}