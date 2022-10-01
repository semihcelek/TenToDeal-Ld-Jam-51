using System;
using SemihCelek.TenToDeal.Model;
using UnityEngine;

namespace SemihCelek.TenToDeal.CombatModule.Model
{
    [Serializable]
    public class SwordCommand : Command
    {
        public override void Execute(GameObject actorGameObject)
        {
            Debug.Log("Execute Sword");
        }
    }
}