namespace SemihCelek.TenToDeal.Model
{
    public interface ICharacterInput
    {
        float HorizontalInput { get; }
        float VerticalInput { get; }
        
        bool Execute { get; }
    }
}