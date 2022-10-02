namespace SemihCelek.TenToDeal.HealthModule.Model
{
    public interface IHealthEntity
    {
         HealthAssetData HealthAssetData { get; }
         int CurrentHealth { get; set; }
    }
}