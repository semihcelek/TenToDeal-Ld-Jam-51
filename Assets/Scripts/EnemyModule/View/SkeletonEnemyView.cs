using Cysharp.Threading.Tasks;
using DG.Tweening;
using SemihCelek.TenToDeal.CombatModule.Controller;
using SemihCelek.TenToDeal.Controller;
using SemihCelek.TenToDeal.EnemyModule.Model;
using SemihCelek.TenToDeal.HealthModule.Controller;
using SemihCelek.TenToDeal.HealthModule.View;
using SemihCelek.TenToDeal.LevelModule.View;
using SemihCelek.TenToDeal.Model;
using SemihCelek.TenToDeal.Utilites;
using SemihCelek.TenToDeal.View;
using UnityEngine;

namespace SemihCelek.TenToDeal.EnemyModule.View
{
    public class SkeletonEnemyView : EnemyView
    {
        [SerializeField] 
        private float _runRotation;
        [SerializeField] 
        private float _chaseDistance = 8f;
        [SerializeField] 
        private float _attackDistance = 1.5f;

        private bool _isSuspended;

        private IGameStateController _gameStateController;

        private PlayerView _playerView;

        private Animator _animator;

        private HealthView _healthView;

        private CombatController _combatController;
        
        private void Start()
        {
            InitializeDependencies();
            ListenEvents();

            EnemyState = EnemyState.Idle;
        }

        private void InitializeDependencies()
        {
            _gameStateController = FindObjectOfType<GameController>();
            _playerView = FindObjectOfType<PlayerView>();
            _combatController = FindObjectOfType<CombatController>();
            
            _enemyTaskObjectView = GetComponent<EnemyTaskObjectView>();
            _animator = GetComponentInChildren<Animator>();
            _healthView = GetComponentInChildren<HealthView>();
        }
        
        private void ListenEvents()
        {
            HealthController.HealthEntityDiedEvent += EntityDiedEvent;
        }

        private void EntityDiedEvent(HealthView healthView)
        {
            if (_enemyTaskObjectView.id == healthView.taskObjectView.id)
            {
                EnemyState = EnemyState.Die;
            }
        }

        private void FixedUpdate()
        {
            if ((_gameStateController.GameState & GameState.Failed) == GameState.Failed || _isSuspended)
            {
                return;
            }
            
            ProcessEnemyState();
        }

        private void ProcessEnemyState()
        {
            switch (EnemyState)
            {
                case EnemyState.Idle:
                    PlayIdleAnimation();
                    CheckPlayerDistance();
                    break;
                case EnemyState.Chase:
                    FollowPlayer();
                    break;
                case EnemyState.Die:
                    PlayDieAnimation();
                    break;
                case EnemyState.Attack:
                    PlayAttackAnimation();
                    break;
            }
        }

        private void CheckPlayerDistance()
        {
            float distance = CalculateDistanceTowardsPlayer();

            if (distance <= _chaseDistance)
            {
                EnemyState = EnemyState.Chase;
            }
            
            SuspendEnemyAsync(0.2f).Forget();
        }

        private void PlayIdleAnimation()
        {
        }

        private void FollowPlayer()
        {
            Transform transformCache = transform;

            Vector3 enemyPosition = transformCache.position;
            Vector3 playerPosition = _playerView.transform.position;

            transformCache.DOMove(playerPosition, 2f);
            
            float distance = Vector3.Distance(enemyPosition, playerPosition);

            if (distance > _attackDistance)
            {
                EnemyState = EnemyState.Idle;
            }
            else if (distance <= _attackDistance)
            {
                EnemyState = EnemyState.Attack;
            }

            SuspendEnemyAsync(0.2f).Forget();
        }

        private void PlayDieAnimation()
        {
            const float dieRotation = 90;
            
            transform.DORotate(Vector3.forward * dieRotation, 1f).SetEase(Ease.OutQuart)
                .OnComplete(() => Destroy(gameObject));
        }

        private void PlayAttackAnimation()
        {
            _combatController.Attack(10, _playerView.GetComponent<HealthView>());
            SuspendEnemyAsync(0.8f).Forget();
        }

        private float CalculateDistanceTowardsPlayer()
        {
            Transform transformCache = transform;

            Vector3 enemyPosition = transformCache.position;
            Vector3 playerPosition = _playerView.transform.position;

            float distance = Vector3.Distance(enemyPosition, playerPosition);
            return distance;
        }

        private async UniTaskVoid SuspendEnemyAsync(float suspendDuration)
        {
            _isSuspended = true;
            await UniTaskHelper.Delay(suspendDuration);
            _isSuspended = false;
        }
    }
}