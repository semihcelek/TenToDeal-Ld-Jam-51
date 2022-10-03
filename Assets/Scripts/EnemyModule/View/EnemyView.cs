using SemihCelek.TenToDeal.EnemyModule.Model;
using SemihCelek.TenToDeal.LevelModule.View;
using SemihCelek.TenToDeal.Model;
using SemihCelek.TenToDeal.UI;
using UnityEngine;

namespace SemihCelek.TenToDeal.EnemyModule.View
{
    public abstract class EnemyView : MonoBehaviour, IView
    {
        public EnemyState EnemyState { get; set; }

        public EnemyTaskObjectView _enemyTaskObjectView;
    }
}