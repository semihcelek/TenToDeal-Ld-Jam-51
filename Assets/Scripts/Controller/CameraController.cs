using SemihCelek.TenToDeal.Model;
using SemihCelek.TenToDeal.View;
using UnityEngine;

namespace SemihCelek.TenToDeal.Controller
{
    public class CameraController : MonoBehaviour, IController
    {
        private PlayerView _playerView;

        private Transform _cameraParentTransform;

        private void Start()
        {
            InitializeDependencies();
        }

        private void InitializeDependencies()
        {
            _playerView = FindObjectOfType<PlayerView>();
            _cameraParentTransform = Camera.main.transform.parent;
        }

        private void FixedUpdate()
        {
            FollowPlayerPosition();
        }

        private void FollowPlayerPosition()
        {
            const float followSpeed = 1.6f;

            Vector3 targetPosition = _playerView.transform.position;
            Vector3 cameraPosition = _cameraParentTransform.position;

            _cameraParentTransform.position =
                Vector3.Lerp(cameraPosition, targetPosition, Time.deltaTime * followSpeed);
        }
    }
}