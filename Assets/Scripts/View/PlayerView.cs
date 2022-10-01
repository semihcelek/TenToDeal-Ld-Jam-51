using Cysharp.Threading.Tasks;
using SemihCelek.TenToDeal.Controller;
using SemihCelek.TenToDeal.Model;
using UnityEngine;
using Utilites;

namespace SemihCelek.TenToDeal.View
{
    public class PlayerView : MonoBehaviour, IView
    {
        [SerializeField] private PlayerSettings _playerSettings;

        private ICharacterInput _characterInputController;
        private IGameStateController _gameStateController;

        private Command _primaryPlayerAction;
        private Command _secondaryPlayerAction;

        private bool _canUseAction = true;

        private void Start()
        {
            InitializeDependencies();
        }

        private void InitializeDependencies()
        {
            _characterInputController = FindObjectOfType<PlayerInputController>();
            _gameStateController = FindObjectOfType<GameController>();
        }

        private void FixedUpdate()
        {
            if (_gameStateController.GameState != GameState.Idle)
            {
                return;
            }

            AdjustMovementForIsometricPerspective();

            CheckPlayerActionsAsync().Forget();
        }

        private async UniTaskVoid CheckPlayerActionsAsync()
        {
            bool isPrimaryExecuteHappened = _characterInputController.PrimaryExecute;
            bool isSecondaryExecuteHappened = _characterInputController.SecondaryExecute;

            if (isPrimaryExecuteHappened && _canUseAction)
            {
                _primaryPlayerAction?.Execute(gameObject);
                SuspendAbilityAsync(0.6f).Forget();
            }

            if (isSecondaryExecuteHappened && _canUseAction)
            {
                _secondaryPlayerAction?.Execute(gameObject);
                SuspendAbilityAsync(0.6f).Forget();
            }
        }

        private void AdjustMovementForIsometricPerspective()
        {
            Transform transformCache = transform;
            float speed = _playerSettings.speed;

            float horizontalInput = _characterInputController.HorizontalInput;
            float verticalInput = _characterInputController.VerticalInput;

            Vector3 position = transformCache.position;

            position += speed * horizontalInput * new Vector3(0.5f, 0, -0.5f);
            position += speed * verticalInput * new Vector3(0.5f, 0, 0.5f);

            transformCache.position = position;
        }

        private void OnCollisionEnter(Collision collision)
        {
            TryToGetAbility(collision);
        }

        private void TryToGetAbility(Collision collision)
        {
            ICommandAcquirable commandAcquirable = collision.collider.GetComponentInParent<ICommandAcquirable>();

            if (commandAcquirable != null)
            {
                _primaryPlayerAction = commandAcquirable.GetCommand();
            }
        }

        private async UniTask SuspendAbilityAsync(float suspendDuration)
        {
            _canUseAction = false;
            await UnitaskHelper.Delay(suspendDuration);
            _canUseAction = true;
        }
    }
}