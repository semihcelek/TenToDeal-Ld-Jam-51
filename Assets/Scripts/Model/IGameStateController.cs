using System;

namespace SemihCelek.TenToDeal.Model
{
    public interface IGameStateController
    {
        event Action<GameState> OnGameStateChanged; 
        
        public GameState GameState { get; }

        void AddState(GameState gameState);
        void RemoveState(GameState gameState);
    }
}