using SemihCelek.TenToDeal.EnemyModule.Model;
using SemihCelek.TenToDeal.Model;
using SemihCelek.TenToDeal.UI;
using UnityEngine;

namespace SemihCelek.TenToDeal.EnemyModule.View
{
    public abstract class EnemyView : MonoBehaviour, IView
    {
        [SerializeField]
        protected ProgressBar _progressBar;
        
        protected EnemyState _enemyState;
        
    }
}