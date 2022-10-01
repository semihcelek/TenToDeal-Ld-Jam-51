using SemihCelek.TenToDeal.Model;
using UnityEngine;

namespace SemihCelek.TenToDeal.Controller
{
    public class GameController : MonoBehaviour, IGameStateController
    {
        public GameState GameState { get; private set; }

        private void Awake()
        {
            GameState = GameState.Idle;
        }

        public void AddState(GameState gameState)
        {
            if (GameState == gameState)
            {
                return;
            }

            GameState = gameState;
        }

        public void RemoveState(GameState gameState)
        {
            if (GameState != gameState)
            {
                return;
            }
        }
    }
}