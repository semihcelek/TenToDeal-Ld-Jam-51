using DG.Tweening;
using UnityEngine;

namespace SemihCelek.TenToDeal.CombatModule.Model
{
    [CreateAssetMenu(fileName = "New Sword Weapon Asset Data", menuName = "SemihCelek/TenToDeal/SwordWeaponAssetData",
        order = 0)]
    public class SwordWeapon : WeaponAssetData
    {
        public Vector3 maxRotationVector;

        public override void PlayWeaponAnimation(GameObject weaponGameObject)
        {
            Sequence sequence = DOTween.Sequence();

            sequence
                .Append(weaponGameObject.transform.DOLocalRotate(maxRotationVector, 0.1f, RotateMode.Fast)
                    .SetEase(Ease.OutExpo))
                .Append(weaponGameObject.transform
                    .DOLocalRotate(Vector3.zero, 0.3f, RotateMode.Fast)
                    .SetEase(Ease.InOutSine));
        }
    }
}