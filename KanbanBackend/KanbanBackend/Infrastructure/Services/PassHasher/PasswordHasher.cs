namespace KanbanBackend.Infrastructure.Services.PassHasher
{
    public class PasswordHasher : IPasswordHasher
    {
        private readonly Microsoft.AspNetCore.Identity.PasswordHasher<object> _hasher = new();
        public string Hash(string password)
            => _hasher.HashPassword(null!, password);

        public bool Verify(string hash, string password)
            => _hasher.VerifyHashedPassword(null!, hash, password)
            == Microsoft.AspNetCore.Identity.PasswordVerificationResult.Success;
    }
}
