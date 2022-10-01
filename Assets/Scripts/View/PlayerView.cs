using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using SemihCelek.TenToDeal.Controller;
using SemihCelek.TenToDeal.Model;
using SemihCelek.TenToDeal.Utilites;
using UnityEngine;

namespace SemihCelek.TenToDeal.View
{
    public class PlayerView : MonoBehaviour, IInteractor, IView
    {
        [SerializeField] private PlayerSettings _playerSettings;

        private ICharacterInput _characterInputController;
        private IGameStateController _gameStateController;

        private IInteractable _primaryPlayerInteractable;
        private IInteractable _secondaryPlayerInteractable;

        private bool _canUseAction = true;

        private readonly Vector3 _horizontalMovementVector = new Vector3(0.5f, 0, -0.5f);
        private readonly Vector3 _verticalMovementVector = new Vector3(0.5f, 0, 0.5f);

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
            AdjustRotation();

            CheckPlayerActionsAsync().Forget();
        }

        private void AdjustRotation()
        {
            Transform transformCache = transform;

            float currentYRotationAxis = transformCache.rotation.y;
            float processedYRotation = ProcessYAxisRotation();

            if (Math.Abs(currentYRotationAxis - processedYRotation) < 0.01f)
            {
                return;
            }

            Vector3 rotationVector =
                new Vector3(transformCache.rotation.x, processedYRotation, transformCache.rotation.z);
            transformCache.DORotate(rotationVector, 0.2f).SetEase(Ease.OutExpo);
        }

        private float ProcessYAxisRotation()
        {
            float horizontalInput = _characterInputController.HorizontalInput;
            float verticalInput = _characterInputController.VerticalInput;

            float yRotation = 0f;

            switch (horizontalInput)
            {
                case 0f:
                    yRotation += 0f;
                    break;
                case > 0f:
                    yRotation += 45f;
                    break;
                case < 0f:
                    yRotation -= 135f;
                    break;
            }

            switch (verticalInput)
            {
                case 0f:
                    yRotation += 0f;
                    break;
                case > 0f:
                    yRotation -= 45f;
                    break;
                case < 0:
                    yRotation += 135f;
                    break;
            }

            if (verticalInput > 0 && horizontalInput > 0)
            {
                yRotation = -0f;
            }

            if (verticalInput > 0 && horizontalInput < 0)
            {
                yRotation = -90f;
            }

            if (verticalInput < 0 && horizontalInput > 0)
            {
                yRotation = 90f;
            }

            if (verticalInput < 0 && horizontalInput < 0)
            {
                yRotation = -180;
            }

            return yRotation;
        }

        private async UniTaskVoid CheckPlayerActionsAsync()
        {
            bool isPrimaryExecuteHappened = _characterInputController.PrimaryExecute;
            bool isSecondaryExecuteHappened = _characterInputController.SecondaryExecute;

            if (isPrimaryExecuteHappened && _canUseAction)
            {
                _primaryPlayerInteractable?.Use();
                SuspendAbilityAsync(0.1f).Forget();
            }

            if (isSecondaryExecuteHappened && _canUseAction)
            {
                _secondaryPlayerInteractable?.Use();
                SuspendAbilityAsync(0.1f).Forget();
            }
        }

        private void AdjustMovementForIsometricPerspective()
        {
            Transform transformCache = transform;
            float speed = _playerSettings.speed;

            float horizontalInput = _characterInputController.HorizontalInput;
            float verticalInput = _characterInputController.VerticalInput;

            Vector3 position = transformCache.position;

            position += speed * horizontalInput * _horizontalMovementVector;
            position += speed * verticalInput * _verticalMovementVector;

            transformCache.position = position;
        }

        private void OnCollisionEnter(Collision collision)
        {
            TryToGetAbility(collision);
        }

        private void TryToGetAbility(Collision collision)
        {
            IInteractable interactable = collision.collider.GetComponentInParent<IInteractable>();

            if (interactable == null)
            {
                return;
            }

            if (_primaryPlayerInteractable != null && _secondaryPlayerInteractable != null)
            {
                return;
            }

            if (_primaryPlayerInteractable is null)
            {
                _primaryPlayerInteractable = interactable;
            }

            if (_secondaryPlayerInteractable is null)
            {
                _secondaryPlayerInteractable = interactable;
            }

            interactable.Interact(gameObject);
        }

        private async UniTask SuspendAbilityAsync(float suspendDuration)
        {
            _canUseAction = false;
            await UnitaskHelper.Delay(suspendDuration);
            _canUseAction = true;
        }
    }
}