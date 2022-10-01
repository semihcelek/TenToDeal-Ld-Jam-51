namespace SemihCelek.TenToDeal.Model
{
    public interface IGameStateController
    {
        public GameState GameState { get; }

        void AddState(GameState gameState);
        void RemoveState(GameState gameState);
    }
}