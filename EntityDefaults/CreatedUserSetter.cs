namespace Repository.EntityDefaults
{
    using global::EntityDefaults;
    using Microsoft.AspNetCore.Identity;

    public class CreatedUserSetter<TUser, TKey> : IDefaultEntitySetter where TUser : IdentityUser<TKey> where TKey : IEquatable<TKey>
    {
        private readonly TUser _user;

        public CreatedUserSetter(TUser user)
        {
            _user = user;
        }

        public void Set(EntityTransaction entityTransaction)
        {
            if (entityTransaction.State == TransactionState.Add && entityTransaction.Entity is ICreatedUser<TUser, TKey> createdUser)
            {
                createdUser.CreatedUser = _user;
            }
        }
    }
}