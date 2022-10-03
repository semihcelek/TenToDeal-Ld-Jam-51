using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SemihCelek.TenToDeal.Model
{
    public interface IInteractable
    {
        int id { get; }
        
        bool IsInteractable { get; }
        
        void Interact(GameObject interactor);
        
        UniTask UseAsync();
    }
}