namespace Repository.EntityDefaults
{
    using global::EntityDefaults;
    using Microsoft.AspNetCore.Identity;

    public class LastUpdatedUserSetter<TUser, TKey> : IDefaultEntitySetter where TUser : IdentityUser<TKey> where TKey : IEquatable<TKey>
    {
        private readonly TUser _user;

        public LastUpdatedUserSetter(TUser user)
        {
            _user = user;
        }

        public void Set(EntityTransaction entityTransaction)
        {
            if (entityTransaction.Entity is ILastUpdatedUser<TUser, TKey> lastUpdatedUser)
            {
                lastUpdatedUser.LastUpdatedUser = _user;
            }
        }
    }
}