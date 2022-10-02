namespace SemihCelek.TenToDeal.EnemyModule.Model
{
    public interface IEnemyState
    {
        void Initialize();
        void Kill();
        void Tick();
    }
}