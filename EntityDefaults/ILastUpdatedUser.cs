namespace Repository.EntityDefaults
{
    using Microsoft.AspNetCore.Identity;

    public interface ILastUpdatedUser<TUser, TKey> where TUser : IdentityUser<TKey> where TKey : IEquatable<TKey>
    {
        TUser LastUpdatedUser { get; set; }
    }
}