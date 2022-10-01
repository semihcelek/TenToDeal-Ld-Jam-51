using System;
using UnityEngine;

namespace SemihCelek.TenToDeal.Model
{
    public abstract class Command
    {
        public abstract void Execute(GameObject actorGameObject);
    }
}