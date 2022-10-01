using System;
using UnityEngine;

namespace SemihCelek.TenToDeal.Model
{
    public interface IInteractable
    {
        bool IsInteractable { get; }
        void Interact(GameObject interactor);
        void Use();
    }
}