namespace Repository.EntityDefaults
{
    using Microsoft.AspNetCore.Identity;

    public interface ICreatedUser<TUser, TKey> where TUser : IdentityUser<TKey> where TKey : IEquatable<TKey>
    {
        TUser CreatedUser { get; set; }
    }
}