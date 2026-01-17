namespace KanbanBackend.Infrastructure.Services.PassHasher
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool Verify(string hash, string password);
    }
}
